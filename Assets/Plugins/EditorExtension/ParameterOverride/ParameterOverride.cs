using UnityEngine;
using System;

namespace MaxRoetzler.Extensions
{
	[Serializable]
	public class ParameterOverride<T>
	{
		[SerializeField]
		private bool m_State;
		[SerializeField]
		private T m_Value;

		#region Constructor
		/// <summary>
		/// Creates a new instance of <ctor>ParameterOverride</ctor> using the default values.
		/// </summary>
		public ParameterOverride ()
		{
			m_Value = default (T);
			m_State = false;
		}

		/// <summary>
		/// Creates a new instance of <ctor>ParameterOverride</ctor> using the specified value.
		/// </summary>
		/// <param name="value">The default value.</param>
		public ParameterOverride (T value)
		{
			m_Value = value;
			m_State = false;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the ParameterOverride value.
		/// </summary>
		public T Value
		{
			get
			{
				return m_Value;
			}
		}

		/// <summary>
		/// Gets the ParameterOverride state.
		/// </summary>
		public bool State
		{
			get
			{
				return m_State;
			}
		}
		#endregion

		/// <summary>
		/// Implicit operator allows to cast ParameterOverride to the target value type.
		/// </summary>
		/// <param name="parameter">The parameter override.</param>
		public static implicit operator T (ParameterOverride<T> parameter)
		{
			return parameter.Value;
		}
	}
}