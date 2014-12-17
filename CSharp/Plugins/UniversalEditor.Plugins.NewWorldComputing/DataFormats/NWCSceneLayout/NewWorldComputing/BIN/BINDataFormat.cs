using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.NWCSceneLayout;
using UniversalEditor.ObjectModels.NWCSceneLayout.SceneObjects;

namespace UniversalEditor.DataFormats.NWCSceneLayout.NewWorldComputing.BIN
{
    public class BINDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(NWCSceneLayoutObjectModel), DataFormatCapabilities.All);
			}
            return _dfr;
        }

        // TODO: fix the weird ones (DATAENTR.BIN, CASLWIND.BIN, etc.) and implement the motion ones (*FRM.BIN)

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            NWCSceneLayoutObjectModel scene = (objectModel as NWCSceneLayoutObjectModel);
            if (scene == null) throw new ObjectModelNotSupportedException();

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;
            scene.Objects.Clear();

            // TODO: Look at multiple BIN files and see what all the parts have in
            // common (i.e. strings, etc.)

            if (br.Accessor.Length < 6) throw new InvalidDataFormatException("Stream must be at least 6 bytes in length");

            ushort sceneWidth = br.ReadUInt16();
            ushort sceneHeight = br.ReadUInt16();

            sceneWidth -= 16;
            sceneHeight -= 16;
            scene.Width = sceneWidth;
            scene.Height = sceneHeight;

            bool screenLoaded = false;

            List<SceneObject> objects = new List<SceneObject>();

            BINContainerType containerType = (BINContainerType)br.ReadUInt16(); // 0x02
            if (!(containerType == BINContainerType.Motion || containerType == BINContainerType.Overlay || containerType == BINContainerType.Toplevel || containerType == BINContainerType.Window))
            {
                throw new InvalidDataFormatException("Container type " + containerType.ToString() + " not supported!");
            }

            switch (containerType)
            {
                case BINContainerType.Overlay:
                case BINContainerType.Toplevel:
                case BINContainerType.Window:
                {
                    while (!br.EndOfStream)
                    {
                        BINObjectType objectType = (BINObjectType)br.ReadUInt16();
                        switch (objectType)
                        {
                            case BINObjectType.Screen:
                            {
                                ushort left = br.ReadUInt16();
                                ushort top = br.ReadUInt16();
                                ushort width = br.ReadUInt16();
                                ushort height = br.ReadUInt16();
                                /*
                                if (containerType != BINContainerType.Overlay)
                                {
                                    ushort unknown1 = br.ReadUInt16();
                                    ushort unknown2 = br.ReadUInt16();
                                    string ICNFileName = br.ReadFixedLengthString(13); // file name of ICN file for window background
                                    ICNFileName = ICNFileName.TrimNull();
                                    scene.BackgroundImageFileName = ICNFileName;
                                }
                                */
                                break;
                            }
                            case BINObjectType.Background:
                            case BINObjectType.Background2:
                            {
                                ushort unknown1 = br.ReadUInt16();
                                string ICNFileName = br.ReadFixedLengthString(13); // file name of ICN file for window background
                                ICNFileName = ICNFileName.TrimNull();
                                scene.BackgroundImageFileName = ICNFileName;
                                break;
                            }
                            case BINObjectType.Unknown0x09:
                            case BINObjectType.Unknown0x0A:
                            case BINObjectType.Unknown0x0B:
                            case BINObjectType.Unknown0x16:
                            case BINObjectType.Unknown0x19:
                            {
                                ushort unknown1 = br.ReadUInt16();
                                ushort unknown2 = br.ReadUInt16();
                                break;
                            }
                            case BINObjectType.Button:
                            {
                                SceneObjectButton obj = new SceneObjectButton();
                                obj.Left = br.ReadUInt16();
                                obj.Top = br.ReadUInt16();
                                obj.Width = br.ReadUInt16();
                                obj.Height = br.ReadUInt16();

                                /*
                                obj.Top += 15;
                                obj.Left -= 7;
                               	*/
                                obj.Left -= 16;

                                string ICNFileName = br.ReadFixedLengthString(13);
                                ICNFileName = ICNFileName.TrimNull();
                                obj.BackgroundImageFileName = ICNFileName;

                                ushort ICNIndex = br.ReadUInt16();
                                obj.BackgroundImageIndex = ICNIndex;

                                ushort unknown2 = br.ReadUInt16();

                                ushort unknown3 = br.ReadUInt16();
                                ushort unknown4 = br.ReadUInt16();
                                obj.DisplayIndex = br.ReadUInt16();
                                ushort unknown6 = br.ReadUInt16();
                                objects.Add(obj);
                                break;
                            }
                            case BINObjectType.Image:
                            {
                                long pos = br.Accessor.Position - 2;

                                // Contains UInt16 * 4 unknown (possibly left, top, width, height),
                                // FixedLengthString[13] file name of ICN file, UInt16 * 5 unknown.
                                SceneObjectImage obj = new SceneObjectImage();
                                obj.Left = br.ReadUInt16();
                                obj.Top = br.ReadUInt16();
                                obj.Width = br.ReadUInt16();
                                obj.Height = br.ReadUInt16();

                                obj.Left -= 6;
                                obj.Top -= 4;

                                string ICNFileName = br.ReadFixedLengthString(13);
                                ICNFileName = ICNFileName.TrimNull();
                                obj.BackgroundImageFileName = ICNFileName;
                                obj.BackgroundImageIndex = br.ReadUInt16();

                                ushort unknown1 = br.ReadUInt16();
                                ushort unknown2 = br.ReadUInt16();  // possibly dialog object index
                                obj.DisplayIndex = br.ReadUInt16();
                                ushort unknown4 = br.ReadUInt16();
                                objects.Add(obj);
                                break;
                            }
                            case BINObjectType.Label:
                            {
                                SceneObjectLabel obj = new SceneObjectLabel();
                                obj.Left = br.ReadUInt16();
                                obj.Top = br.ReadUInt16();
                                obj.Width = br.ReadUInt16();
                                obj.Height = br.ReadUInt16();

                                /*
                                obj.Left += 4;
                                obj.Top += 11;
                                */

                                ushort labelTextSize = br.ReadUInt16();
                                string labelText = br.ReadFixedLengthString(labelTextSize);
                                labelText = labelText.TrimNull();
                                obj.Text = labelText;

                                string FNTFileName = br.ReadFixedLengthString(13);
                                FNTFileName = FNTFileName.TrimNull();
                                obj.FontFileName = FNTFileName;

                                ushort unknown1 = br.ReadUInt16();
                                ushort unknown2 = br.ReadUInt16();
                                obj.DisplayIndex = br.ReadUInt16();
                                ushort unknown4 = br.ReadUInt16();
                                objects.Add(obj);
                                break;
                            }
                            case BINObjectType.EOF:
                            {
                                // no need to do anything here!
                                break;
                            }
                            default:
                            {

                                break;
                            }
                        }
                    }
                    break;
                }
            }

            objects.Sort();
            foreach (SceneObject obj in objects)
            {
                scene.Objects.Add(obj);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
        }
    }
}
