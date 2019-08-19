using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.DataFormats.Package.OpenPackagingConvention;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
using UniversalEditor.ObjectModels.Package;

namespace UniversalEditor.TestProject
{
	class MainClass
	{

		class OPCModelDataFormat : OPCDataFormat
		{
			private static DataFormatReference _dfr = null;
			protected override DataFormatReference MakeReferenceInternal()
			{
				if (_dfr == null)
				{
					_dfr = new DataFormatReference(this.GetType());
					_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				}
				return _dfr;
			}
			protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
			{
				base.BeforeLoadInternal(objectModels);
				objectModels.Push(new PackageObjectModel());
			}
			protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
			{
				base.AfterLoadInternal(objectModels);

				PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
				ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
			}

			protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
			{
				ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
				PackageObjectModel package = new PackageObjectModel();

				MarkupObjectModel momDocument = CreateDocument(model);

				File file = package.FileSystem.AddFile("model/document.xml");
				file.SetObjectModel<MarkupObjectModel>(new XMLDataFormat(), momDocument);

				objectModels.Push(package);

				base.BeforeSaveInternal(objectModels);
			}

			private MarkupObjectModel CreateDocument(ModelObjectModel model)
			{
				MarkupObjectModel momDocument = new MarkupObjectModel();
				MarkupTagElement tagModel = new MarkupTagElement();
				tagModel.FullName = "cr:model";
				tagModel.Attributes.Add("xmlns:cr", "urn:net.alcetech.schemas.Concertroid.OPC.Model");

				MarkupTagElement tagSettings = new MarkupTagElement();
				tagSettings.FullName = "cr:settings";

				MarkupTagElement tagSettingIgnoreEdgeFlag = new MarkupTagElement();
				tagSettingIgnoreEdgeFlag.FullName = "cr:setting";
				tagSettingIgnoreEdgeFlag.Attributes.Add("name", "ignoreEdgeFlag");
				tagSettingIgnoreEdgeFlag.Attributes.Add("value", model.IgnoreEdgeFlag ? "true" : "false");

				tagModel.Elements.Add(tagSettings);

				MarkupTagElement tagBones = new MarkupTagElement();
				tagBones.FullName = "cr:bones";
				foreach (ModelBone bone in model.Bones)
				{
					MarkupTagElement tagBone = new MarkupTagElement();
					tagBone.FullName = "cr:bone";
					tagBone.Attributes.Add("id", "b" + model.Bones.IndexOf(bone).ToString());

					MarkupTagElement tagAngleLimit = new MarkupTagElement();
					tagAngleLimit.FullName = "cr:angleLimit";
					tagAngleLimit.Attributes.Add("enabled", bone.AngleLimit.Enabled ? "true" : "false");
					tagAngleLimit.Elements.Add(bone.AngleLimit.Lower.ToXML("cr:lower"));
					tagAngleLimit.Elements.Add(bone.AngleLimit.Upper.ToXML("cr:upper"));
					tagBone.Elements.Add(tagAngleLimit);

					tagBone.Attributes.Add("type", BoneTypeToXML(bone.BoneType));
					tagBone.Attributes.Add("childId", "b" + model.Bones.IndexOf(bone.ChildBone).ToString());
					tagBone.Attributes.Add("ikNumber", bone.IKNumber.ToString());
					tagBone.Attributes.Add("name", bone.Name);
					tagBone.Attributes.Add("parentId", "b" + model.Bones.IndexOf(bone.ParentBone).ToString());

					tagBone.Elements.Add(bone.Position.ToXML("cr:position"));
					tagBone.Elements.Add(bone.Rotation.ToXML("cr:rotation"));
					tagBone.Elements.Add(bone.Vector3Offset.ToXML("cr:offset"));

					tagBones.Elements.Add(tagBone);
				}
				tagModel.Elements.Add(tagBones);

				MarkupTagElement tagExpressions = new MarkupTagElement();
				tagExpressions.FullName = "cr:expressions";
				foreach (ushort u in model.Expressions)
				{
					MarkupTagElement tagExpression = new MarkupTagElement();
					tagExpression.FullName = "cr:expression";
					tagExpression.Attributes.Add("value", u.ToString());
					tagExpressions.Elements.Add(tagExpression);
				}
				tagModel.Elements.Add(tagExpressions);

				MarkupTagElement tagJoints = new MarkupTagElement();
				tagJoints.FullName = "cr:joints";
				foreach (ModelJoint joint in model.Joints)
				{
					MarkupTagElement tagJoint = new MarkupTagElement();
					tagJoint.FullName = "cr:joint";

					MarkupTagElement tagLimits = new MarkupTagElement();
					tagLimits.FullName = "cr:limits";
					tagLimits.Elements.Add(joint.LimitAngleHigh.ToXML("cr:angleHigh"));
					tagLimits.Elements.Add(joint.LimitAngleLow.ToXML("cr:angleLow"));
					tagLimits.Elements.Add(joint.LimitMoveHigh.ToXML("cr:moveHigh"));
					tagLimits.Elements.Add(joint.LimitMoveLow.ToXML("cr:moveLow"));
					tagJoint.Elements.Add(tagLimits);

					tagJoint.Attributes.Add("name", joint.Name);
					tagJoint.Elements.Add(joint.Position.ToXML("cr:position"));
					tagJoint.Elements.Add(joint.Rotation.ToXML("cr:rotation"));

					MarkupTagElement tagSpringConstraint = new MarkupTagElement();
					tagSpringConstraint.FullName = "cr:springConstraint";
					tagSpringConstraint.Elements.Add(joint.SpringConstraintMovementStiffness.ToXML("cr:movementStiffness"));
					tagSpringConstraint.Elements.Add(joint.SpringConstraintRotationStiffness.ToXML("cr:rotationStiffness"));
					tagJoint.Elements.Add(tagSpringConstraint);

					tagJoints.Elements.Add(tagJoint);
				}
				tagModel.Elements.Add(tagJoints);

				MarkupTagElement tagIKHandles = new MarkupTagElement();
				tagIKHandles.FullName = "cr:ikHandles";
				foreach (ModelIK ik in model.IK)
				{
					MarkupTagElement tagIKHandle = new MarkupTagElement();
					tagIKHandle.FullName = "cr:ikHandle";

					tagIKHandle.Attributes.Add("effectedBoneId", model.Bones.IndexOf(ik.EffBone).ToString());
					tagIKHandle.Attributes.Add("index", ik.Index.ToString());
					tagIKHandle.Attributes.Add("limitOnce", ik.LimitOnce.ToString());
					tagIKHandle.Attributes.Add("loopCount", ik.LoopCount.ToString());
					tagIKHandle.Attributes.Add("targetBoneId", model.Bones.IndexOf(ik.TargetBone).ToString());

					MarkupTagElement tagBoneList = new MarkupTagElement();
					tagBoneList.FullName = "cr:boneList";
					foreach (ModelBone bone in ik.BoneList)
					{
						MarkupTagElement tagBone = new MarkupTagElement();
						tagBone.FullName = "cr:boneReference";
						tagBone.Attributes.Add("boneId", model.Bones.IndexOf(bone).ToString());
						tagBoneList.Elements.Add(tagBone);
					}
					tagIKHandle.Elements.Add(tagBoneList);
				}
				tagModel.Elements.Add(tagIKHandles);

				MarkupTagElement tagSurfaces = new MarkupTagElement();
				tagSurfaces.FullName = "cr:surfaces";
				foreach (ModelSurface surf in model.Surfaces)
				{
					MarkupTagElement tagSurface = new MarkupTagElement();
					tagSurface.FullName = "cr:surface";

					MarkupTagElement tagTriangles = new MarkupTagElement();
					tagTriangles.FullName = "cr:triangles";
					foreach (ModelTriangle tri in surf.Triangles)
					{
						MarkupTagElement tagTriangle = new MarkupTagElement();
						tagTriangle.FullName = "cr:triangle";

						tagTriangle.Elements.Add(tri.Vertex1.ToXML("cr:vertex"));
						tagTriangle.Elements.Add(tri.Vertex2.ToXML("cr:vertex"));
						tagTriangle.Elements.Add(tri.Vertex3.ToXML("cr:vertex"));

						tagTriangles.Elements.Add(tagTriangle);
					}
					tagSurface.Elements.Add(tagTriangles);

					MarkupTagElement tagVertices = new MarkupTagElement();
					tagVertices.FullName = "cr:vertices";
					foreach (ModelVertex vtx in surf.Vertices)
					{
						tagVertices.Elements.Add(vtx.ToXML("cr:vertex"));
					}
					tagSurface.Elements.Add(tagVertices);

					tagSurfaces.Elements.Add(tagSurface);
				}
				tagModel.Elements.Add(tagSurfaces);

				MarkupTagElement tagRigidBodies = new MarkupTagElement();
				tagRigidBodies.FullName = "cr:rigidBodies";

				foreach (ModelRigidBody rb in model.RigidBodies)
				{
					MarkupTagElement tagRigidBody = new MarkupTagElement();
					tagRigidBody.FullName = "cr:rigidBody";

					tagRigidBody.Attributes.Add("boneId", "b" + model.Bones.IndexOf(rb.Bone).ToString());
					tagRigidBody.Elements.Add(rb.BoxSize.ToXML("cr:boxSize"));
					tagRigidBody.Attributes.Add("boxType", rb.BoxType.ToString());

					tagRigidBody.Attributes.Add("friction", rb.Friction.ToString());
					tagRigidBody.Attributes.Add("groupId", rb.GroupID.ToString());
					tagRigidBody.Attributes.Add("itype", rb.IType.ToString());
					tagRigidBody.Attributes.Add("mass", rb.Mass.ToString());
					tagRigidBody.Attributes.Add("mode", rb.Mode.ToString());
					tagRigidBody.Attributes.Add("name", rb.Name);
					tagRigidBody.Elements.Add(rb.Position.ToXML("cr:position"));
					tagRigidBody.Attributes.Add("positionDamping", rb.PositionDamping.ToString());
					tagRigidBody.Attributes.Add("restitution", rb.Restitution.ToString());
					tagRigidBody.Elements.Add(rb.Rotation.ToXML("cr:rotation"));
					tagRigidBody.Attributes.Add("rotationDamping", rb.RotationDamping.ToString());

					tagRigidBodies.Elements.Add(tagRigidBody);
				}

				tagModel.Elements.Add(tagRigidBodies);

				momDocument.Elements.Add(tagModel);
				return momDocument;
			}

