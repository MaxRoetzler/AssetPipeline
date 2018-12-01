using MaxRoetzler.Extensions;
using UnityEditor;
using UnityEngine;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public sealed class ModelSettings : AssetSettings
	{
		#region Fields
		// Mesh settings
		[SerializeField] private ParameterBool m_KeepQuads;
		[SerializeField] private ParameterBool m_IsReadable;
		[SerializeField] private ParameterBool m_OptmizeMesh;
		[SerializeField] private ParameterBool m_WeldVertices;
		[SerializeField] private ParameterBool m_ImportLights;
		[SerializeField] private ParameterBool m_UseFileScale;
		[SerializeField] private ParameterBool m_ImportCameras;
		[SerializeField] private ParameterBool m_SwapUVChannels;
		[SerializeField] private ParameterBool m_ImportVisibility;
		[SerializeField] private ParameterBool m_ImportBlendShapes;
		[SerializeField] private ParameterBool m_GenerateColliders;
		[SerializeField] private ParameterBool m_PreserveHierarchy;
		[SerializeField] private ParameterInt m_MeshCompression;
		[SerializeField] private ParameterInt m_IndexFormat;
		[SerializeField] private ParameterFloat m_ScaleFactor;
		[SerializeField] private ParameterBool m_GenerateSecondaryUV;
		[SerializeField] private ParameterFloat m_HardAngle;
		[SerializeField] private ParameterFloat m_PackMargin;
		[SerializeField] private ParameterFloat m_AngleDistortion;
		[SerializeField] private ParameterFloat m_AreaDistortion;
		[SerializeField] private ParameterInt m_ImportNormals;
		[SerializeField] private ParameterInt m_ImportTangents;
		[SerializeField] private ParameterInt m_NormalCalculationMode;
		[SerializeField] private ParameterFloat m_NormalSmoothingAngle;

		// Animation
		[SerializeField] private ParameterInt m_AnimationType;
		[SerializeField] private ParameterInt m_GenerateAnimations;
		[SerializeField] private ParameterInt m_AnimationCompression;
		[SerializeField] private ParameterBool m_BakeIK;
		[SerializeField] private ParameterBool m_ImportAnimation;
		[SerializeField] private ParameterBool m_ImportConstraints;

		// Material
		[SerializeField] private ParameterBool m_ImportMaterials;
		[SerializeField] private ParameterInt m_MaterialName;
		[SerializeField] private ParameterInt m_MaterialSearch;
		[SerializeField] private ParameterInt m_MaterialLocation;
		#endregion

		public override void Reset ()
		{
			// Meshes tab
			m_ScaleFactor = 1f;
			m_UseFileScale = true;
			m_MeshCompression = (int) ModelImporterMeshCompression.Off;
			m_IsReadable = false;
			m_OptmizeMesh = true;
			m_ImportBlendShapes = false;
			m_GenerateColliders = false;
			m_KeepQuads = false;
			m_IndexFormat = (int) ModelImporterIndexFormat.Auto;
			m_WeldVertices = true;
			m_ImportVisibility = false;
			m_ImportCameras = false;
			m_ImportLights = false;
			m_PreserveHierarchy = false;
			m_SwapUVChannels = false;
			m_GenerateSecondaryUV = false;
			m_HardAngle = 88f;
			m_PackMargin = 4;
			m_AngleDistortion = 8;
			m_AreaDistortion = 15;
			m_ImportNormals = (int) ModelImporterNormals.Import;
			m_NormalSmoothingAngle = 0;
			m_NormalCalculationMode = (int) ModelImporterNormalCalculationMode.Unweighted;
			m_ImportTangents = (int) ModelImporterTangents.CalculateMikk;

			// Animation & Rig tab
			m_ImportAnimation = false;
			m_ImportConstraints = false;
			m_AnimationType = (int) ModelImporterAnimationType.None;

			// Materials tab
			m_ImportMaterials = false;
			m_MaterialLocation = (int) ModelImporterMaterialLocation.InPrefab;
			m_MaterialName = (int) ModelImporterMaterialName.BasedOnTextureName;
			m_MaterialSearch = (int) ModelImporterMaterialSearch.Local;
		}

		public override void Apply (AssetImporter assetImporter, AssetSettings assetSettings)
		{
			ModelImporter importer = (ModelImporter) assetImporter;
			ModelSettings settings = (ModelSettings) assetSettings;

			importer.keepQuads = m_KeepQuads.State ? m_KeepQuads : settings.m_KeepQuads;
			importer.isReadable = m_IsReadable.State ? m_IsReadable : settings.m_IsReadable;
			importer.globalScale = m_ScaleFactor.State ? m_ScaleFactor : settings.m_ScaleFactor;
			importer.optimizeMesh = m_OptmizeMesh.State ? m_OptmizeMesh : settings.m_OptmizeMesh;
			importer.useFileScale = m_UseFileScale.State ? m_UseFileScale : settings.m_UseFileScale;
			importer.weldVertices = m_WeldVertices.State ? m_WeldVertices : settings.m_WeldVertices;
			importer.importLights = m_ImportLights.State ? m_ImportLights : settings.m_ImportLights;
			importer.importCameras = m_ImportCameras.State ? m_ImportCameras : settings.m_ImportCameras;
			importer.swapUVChannels = m_SwapUVChannels.State ? m_SwapUVChannels : settings.m_SwapUVChannels;
			importer.addCollider = m_GenerateColliders.State ? m_GenerateColliders : settings.m_GenerateColliders;
			importer.importVisibility = m_ImportVisibility.State ? m_ImportVisibility : settings.m_ImportVisibility;
			importer.importBlendShapes = m_ImportBlendShapes.State ? m_ImportBlendShapes : settings.m_ImportBlendShapes;
			importer.preserveHierarchy = m_PreserveHierarchy.State ? m_PreserveHierarchy : settings.m_PreserveHierarchy;
			importer.indexFormat = (ModelImporterIndexFormat)(int) (m_IndexFormat.State ? m_IndexFormat : settings.m_IndexFormat);
			importer.meshCompression = (ModelImporterMeshCompression)(int) (m_MeshCompression.State ? m_MeshCompression : settings.m_MeshCompression);
			importer.generateSecondaryUV = m_GenerateSecondaryUV.State ? m_GenerateSecondaryUV : settings.m_GenerateSecondaryUV;
			importer.secondaryUVAngleDistortion = m_AngleDistortion.State ? m_AngleDistortion : settings.m_AngleDistortion;
			importer.secondaryUVAreaDistortion = m_AreaDistortion.State ? m_AreaDistortion : settings.m_AreaDistortion;
			importer.secondaryUVHardAngle = m_HardAngle.State ? m_HardAngle : settings.m_HardAngle;
			importer.secondaryUVPackMargin = m_PackMargin.State ? m_PackMargin : settings.m_PackMargin;

			// Material
			importer.importMaterials = m_ImportMaterials.State ? m_ImportMaterials : settings.m_ImportMaterials;
			importer.materialLocation = (ModelImporterMaterialLocation) (int) (m_MaterialLocation.State ? m_MaterialLocation : settings.m_MaterialLocation);
			importer.materialName = (ModelImporterMaterialName) (int) (m_MaterialName.State ? m_MaterialName : settings.m_MaterialName);
			importer.materialSearch = (ModelImporterMaterialSearch) (int) (m_MaterialSearch.State ? m_MaterialSearch : settings.m_MaterialSearch);

			// Normals & Tangents
			importer.importNormals = (ModelImporterNormals)(int) (m_ImportNormals.State ? m_ImportNormals : settings.m_ImportNormals);
			importer.normalCalculationMode = (ModelImporterNormalCalculationMode)(int) (m_NormalCalculationMode.State ? m_NormalCalculationMode : settings.m_NormalCalculationMode);
			importer.normalSmoothingAngle = m_NormalSmoothingAngle.State ? m_NormalSmoothingAngle : settings.m_NormalSmoothingAngle;
			importer.importTangents = (ModelImporterTangents)(int) (m_ImportTangents.State ? m_ImportTangents : settings.m_ImportTangents);

			// Rig & Animations
			importer.animationType = (ModelImporterAnimationType) (int) (m_AnimationType.State ? m_AnimationType : settings.m_AnimationType);
			importer.importAnimation = m_ImportAnimation.State ? m_ImportAnimation : settings.m_ImportAnimation;
			importer.importConstraints = m_ImportConstraints.State ? m_ImportConstraints.Value : settings.m_ImportConstraints.Value;
			importer.generateAnimations = (ModelImporterGenerateAnimations) (int) (m_GenerateAnimations.State ? m_GenerateAnimations : settings.m_GenerateAnimations);
			importer.bakeIK = m_BakeIK.State ? m_BakeIK : settings.m_BakeIK;
			importer.animationCompression = (ModelImporterAnimationCompression) (int) (m_AnimationCompression.State ? m_AnimationCompression : settings.m_AnimationCompression);
		}
	}
}