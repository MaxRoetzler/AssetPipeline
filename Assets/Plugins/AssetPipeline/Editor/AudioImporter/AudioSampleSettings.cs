using MaxRoetzler.Extensions;
using UnityEngine;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public sealed class AudioSampleSettings
	{
		[SerializeField] private ParameterFloat m_Quality;
		[SerializeField] private ParameterInt m_SampleRateSetting;
		[SerializeField] private ParameterInt m_SampleRateOverride;
		[SerializeField] private ParameterInt m_CompressionFormat;
		[SerializeField] private ParameterBool m_PreloadAudioData;
	}
}