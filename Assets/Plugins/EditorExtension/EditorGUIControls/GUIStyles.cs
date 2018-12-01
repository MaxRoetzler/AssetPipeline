using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace MaxRoetzler.AssetPipeline
{
	//[InitializeOnLoad]
	//public class GUIStyles
	//{
	//	static GUIStyles ()
	//	{
	//		GenerateStyles ();
	//	}

	//	public static void GenerateStyles ()
	//	{
	//		GUISkin guiSkin = new GUISkin ();
	//		List<GUIStyle> styles = new List<GUIStyle> ();

	//		GUISkin editorSkin = UnityEngine.Object.Instantiate (EditorGUIUtility.GetBuiltinSkin (EditorSkin.Inspector)) as GUISkin;

	//		GUIStyle buttonOn = new GUIStyle (editorSkin.button) { name = "ButtonOn" };
	//		buttonOn.normal.background = buttonOn.active.background;
	//		styles.Add (buttonOn);

	//		GUIStyle buttonLeftOn = new GUIStyle (Array.Find (editorSkin.customStyles, x => x.name == "ButtonLeft")) { name = "ButtonLeftOn" };
	//		buttonLeftOn.normal.background = buttonLeftOn.active.background;
	//		styles.Add (buttonLeftOn);

	//		GUIStyle buttonMidOn = new GUIStyle (Array.Find (editorSkin.customStyles, x => x.name == "ButtonMid")) { name = "ButtonMidOn" };
	//		buttonMidOn.normal.background = buttonMidOn.active.background;
	//		styles.Add (buttonMidOn);

	//		GUIStyle buttonRightOn = new GUIStyle (Array.Find (editorSkin.customStyles, x => x.name == "ButtonRight")) { name = "ButtonRightOn" };
	//		buttonRightOn.normal.background = buttonRightOn.active.background;
	//		styles.Add (buttonRightOn);

	//		guiSkin.customStyles = styles.ToArray ();
	//		AssetDatabase.CreateAsset (guiSkin, "Assets/GUIStyles.guiskin");
	//	}

	//	private static Texture2D LoadTexture (string name)
	//	{
	//		string texturePath = string.Format ("builtin skins/{0}/images/{1}.png", EditorGUIUtility.isProSkin ? "darkskin" : "lightskin", name);
	//		return EditorGUIUtility.Load (texturePath) as Texture2D;
	//	}
	//}
}