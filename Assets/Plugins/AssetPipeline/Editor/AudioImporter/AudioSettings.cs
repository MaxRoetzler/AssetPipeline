using MaxRoetzler.Extensions;
using UnityEditor;
using UnityEngine;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public sealed class AudioSettings : AssetSettings
	{
		#region Fields
		[SerializeField] private ParameterBool m_ForceToMono;
		[SerializeField] private ParameterBool m_LoadInBackground;
		[SerializeField] private ParameterBool m_Ambisonic;
		[SerializeField] private AudioSampleSettings [] m_SampleSettings;

		public AudioSettings ()
		{
			m_SampleSettings = new AudioSampleSettings [6];
		}
		#endregion

		public override void Reset ()
		{
			m_Ambisonic = false;
			m_ForceToMono = false;
			m_LoadInBackground = false;
			m_SampleSettings = new AudioSampleSettings [0];
		}

		public override void Apply (AssetImporter assetImporter, AssetSettings assetSettings)
		{
			AudioImporter importer = (AudioImporter) assetImporter;
			AudioSettings settings = (AudioSettings) assetSettings;
			AudioImporterSampleSettings sampleSettings = importer.defaultSampleSettings;

			importer.forceToMono = m_ForceToMono.State ? m_ForceToMono : settings.m_ForceToMono;
			importer.loadInBackground = m_LoadInBackground.State ? m_LoadInBackground : settings.m_LoadInBackground;
			importer.ambisonic = m_Ambisonic.State ? m_Ambisonic : settings.m_Ambisonic;

			importer.defaultSampleSettings = sampleSettings;
		}
	}
}