using MaxRoetzler.Extensions;
using UnityEngine;
using System;

namespace MaxRoetzler.AssetPipeline
{
	[Serializable]
	public class AssetFilters
	{
		[SerializeField]
		private ParameterString m_NameFilter;
		[SerializeField]
		private ParameterString m_PathFilter;
		[SerializeField]
		private ParameterString m_TypeFilter;

		public AssetFilters (string typeFilter)
		{
			m_TypeFilter = typeFilter;
		}

		public ParameterString NameFilter
		{
			get
			{
				return m_NameFilter;
			}
		}

		public ParameterString PathFilter
		{
			get
			{
				return m_PathFilter;
			}
		}

		public ParameterString TypeFilter
		{
			get
			{
				return m_TypeFilter;
			}
		}
	}
}