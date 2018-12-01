using UnityEditor;
using UnityEngine;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public sealed class AudioSettingsGUI : AssetSettingsGUI
	{
		private static readonly string [] k_SampleRateStrings = new [] { "8,000 Hz", "11,025 Hz", "22,050 Hz", "44,100 Hz", "48,000 Hz", "96,000 Hz", "192,000 Hz" };
		private static readonly int [] k_SampleRateValues = new [] { 8000, 11025, 22050, 44100, 48000, 96000, 192000 };

		public AudioSettingsGUI (SerializedObject serializedObject, bool isDefault) : base (serializedObject, isDefault) { }

		public override void Draw ()
		{
			#region General Settings
			ParameterOverrideGUI.DrawTitle ("General Settings");
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_ForceToMono"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_LoadInBackground"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_Ambisonic"));
			#endregion

			#region Platform
			EditorGUILayout.Space ();
			m_PlatformSelection = GUIPlatformGroup.Draw (m_PlatformSelection);

			ParameterOverrideGUI.DrawSlider (m_Iterator.SelectArray ("m_SampleSettings", "m_Quality", m_PlatformSelection), 0, 100);
			ParameterOverrideGUI.DrawEnum (m_Iterator.SelectArray ("m_SampleSettings", "m_SampleRateSetting", m_PlatformSelection), typeof (AudioSampleRateSetting));
			ParameterOverrideGUI.DrawIntPopup (m_Iterator.SelectArray ("m_SampleSettings", "m_SampleRateOverride", m_PlatformSelection), k_SampleRateStrings, k_SampleRateValues);
			ParameterOverrideGUI.DrawEnum (m_Iterator.SelectArray ("m_SampleSettings", "m_CompressionFormat", m_PlatformSelection), typeof (AudioCompressionFormat));
			#endregion
		}
	}
}