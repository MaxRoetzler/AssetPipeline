using UnityEngine;
using UnityEditor;
using System;

namespace MaxRoetzler.Extensions
{
	public static class ParameterOverrideGUI
	{
		private const int k_Toggle = 16;
		private const float k_LabelWidth = 0.6f;
		private const float k_FieldWidth = 0.4f;

		public static void DrawGeneric (ParameterOverrideTuple tuple)
		{
			Rect rect = DrawLabel (tuple);
			EditorGUI.PropertyField (rect, tuple.Property, GUIContent.none);
		}

		public static bool DrawToggle (ParameterOverrideTuple tuple)
		{
			Rect rect = DrawLabel (tuple);
			EditorGUI.PropertyField (rect, tuple.Property, GUIContent.none);

			return tuple.BoolValue;
		}

		public static int DrawEnum (ParameterOverrideTuple tuple, Type type)
		{
			Rect rect = DrawLabel (tuple);
			string [] options = Enum.GetNames (type);

			tuple.IntValue = EditorGUI.Popup (rect, string.Empty, tuple.IntValue, options);

			return tuple.IntValue;
		}

		public static int DrawIntPopup (ParameterOverrideTuple tuple, string [] options, int [] values)
		{
			Rect rect = DrawLabel (tuple);
			tuple.IntValue = EditorGUI.IntPopup (rect, string.Empty, tuple.IntValue, options, values);

			return tuple.IntValue;
		}

		public static float DrawSlider (ParameterOverrideTuple tuple, float min, float max)
		{
			Rect rect = DrawLabel (tuple);
			tuple.FloatValue = EditorGUI.Slider (rect, GUIContent.none, tuple.FloatValue, min, max);

			return tuple.FloatValue;
		}

		public static void DrawTitle (string name)
		{
			EditorGUILayout.Space ();
			Rect rect = EditorGUILayout.GetControlRect ();

			EditorGUI.LabelField (rect, new GUIContent (name), EditorStyles.boldLabel);
		}

		private static Rect DrawLabel (ParameterOverrideTuple tuple)
		{
			GUI.enabled = true;

			Rect rect = EditorGUILayout.GetControlRect ();
			Rect rectLabel = new Rect (rect.x, rect.y, rect.width * k_LabelWidth, rect.height);
			Rect rectField = new Rect (rect.x + rect.width * k_LabelWidth, rect.y, rect.width * k_FieldWidth, rect.height);

			if (tuple.IsOverridable)
			{
				rect.width = k_Toggle;
				tuple.State = EditorGUI.Toggle (rect, GUIContent.none, tuple.State, EditorStyles.radioButton);
				rectLabel.width -= k_Toggle;
				rectLabel.x += k_Toggle;
				GUI.enabled = tuple.State;
			}

			rectLabel = EditorGUI.IndentedRect (rectLabel);
			EditorGUI.LabelField (rectLabel, tuple.Label);
			return rectField;
		}
	}
}