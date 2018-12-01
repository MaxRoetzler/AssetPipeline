using UnityEngine;
using UnityEditor;

namespace MaxRoetzler.Extensions
{
	public class ParameterOverrideTuple
	{
		private SerializedProperty m_BaseValue;
		private SerializedProperty m_OverrideValue;
		private SerializedProperty m_OverrideState;

		public void Init (SerializedProperty baseParameter, SerializedProperty overrideParameter)
		{
			m_OverrideValue = overrideParameter.FindPropertyRelative ("m_Value");
			m_OverrideState = overrideParameter.FindPropertyRelative ("m_State");
			m_BaseValue = baseParameter.FindPropertyRelative ("m_Value");

			Label = baseParameter.displayName;

			if (!IsOverridable)
			{
				m_BaseValue = m_OverrideValue;
			}
		}

		public bool IsOverridable
		{
			get;
			set;
		}

		public string Label
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public bool State
		{
			get
			{
				return m_OverrideState.boolValue;
			}
			set
			{
				m_OverrideState.boolValue = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public SerializedProperty Property
		{
			get
			{
				return m_OverrideState.boolValue ? m_OverrideValue : m_BaseValue;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool BoolValue
		{
			get
			{
				return m_OverrideState.boolValue ? m_OverrideValue.boolValue : m_BaseValue.boolValue;
			}
			set
			{
				m_OverrideValue.boolValue = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int IntValue
		{
			get
			{
				return m_OverrideState.boolValue ? m_OverrideValue.intValue : m_BaseValue.intValue;
			}
			set
			{
				m_OverrideValue.intValue = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public float FloatValue
		{
			get
			{
				return m_OverrideState.boolValue ? m_OverrideValue.floatValue : m_BaseValue.floatValue;
			}
			set
			{
				m_OverrideValue.floatValue = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string StringValue
		{
			get
			{
				return m_OverrideState.boolValue ? m_OverrideValue.stringValue : m_BaseValue.stringValue;
			}
			set
			{
				m_OverrideValue.stringValue = value;
			}
		}
	}
}