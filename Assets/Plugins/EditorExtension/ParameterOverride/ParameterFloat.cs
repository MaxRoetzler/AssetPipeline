using System;

namespace MaxRoetzler.Extensions
{
	[Serializable]
	public sealed class ParameterFloat : ParameterOverride<float>
	{
		/// <summary>
		/// Creates a new instance of <ctor>ParameterFloat</ctor>.
		/// </summary>
		/// <param name="value">The float value.</param>
		public ParameterFloat (float value) : base (value)
		{
		}

		/// <summary>
		/// Implicit operator allows to cast an float value to a ParameterFloat object.
		/// </summary>
		/// <param name="value">The float value.</param>
		public static implicit operator ParameterFloat (float value)
		{
			return new ParameterFloat (value);
		}
	}
}