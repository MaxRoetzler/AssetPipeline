using UnityEngine;
using UnityEditor;

namespace MaxRoetzler.Extensions
{
	public class GUIPlatformGroup
	{
		public static int Draw (int selection)
		{
			return GUILayout.SelectionGrid (selection, new string [] { "Default", "Desktop", "iOS", "Android", "XBox", "PS4" }, 6, EditorStyles.toolbarButton);
		}
	}
}