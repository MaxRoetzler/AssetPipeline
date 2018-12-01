using UnityEditor;
using UnityEngine;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public class AssetPipeline : AssetPostprocessor
	{
		private static AssetPipelineData s_PipelineData;

		#region Properties
		public static AssetPipelineData Data
		{
			get
			{
				return s_PipelineData;
			}
		}
		#endregion

		#region PreProcessors
		private void OnPreprocessAudio ()
		{
			s_PipelineData.GetByRating (s_PipelineData.AudioDefinitions, assetPath).OnPreprocess (assetImporter);
		}

		private void OnPreprocessModel ()
		{
			s_PipelineData.GetByRating (s_PipelineData.ModelDefinitions, assetPath).OnPreprocess (assetImporter);
		}

		private void OnPreprocessTexture ()
		{
			s_PipelineData.GetByRating (s_PipelineData.TextureDefinitions, assetPath).OnPreprocess (assetImporter);
		}
		#endregion

		[InitializeOnLoadMethod]
		static void OnProjectLoadedInEditor ()
		{
			s_PipelineData = EditorExtensions.LoadAsset<AssetPipelineData> ();

			if (s_PipelineData == null)
			{
				string assetPath = EditorExtensions.Search ("t:Folder AssetPipeline");
				assetPath = (assetPath == string.Empty) ? "Assets/AssetPipelineData.asset" : assetPath + "/AssetPipelineData.asset";

				s_PipelineData = EditorExtensions.CreateAsset<AssetPipelineData> (assetPath, true);
				s_PipelineData.Reset ();
			}
		}
	}
}