using UnityEngine;
using UnityEditor;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public abstract class AssetDefinition : ScriptableObject
	{
		#region Fields
		[SerializeField]
		protected AssetFilters m_Filters;
		[SerializeField]
		protected AssetDefinition m_Template;

		public AssetDefinition Template
		{
			get
			{
				return m_Template;
			}
		}

		public abstract AssetSettings Settings
		{
			get;
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		/// <param name="category"></param>
		/// <param name="template"></param>
		public abstract void OnCreate (string name, string type, AssetDefinition template);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="assetImporter"></param>
		/// <param name="assetPath"></param>
		public abstract void OnPreprocess (AssetImporter assetImporter);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="assetPath"></param>
		/// <returns></returns>
		public int CalculateRating (string assetPath)
		{
			int rating = 0;
			string filters = (m_Filters.TypeFilter.State ? m_Filters.TypeFilter : m_Template.m_Filters.TypeFilter) + "," +
							 (m_Filters.NameFilter.State ? m_Filters.NameFilter : m_Template.m_Filters.NameFilter) + "," +
							 (m_Filters.PathFilter.State ? m_Filters.PathFilter : m_Template.m_Filters.PathFilter);

			foreach (string filter in filters.Split (new char [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (!assetPath.Contains (filter))
				{
					return -1;
				}

				rating++;
			}

			return rating;
		}
	}
}