using System;
using UnityEngine;

namespace MaxRoetzler.Extensions
{
	[Serializable]
	public sealed class ParameterInt : ParameterOverride<int>
	{
		/// <summary>
		/// Creates a new instance of <ctor>ParameterInt</ctor>.
		/// </summary>
		/// <param name="value">The integer value.</param>
		public ParameterInt (int value) : base (value)
		{
		}

		/// <summary>
		/// Implicit operator to cast an int value to a ParameterInt object.
		/// </summary>
		/// <param name="value">The integer value.</param>
		public static implicit operator ParameterInt (int value)
		{
			return new ParameterInt (value);
		}
	}
}