using UnityEngine;
using UnityEditor;
using MaxRoetzler.Extensions;
using System.Collections.Generic;

namespace MaxRoetzler.AssetPipeline
{
	public class AssetPipelineData : ScriptableObject
	{
		[SerializeField]
		private List<ModelDefinition> m_ModelDefinitions;
		[SerializeField]
		private List<TextureDefinition> m_TextureDefinitions;
		[SerializeField]
		private List<AudioDefinition> m_AudioDefinitions;

		#region Getters
		public List<AudioDefinition> AudioDefinitions
		{
			get
			{
				return m_AudioDefinitions;
			}
		}

		public List<ModelDefinition> ModelDefinitions
		{
			get
			{
				return m_ModelDefinitions;
			}
		}

		public List<TextureDefinition> TextureDefinitions
		{
			get
			{
				return m_TextureDefinitions;
			}
		}
		#endregion

		#region Public Methods
		public void Reset ()
		{
			EditorExtensions.DeleteSubAssets (this);

			m_AudioDefinitions = new List<AudioDefinition> { CreateDefinition (m_AudioDefinitions, true) };
			m_ModelDefinitions = new List<ModelDefinition> { CreateDefinition (m_ModelDefinitions, true) };
			m_TextureDefinitions = new List<TextureDefinition> { CreateDefinition (m_TextureDefinitions, true) };
		}

		public T CreateDefinition<T> (List <T> list, bool isDefault = false) where T : AssetDefinition
		{
			T definition = CreateInstance<T> ();

			if (isDefault)
			{
				definition.OnCreate ("Default", ".*", definition);
			}
			else
			{
				definition.OnCreate ("New Definition", ".*", list[0]);
			}

			AssetDatabase.AddObjectToAsset (definition, this);
			EditorUtility.SetDirty (this);

			return definition;
		}

		public T GetByRating<T> (List<T> list, string assetPath) where T : AssetDefinition
		{
			int maxRating = 0;
			T result = list [0];

			foreach (T item in list)
			{
				int rating = item.CalculateRating (assetPath);

				if (rating > maxRating)
				{
					result = item;
					maxRating = rating;
				}
			}

			return result;
		}
		#endregion
	}
}