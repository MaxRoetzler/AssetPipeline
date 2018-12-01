using UnityEngine;
using System;
using UnityEditor;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public sealed class ModelDefinition : AssetDefinition
	{
		[SerializeField]
		private ModelSettings m_Settings;

		public override AssetSettings Settings
		{
			get
			{
				return m_Settings;
			}
		}

		public override void OnCreate (string name, string type, AssetDefinition template)
		{
			this.name = name;

			m_Template = template;
			m_Settings = new ModelSettings ();
			m_Filters = new AssetFilters (type);
		}

		public override void OnPreprocess (AssetImporter assetImporter)
		{
			m_Settings.Apply (assetImporter, m_Template.Settings);
		}
	}
}