			private string BoneTypeToXML(ModelBoneType boneType)
			{
				switch (boneType)
				{
					case ModelBoneType.Blank: return "blank";
					case ModelBoneType.Hidden: return "hidden";
					case ModelBoneType.IKConnect: return "inverseKinematicsConnect";
					case ModelBoneType.IKInfluencedRotation: return "inverseKinematicsInfluencedRotation";
					case ModelBoneType.InfluencedRotation: return "influencedRotation";
					case ModelBoneType.InverseKinematics: return "inverseKinematics";
					case ModelBoneType.Revolution: return "revolution";
					case ModelBoneType.Rotate: return "rotate";
					case ModelBoneType.RotateMove: return "rotateMove";
					case ModelBoneType.Twist: return "twist";
					case ModelBoneType.Unknown: return "unknown";
				}
				return String.Empty;
			}
		}

		public static void Main (string [] args)
		{
			/*
			string fileName = "/tmp/UETest/test.opc";

			PackageObjectModel om = new PackageObjectModel ();
			OPCDataFormat df = new OPCDataFormat ();

			Document.Save (om, df, new FileAccessor (fileName, true, true));
			*/

			DateTime start = DateTime.Now;

			ModelObjectModel model = new ModelObjectModel();
			UniversalEditor.DataFormats.Multimedia3D.Model.PolygonMovieMaker.PMDModelDataFormat pmd = new DataFormats.Multimedia3D.Model.PolygonMovieMaker.PMDModelDataFormat();

			Document.Load(model, pmd, new FileAccessor("/home/beckermj/Documents/UE Tests/Open Packaging Convention/Concertroid Model Data OPC (.pmdx)/test.pmdx_source/model/kio_miku_20111121.pmd"));
			Document.Save(model, new OPCModelDataFormat(), new FileAccessor("/home/beckermj/Documents/UE Tests/Open Packaging Convention/Concertroid Model Data OPC (.pmdx)/kio_miku.pmdx", true, true));

			DateTime end = DateTime.Now;
			Console.WriteLine("Took " + (end - start).ToString() + " with buffer size " + MemoryAccessor.DefaultBufferAllocationSize);
		}
	}
}
