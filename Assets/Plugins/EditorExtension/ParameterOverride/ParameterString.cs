using System;

namespace MaxRoetzler.Extensions
{
	[Serializable]
	public sealed class ParameterString : ParameterOverride<string>
	{
		/// <summary>
		/// Creates a new instance of <ctor>ParameterInt</ctor>.
		/// </summary>
		/// <param name="value">The integer value.</param>
		public ParameterString (string value) : base (value)
		{
		}

		/// <summary>
		/// Implicit operator allows to cast an int value to a ParameterInt object.
		/// </summary>
		/// <param name="value">The integer value.</param>
		public static implicit operator ParameterString (string value)
		{
			return new ParameterString (value);
		}
	}
}