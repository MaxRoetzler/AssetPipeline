using UnityEditor;
using UnityEngine;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public class AssetFiltersGUI : AssetSettingsGUI
	{
		private SerializedProperty m_Name;

		public AssetFiltersGUI (SerializedObject serializedObject, bool isDefault) : base (serializedObject, isDefault)
		{
			m_Name = serializedObject.FindProperty ("m_Name");

			SerializedProperty settingsBase = serializedObject.FindProperty ("m_Filters");
			SerializedProperty settingsOverride = new SerializedObject (serializedObject.FindProperty ("m_Template").objectReferenceValue).FindProperty ("m_Filters");

			m_Iterator = new ParamterOverrideIterator (settingsBase, settingsOverride, !isDefault);
		}

		public override void Draw ()
		{
			ParameterOverrideGUI.DrawTitle ("Name");
			EditorGUILayout.PropertyField (m_Name, GUIContent.none);
			EditorGUILayout.Space ();

			#region Filters
			ParameterOverrideGUI.DrawTitle ("Filters");
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_NameFilter"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_PathFilter"));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_TypeFilter"));
			EditorGUILayout.Space ();
			#endregion

			#region PostProcessors
			ParameterOverrideGUI.DrawTitle ("Post Processors");
			EditorGUILayout.LabelField ("Not Yet Implemented");
			EditorGUILayout.Space ();
			#endregion

			#region Validators
			ParameterOverrideGUI.DrawTitle ("Validators");
			EditorGUILayout.LabelField ("Not Yet Implemented");
			#endregion
		}
	}
}