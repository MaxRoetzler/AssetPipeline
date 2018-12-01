using UnityEditor;

namespace MaxRoetzler.Extensions
{
	public class ParamterOverrideIterator
	{
		private ParameterOverrideTuple m_Tuple;
		private SerializedProperty m_BaseObject;
		private SerializedProperty m_OverrideObject;

		public ParamterOverrideIterator (SerializedProperty baseObject, SerializedProperty overrideObject, bool isOverridable = true)
		{
			m_BaseObject = baseObject;
			m_OverrideObject = overrideObject;

			m_Tuple = new ParameterOverrideTuple
			{
				IsOverridable = isOverridable
			};
		}

		public ParameterOverrideTuple Select (string propertyName)
		{
			m_Tuple.Init (m_BaseObject.FindPropertyRelative (propertyName), m_OverrideObject.FindPropertyRelative (propertyName));

			return m_Tuple;
		}

		public ParameterOverrideTuple SelectArray (string arrayName, string propertyName, int index)
		{
			string name = string.Format ("{0}.Array.data[{1}].{2}", arrayName, index.ToString (), propertyName);
			m_Tuple.Init (m_BaseObject.FindPropertyRelative (name), m_OverrideObject.FindPropertyRelative (name));

			return m_Tuple;
		}
	}
}