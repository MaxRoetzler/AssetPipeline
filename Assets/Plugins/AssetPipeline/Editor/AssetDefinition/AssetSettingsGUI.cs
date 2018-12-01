using MaxRoetzler.Extensions;
using UnityEditor;

namespace MaxRoetzler.AssetPipeline
{
	public abstract class AssetSettingsGUI
	{
		protected int m_PlatformSelection;
		protected ParamterOverrideIterator m_Iterator;

		public AssetSettingsGUI (SerializedObject serializedObject, bool isDefault)
		{
			SerializedProperty settingsOverride = serializedObject.FindProperty ("m_Settings");
			SerializedProperty settingsBase = new SerializedObject (serializedObject.FindProperty ("m_Template").objectReferenceValue).FindProperty ("m_Settings");

			m_Iterator = new ParamterOverrideIterator (settingsBase, settingsOverride, !isDefault);
		}

		public abstract void Draw ();
	}
}