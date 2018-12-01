using MaxRoetzler.Extensions;
using UnityEditor;
using UnityEngine;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public sealed class TextureSettings : AssetSettings
	{
		#region Fields
		[SerializeField] private ParameterInt m_TextureType;
		[SerializeField] private ParameterInt m_TextureShape;
		[SerializeField] private ParameterBool m_SRGBTexture;
		[SerializeField] private ParameterInt m_AlphaSource;
		[SerializeField] private ParameterBool m_AlphaIsTransparency;
		[SerializeField] private ParameterInt m_NpotScale;
		[SerializeField] private ParameterBool m_IsReadable;
		[SerializeField] private ParameterBool m_MipmapEnabled;
		[SerializeField] private ParameterBool m_BorderMipmap;
		[SerializeField] private ParameterInt m_MipmapFilter;
		[SerializeField] private ParameterBool m_MipMapsPreserveCoverage;
		[SerializeField] private ParameterBool m_Fadeout;
		[SerializeField] private ParameterInt m_MipmapFadeDistanceStart;
		[SerializeField] private ParameterInt m_MipmapFadeDistanceEnd;
		[SerializeField] private ParameterBool m_ConvertToNormalmap;
		[SerializeField] private ParameterInt m_SpriteImportMode;
		[SerializeField] private ParameterString m_SpritePackingTag;
		[SerializeField] private ParameterInt m_SpritePixelsPerUnit;
		[SerializeField] private ParameterInt m_GenerateCubemap;

		[SerializeField] private ParameterInt m_WrapMode;
		[SerializeField] private ParameterInt m_FilterMode;
		[SerializeField] private ParameterInt m_AnisoLevel;

		#endregion

		/// <summary>
		/// Reset all texture settings.
		/// </summary>
		public override void Reset ()
		{
			m_TextureType = 0;
			m_TextureShape = 0;
			m_SRGBTexture = true;
			m_AlphaSource = 0;
			m_AlphaIsTransparency = false;
			m_NpotScale = 1;
			m_IsReadable = false;
			m_MipmapEnabled = true;
			m_BorderMipmap = true;
			m_MipmapFilter = 0;
			m_MipMapsPreserveCoverage = false;
			m_Fadeout = false;
			m_MipmapFadeDistanceStart = 2;
			m_MipmapFadeDistanceEnd = 5;
			m_ConvertToNormalmap = false;
			m_SpriteImportMode = 1;
			m_SpritePackingTag = string.Empty;
			m_SpritePixelsPerUnit = 100;
			m_GenerateCubemap = 6;
			m_WrapMode = 0;
			m_FilterMode = 1;
			m_AnisoLevel = 1;
		}

		/// <summary>
		/// Apply the texture settings to the specified AssetImporter.
		/// </summary>
		/// <param name="assetImporter">The asset importer.</param>
		public override void Apply (AssetImporter assetImporter, AssetSettings assetSettings)
		{
			TextureImporter importer = (TextureImporter) assetImporter;
			TextureSettings settings = (TextureSettings) assetSettings;

			// General
			importer.textureType = (TextureImporterType)(int) (m_TextureType.State ? m_TextureType : settings.m_TextureType);
			importer.textureShape = (TextureImporterShape)(int) (m_TextureShape.State ? m_TextureShape : settings.m_TextureShape);
			importer.sRGBTexture = m_SRGBTexture.State ? m_SRGBTexture : settings.m_SRGBTexture;
			importer.alphaSource = (TextureImporterAlphaSource)(int) (m_AlphaSource.State ? m_AlphaSource : settings.m_AlphaSource);
			importer.alphaIsTransparency = m_AlphaIsTransparency.State ? m_AlphaIsTransparency : settings.m_AlphaIsTransparency;

			// Advanced
			importer.npotScale = (TextureImporterNPOTScale)(int) (m_NpotScale.State ? m_NpotScale : settings.m_NpotScale);
			importer.isReadable = m_IsReadable.State ? m_IsReadable : settings.m_IsReadable;
			importer.wrapMode = (TextureWrapMode)(int) (m_WrapMode.State ? m_WrapMode : settings.m_WrapMode);
			importer.filterMode = (FilterMode)(int) (m_FilterMode.State ? m_FilterMode : settings.m_FilterMode);
			importer.anisoLevel = m_AnisoLevel.State ? m_AnisoLevel : settings.m_AnisoLevel;

			//importer.maxTextureSize

			// Mip Maps
			importer.mipmapEnabled = m_MipmapEnabled.State ? m_MipmapEnabled : settings.m_MipmapEnabled;
			importer.borderMipmap = m_BorderMipmap.State ? m_BorderMipmap : settings.m_BorderMipmap;
			importer.mipmapFilter = (TextureImporterMipFilter)(int) (m_MipmapFilter.State ? m_MipmapFilter : settings.m_MipmapFilter);
			importer.mipMapsPreserveCoverage = m_MipMapsPreserveCoverage.State ? m_MipMapsPreserveCoverage : settings.m_MipMapsPreserveCoverage;
			importer.mipmapFadeDistanceStart = m_MipmapFadeDistanceStart.State ? m_MipmapFadeDistanceStart : settings.m_MipmapFadeDistanceStart;
			importer.mipmapFadeDistanceEnd = m_MipmapFadeDistanceEnd.State ? m_MipmapFadeDistanceEnd : settings.m_MipmapFadeDistanceEnd;
			importer.fadeout = m_Fadeout.State ? m_Fadeout : settings.m_Fadeout;

			// Normal Map
			importer.convertToNormalmap = m_ConvertToNormalmap.State ? m_ConvertToNormalmap : settings.m_ConvertToNormalmap;

			// Sprite
			importer.spriteImportMode = (SpriteImportMode)(int) (m_SpriteImportMode.State ? m_SpriteImportMode : settings.m_SpriteImportMode);
			importer.spritePackingTag = m_SpritePackingTag.State ? m_SpritePackingTag : settings.m_SpritePackingTag;
			importer.spritePixelsPerUnit = m_SpritePixelsPerUnit.State ? m_SpritePixelsPerUnit : settings.m_SpritePixelsPerUnit;

			// Cubemap
			importer.generateCubemap = (TextureImporterGenerateCubemap)(int) (m_GenerateCubemap.State ? m_GenerateCubemap : settings.m_GenerateCubemap);
		}
	}
}