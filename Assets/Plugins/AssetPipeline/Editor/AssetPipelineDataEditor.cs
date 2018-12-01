using UnityEngine;
using UnityEditor;

namespace MaxRoetzler.AssetPipeline
{
	[CustomEditor (typeof (AssetPipelineData))]
	public class AssetPipelineDataEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			EditorGUILayout.HelpBox ("Edit the settings through the AssetPiple Window.", MessageType.Info);

			if (GUILayout.Button ("Open"))
			{
				EditorWindow.GetWindow<AssetPipelineWindow> ().Show ();
			}

			base.OnInspectorGUI ();
		}
	}
}