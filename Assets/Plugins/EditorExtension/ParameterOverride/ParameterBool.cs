using System;

namespace MaxRoetzler.Extensions
{
	[Serializable]
	public sealed class ParameterBool : ParameterOverride<bool>
	{
		/// <summary>
		/// Creates a new instance of <ctor>ParameterBool</ctor>.
		/// </summary>
		/// <param name="state">The boolean state.</param>
		public ParameterBool (bool value) : base (value)
		{
		}

		/// <summary>
		/// Implicit operator allows to cast a boolean value to a ParameterBool object.
		/// </summary>
		/// <param name="state">The boolean state.</param>
		public static implicit operator ParameterBool (bool state)
		{
			return new ParameterBool (state);
		}
	}
}