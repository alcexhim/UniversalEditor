using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Icarus;
using UniversalEditor.ObjectModels.Icarus.Commands;

namespace UniversalEditor.DataFormats.Icarus
{
	public class IcarusBinaryDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(IcarusScriptObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IcarusScriptObjectModel script = (objectModel as IcarusScriptObjectModel);

			Reader br = base.Accessor.Reader;
			br.Accessor.Seek(0, SeekOrigin.Begin);
			string IBI = br.ReadNullTerminatedString(4);
			DateTime dt = br.ReadDateTime32();
			script.Commands.Clear();

			try
			{
				while (!br.EndOfStream)
				{
					IcarusCommand cmd = ReadCommand(br);
					script.Commands.Add(cmd);
				}
			}
			catch (System.IO.EndOfStreamException ex)
			{
				throw new InvalidDataFormatException(ex.Message);
			}
		}

		private IcarusCommand ReadCommand(Reader br)
		{
			IcarusCommand command = null;
			IcarusCommandType commandType = (IcarusCommandType)br.ReadInt32();
			int parameterCount = br.ReadInt32();            // 2
			byte n3 = br.ReadByte();            // 0

			List<IcarusExpression> parameters = new List<IcarusExpression>();
			for (int i = 0; i < parameterCount; i++)
			{
				IcarusExpression expr = ReadIBIExpression(br);
				if (expr.Type == IcarusExpressionType.Get)
				{
					parameterCount -= 2;
				}
				else if (expr.Type == IcarusExpressionType.Vector)
				{
					parameterCount -= 3;
				}
				parameters.Add(expr);
			}

			switch (commandType)
			{
				case IcarusCommandType.Affect:
				{
					IcarusCommandAffect cmd = new IcarusCommandAffect();

					IcarusExpression target = parameters[0];
					IcarusExpression affectType = parameters[1];

					cmd.Target = target;
					if (affectType.Type == IcarusExpressionType.Float)
					{
						cmd.AffectType = new IcarusExpression(IcarusExpressionType.Float, (IcarusAffectType)(float)affectType.Value);
					}
					else
					{
						cmd.AffectType = affectType;
					}
					command = cmd;
					break;
				}
				case IcarusCommandType.Camera:
				{
					command = new IcarusCommandCamera();

					IcarusCommandCamera cmd = (command as IcarusCommandCamera);
					cmd.Operation = (IcarusCameraOperation)((float)(parameters[0].Value));
					for (int i = 1; i < parameters.Count; i++)
					{
						cmd.Parameters.Add(parameters[i]);
					}
					break;
				}
				case IcarusCommandType.Declare: command = new IcarusCommandDeclare(); break;
				case IcarusCommandType.Do:
				{
					command = new IcarusCommandControlFlowDo();

					IcarusCommandControlFlowDo cmd = (command as IcarusCommandControlFlowDo);
					IcarusExpression target = parameters[0];
					cmd.Target = target.Value.ToString();
					break;
				}
				case IcarusCommandType.End:
				{
					command = new IcarusCommandEnd();
					break;
				}
				case IcarusCommandType.Flush: command = new IcarusCommandFlush(); break;
				case IcarusCommandType.Kill: command = new IcarusCommandKill(); break;
				case IcarusCommandType.Loop:
				{
					command = new IcarusCommandLoop();

					IcarusCommandLoop cmd = (command as IcarusCommandLoop);
					IcarusExpression loopCount = parameters[0];
					cmd.Count = (float)(loopCount.Value);
					break;
				}
				case IcarusCommandType.None: break;
				case IcarusCommandType.Print:
				{
					command = new IcarusCommandPrint();

					IcarusCommandPrint cmd = (command as IcarusCommandPrint);
					IcarusExpression text = parameters[0];
					cmd.Text = text.Value.ToString();
					break;
				}
				case IcarusCommandType.Rotate: command = new IcarusCommandRotate(); break;
				case IcarusCommandType.Run: command = new IcarusCommandRun(); break;
				case IcarusCommandType.Set:
				{
					command = new IcarusCommandSet();

					IcarusExpression objectName = parameters[0];
					IcarusExpression value = parameters[1];

					(command as IcarusCommandSet).ObjectName = objectName.Value.ToString();
					(command as IcarusCommandSet).Value = value.Value.ToString();
					break;
				}
				case IcarusCommandType.Signal: command = new IcarusCommandSignal(); break;
				case IcarusCommandType.Sound:
				{
					command = new IcarusCommandSound();
					
					IcarusCommandSound cmd = (command as IcarusCommandSound);
					// cmd.Channel = parameters[0].Value;
					// cmd.FileName = parameters[1].Value;
					break;
				}
				case IcarusCommandType.Task:
				{
					command = new IcarusCommandTask();

					IcarusCommandTask cmd = (command as IcarusCommandTask);
					
					IcarusExpression taskName = parameters[0];
					cmd.TaskName = taskName.Value.ToString();
					break;
				}
				case IcarusCommandType.Use: command = new IcarusCommandUse(); break;
				case IcarusCommandType.Wait:
				{
					command = new IcarusCommandWait();

					IcarusExpression duration = parameters[0];
					if (duration.Value is string)
					{
						(command as IcarusCommandWait).Target = duration.Value.ToString();
					}
					else
					{
						(command as IcarusCommandWait).Duration = (float)(duration.Value);
					}
					break;
				}
				case IcarusCommandType.WaitSignal: command = new IcarusCommandWaitSignal(); break;
			}

			if (command is IIcarusContainerCommand)
			{
				IcarusCommand child = null;
				while ((child = ReadCommand(br)) != null)
				{
					if (child is IcarusCommandEnd)
					{
						break;
					}
					else
					{
						(command as IIcarusContainerCommand).Commands.Add(child);
					}
				}
			}

			return command;

			/*
			if (commandType == IcarusCommandType.End) return new IcarusCommandEnd();
			if (commandType == IcarusCommandType.Flush) return new IcarusCommandFlush();

			switch (commandType)
			{
				case IcarusCommandType.Affect:
				{
					IcarusCommandAffect command = new IcarusCommandAffect();

					command.Target = ReadIBIExpression(br);
					IcarusExpression expr = ReadIBIExpression(br);

					IcarusCommand child = null;
					while ((child = ReadCommand(br)) != null)
					{
						if (child.GetType() == typeof(IcarusCommandEnd))
						{
							break;
						}
						else
						{
							command.Commands.Add(child);
						}
					}

					return command;
				}
				case IcarusCommandType.Loop:
				{
					IcarusCommandLoop command = new IcarusCommandLoop();
					command.Target = ReadIBIExpression(br);
					
					IcarusCommand child = null;
					while ((child = ReadCommand(br)) != null)
					{
						if (child.GetType() == typeof(IcarusCommandEnd))
						{
							break;
						}
						else
						{
							command.Commands.Add(child);
						}
					}

					return command;
				}
				case IcarusCommandType.Rotate:
				{
					IcarusCommandRotate command = new IcarusCommandRotate();
					command.Rotation = ReadIBIExpression(br);
					command.Speed = ReadIBIExpression(br);

					return command;
				}
				case IcarusCommandType.Wait:
				{
					IcarusCommandWait command = new IcarusCommandWait();
					command.Target = ReadIBIExpression(br);
					return command;
				}
				case IcarusCommandType.Declare:
				{
					IcarusCommandDeclare command = new IcarusCommandDeclare();
					int exprType = br.ReadInt32();            // 6

					int n5 = br.ReadInt32();            // 4
					short n6 = br.ReadInt16();          // 0

					command.DataType = (IcarusVariableDataType)br.ReadInt16();
					int n8 = br.ReadInt32();            // 4
					command.VariableName = ReadIBIString(br);
					return command;
				}
				case IcarusCommandType.Do:
				{
					IcarusCommandDo command = new IcarusCommandDo();
					command.Target = ReadIBIExpression(br);
					return command;
				}
				case IcarusCommandType.Flush:
				{
					return new IcarusCommandFlush();
				}
				case IcarusCommandType.Use:
				{
					IcarusCommandUse command = new IcarusCommandUse();
					command.Target = ReadIBIExpression(br);
					return command;
				}
				case IcarusCommandType.Run:
				{
					IcarusCommandRun command = new IcarusCommandRun();
					command.Target = ReadIBIExpression(br);
					return command;
				}
				case IcarusCommandType.Kill:
				{
					IcarusCommandKill kill = new IcarusCommandKill();
					kill.Target = ReadIBIExpression(br);
					return kill;
				}
				case IcarusCommandType.Print:
				{
					IcarusCommandPrint command = new IcarusCommandPrint();
					command.Content = ReadIBIExpression(br);
					return command;
				}
				case IcarusCommandType.Set:
				{
					IcarusCommandSet command = new IcarusCommandSet();

					command.Property = ReadIBIExpression(br);
					command.Value = ReadIBIExpression(br);

					return command;
				}
				case IcarusCommandType.Signal:
				{
					IcarusCommandSignal command = new IcarusCommandSignal();
					int exprType = br.ReadInt32();            // 6
					command.Target = ReadIBIString(br);
					return command;
				}
				case IcarusCommandType.WaitSignal:
				{
					IcarusCommandWaitSignal command = new IcarusCommandWaitSignal();
					int exprType = br.ReadInt32();            // 6
					command.Target = ReadIBIString(br);
					return command;
				}
				case IcarusCommandType.Task:
				{
					IcarusCommandTask command = new IcarusCommandTask();
					int exprType = br.ReadInt32();            // 6
					command.TaskName = ReadIBIString(br);

					IcarusCommand child = null;
					while ((child = ReadCommand(br)) != null)
					{
						if (child.GetType() == typeof(IcarusCommandEnd))
						{
							break;
						}
						else
						{
							command.Commands.Add(child);
						}
					}
					return command;
				}
			}
			*/
		}

