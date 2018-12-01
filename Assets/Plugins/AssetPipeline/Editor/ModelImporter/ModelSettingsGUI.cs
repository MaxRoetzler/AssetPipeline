using UnityEditor;
using UnityEngine;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public sealed class ModelSettingsGUI : AssetSettingsGUI
	{
		private bool m_AdvancedUVSettings;

		public ModelSettingsGUI (SerializedObject serializedObject, bool isDefault) : base (serializedObject, isDefault) { }

		public override void Draw ()
		{
			#region Meshes
			ParameterOverrideGUI.DrawTitle ("Meshes");
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ScaleFactor"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_UseFileScale"));
			ParameterOverrideGUI.DrawEnum	 (m_Iterator.Select ("m_MeshCompression"), typeof (ModelImporterMeshCompression));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_IsReadable"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_OptmizeMesh"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ImportBlendShapes"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_GenerateColliders"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_KeepQuads"));
			ParameterOverrideGUI.DrawEnum	 (m_Iterator.Select ("m_IndexFormat"), typeof (ModelImporterIndexFormat));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_WeldVertices"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ImportVisibility"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ImportCameras"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ImportLights"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_PreserveHierarchy"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_SwapUVChannels"));

			if (ParameterOverrideGUI.DrawToggle (m_Iterator.Select ("m_GenerateSecondaryUV")))
			{
				EditorGUI.indentLevel += 1;
				m_AdvancedUVSettings = EditorGUILayout.Foldout (m_AdvancedUVSettings, "Advanced");

				if (m_AdvancedUVSettings)
				{
					ParameterOverrideGUI.DrawSlider (m_Iterator.Select ("m_HardAngle"), 0, 180);
					ParameterOverrideGUI.DrawSlider (m_Iterator.Select ("m_PackMargin"), 1, 64);
					ParameterOverrideGUI.DrawSlider (m_Iterator.Select ("m_AngleDistortion"), 1, 75);
					ParameterOverrideGUI.DrawSlider (m_Iterator.Select ("m_AreaDistortion"), 1, 75);
				}

				EditorGUI.indentLevel -= 1;
			}
			#endregion

			#region Normals & Tangents
			ParameterOverrideGUI.DrawTitle ("Normals & Tangents");
			if (ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_ImportNormals"), typeof (ModelImporterNormals)) == 1)
			{
				EditorGUI.indentLevel += 1;
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_NormalCalculationMode"), typeof (ModelImporterNormalCalculationMode));
				ParameterOverrideGUI.DrawSlider (m_Iterator.Select ("m_NormalSmoothingAngle"), 0, 180);
				EditorGUI.indentLevel -= 1;
			}
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_ImportTangents"), typeof (ModelImporterTangents));
			#endregion

			#region Materials
			ParameterOverrideGUI.DrawTitle ("Materials");
			if (ParameterOverrideGUI.DrawToggle (m_Iterator.Select ("m_ImportMaterials")))
			{
				EditorGUI.indentLevel += 1;
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_MaterialName"), typeof (ModelImporterMaterialName));
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_MaterialSearch"), typeof (ModelImporterMaterialName));
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_MaterialLocation"), typeof (ModelImporterMaterialName));
				EditorGUI.indentLevel -= 1;
			}
			#endregion

			#region Animation
			ParameterOverrideGUI.DrawTitle ("Animation");
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_AnimationType"), typeof (ModelImporterAnimationType));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ImportConstraints"));

			if (ParameterOverrideGUI.DrawToggle (m_Iterator.Select ("m_ImportAnimation")))
			{
				EditorGUI.indentLevel += 1;
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_BakeIK"));
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_GenerateAnimations"), typeof (ModelImporterGenerateAnimations));
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_AnimationCompression"), typeof (ModelImporterAnimationCompression));
				EditorGUI.indentLevel -= 1;
			}
			#endregion
		}
	}
}
