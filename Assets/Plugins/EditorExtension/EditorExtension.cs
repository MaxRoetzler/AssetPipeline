using UnityEngine;
using UnityEditor;

namespace MaxRoetzler.Extensions
{
	public static class EditorExtensions
	{
		public static T LoadAsset <T> () where T : Object
		{
			string type = typeof (T).Name;
			string [] guids = AssetDatabase.FindAssets ("t:" + type);

			if (guids.Length > 0)
			{
				if (guids.Length > 1)
				{
					Debug.LogWarningFormat ("More than one instance of <b>{0}</b> found, using first occurance.", type);
				}

				return AssetDatabase.LoadAssetAtPath<T> (AssetDatabase.GUIDToAssetPath (guids [0]));
			}

			return null;
		}

		public static T CreateAsset<T> (string assetPath, bool pingObject) where T : ScriptableObject
		{
			T instance = ScriptableObject.CreateInstance<T> ();

			AssetDatabase.CreateAsset (instance, assetPath);
			EditorUtility.SetDirty (instance);

			if (pingObject)
			{
				Selection.activeObject = instance;
				EditorGUIUtility.PingObject (instance);
			}

			return instance;
		}

		public static void DeleteSubAssets (Object asset)
		{
			Object [] assets = AssetDatabase.LoadAllAssetRepresentationsAtPath (AssetDatabase.GetAssetPath (asset));

			// TODO : Check if reverse order is required here ...
			for (int i = assets.Length - 1; i > -1; i--)
			{
				Object.DestroyImmediate (assets [i], true);
			}

			EditorUtility.SetDirty (asset);
		}

		public static string Search (string search)
		{
			string [] guids = AssetDatabase.FindAssets (search);

			if (guids.Length > 0)
			{
				if (guids.Length > 1)
				{
					Debug.LogWarningFormat ("More than one instance of <b>{0}</b> found, using first occurance.", search);
				}

				return AssetDatabase.GUIDToAssetPath (guids [0]);
			}

			return string.Empty;
		}
	}
}