		private IcarusExpression ReadIBIExpression(Reader br)
		{
			IcarusExpressionType exprType = (IcarusExpressionType)br.ReadInt32();
			switch (exprType)
			{
				case (IcarusExpressionType)25:
				{
					byte[] unknowndata = br.ReadBytes(14);

					break;
				}
				case IcarusExpressionType.Vector:
				{
					int exprSize = br.ReadInt32();
					float unknown = br.ReadSingle();

					IcarusExpression[] values = new IcarusExpression[3];
					for (int i = 0; i < 3; i++)
					{
						values[i] = ReadIBIExpression(br);
					}
					return new IcarusExpression(IcarusExpressionType.Vector, values);
				}
				case IcarusExpressionType.Float:
				{
					int exprSize = br.ReadInt32();
					float value = br.ReadSingle();
					return new IcarusExpression(IcarusExpressionType.Float, value);
				}
				case IcarusExpressionType.String:
				case IcarusExpressionType.Enumeration:
				{
					string value = ReadIBIString(br);
					return new IcarusExpression(IcarusExpressionType.String, value);
				}
				case IcarusExpressionType.Get:
				{
					int exprSize = br.ReadInt32();
					float unknown = br.ReadSingle();
					IcarusExpression expr = ReadIBIExpression(br);
					IcarusExpression expr2 = ReadIBIExpression(br);

					IcarusExpressionType exprType1 = (IcarusExpressionType)((float)expr.Value);
					return new IcarusExpression(exprType1, expr2.Value);
				}
			}
			return null;
		}

		private string ReadIBIString(Reader br)
		{
			int nIDSize = br.ReadInt32(); // size of string with nul terminator
			string value = br.ReadNullTerminatedString(nIDSize - 1);
			return value;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
