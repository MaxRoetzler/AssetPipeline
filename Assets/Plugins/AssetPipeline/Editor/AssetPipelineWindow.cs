using UnityEngine;
using UnityEditor;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public class AssetPipelineWindow : EditorWindow
	{
		#region Fields
		private SerializedObject m_PipelineData;
		private SerializedObject m_DefinitionItem;
		private SerializedProperty m_DefinitionArray;
		private AssetFiltersGUI m_AssetFiltersGUI;
		private AssetSettingsGUI m_AssetSettingsGUI;
		private GUISelectableList m_DefinitionsGUI;
		private Vector2 m_ScrollPosition;

		private GUIStyle m_StyleColumns;
		#endregion

		#region Delegates
		private delegate void DeleteDefinitionDelegate (int index);
		private delegate AssetDefinition CreateDefinitionDelegate ();
		private delegate AssetSettingsGUI CreateSettingsGUIDelegate (bool isDefault);

		// Annonymous methods
		private CreateDefinitionDelegate onCreateDefinition;
		private DeleteDefinitionDelegate onDeleteDefinition;
		private CreateSettingsGUIDelegate onCreateSettingsGUI;
		#endregion

		#region Default Methods
		private AudioDefinition CreateAudioDefinition ()
		{
			return AssetPipeline.Data.CreateDefinition (AssetPipeline.Data.AudioDefinitions);
		}

		private AssetSettingsGUI CreateAudioSettingsGUI (bool isDefault)
		{
			return new AudioSettingsGUI (m_DefinitionItem, isDefault);
		}

		private ModelDefinition CreateModelDefinition ()
		{
			return AssetPipeline.Data.CreateDefinition (AssetPipeline.Data.ModelDefinitions);
		}

		private AssetSettingsGUI CreateModelSettingsGUI (bool isDefault)
		{
			return new ModelSettingsGUI (m_DefinitionItem, isDefault);
		}

		private TextureDefinition CreateTextureDefinition ()
		{
			return AssetPipeline.Data.CreateDefinition (AssetPipeline.Data.TextureDefinitions);
		}

		private AssetSettingsGUI CreateTextureSettingsGUI (bool isDefault)
		{
			return new TextureSettingsGUI (m_DefinitionItem, isDefault);
		}
		#endregion

		private void SelectCategory (AssetCategory assetCategory)
		{
			switch (assetCategory)
			{
				case AssetCategory.Audio:
					onCreateDefinition = CreateAudioDefinition;
					onCreateSettingsGUI = CreateAudioSettingsGUI;
					m_DefinitionArray = m_PipelineData.FindProperty ("m_AudioDefinitions");
					break;

				case AssetCategory.Model:
					onCreateDefinition = CreateModelDefinition;
					onCreateSettingsGUI = CreateModelSettingsGUI;
					m_DefinitionArray = m_PipelineData.FindProperty ("m_ModelDefinitions");
					break;

				case AssetCategory.Texture:
					onCreateDefinition = CreateTextureDefinition;
					onCreateSettingsGUI = CreateTextureSettingsGUI;
					m_DefinitionArray = m_PipelineData.FindProperty ("m_TextureDefinitions");
					break;
			}

			m_DefinitionsGUI.SerializedProperty = m_DefinitionArray;
			SelectDefinition (0);
		}

		private void SelectDefinition (int index)
		{
			m_DefinitionItem = new SerializedObject (m_DefinitionArray.GetArrayElementAtIndex (index).objectReferenceValue);
			m_AssetFiltersGUI = new AssetFiltersGUI (m_DefinitionItem, index == 0);
			m_AssetSettingsGUI = onCreateSettingsGUI (index == 0);
		}

		private void DrawCategories ()
		{
			GUILayout.BeginHorizontal ();

			if (GUILayout.Button ("Audio", EditorStyles.miniButtonLeft, GUILayout.Height (24)))
			{
				SelectCategory (AssetCategory.Audio);
			}
			if (GUILayout.Button ("Model", EditorStyles.miniButtonMid, GUILayout.Height (24)))
			{
				SelectCategory (AssetCategory.Model);
			}
			if (GUILayout.Button ("Texture", EditorStyles.miniButtonMid, GUILayout.Height (24)))
			{
				SelectCategory (AssetCategory.Texture);
			}
			if (GUILayout.Button ("Animation", EditorStyles.miniButtonRight, GUILayout.Height (24)))
			{
				SelectCategory (AssetCategory.Texture);
			}

			GUILayout.EndHorizontal ();
		}

		private void OnGUI ()
		{
			m_StyleColumns = EditorStyles.inspectorDefaultMargins;
			m_StyleColumns.margin = new RectOffset (0, 4, 10, 0);

			m_PipelineData.Update ();
			m_DefinitionItem.Update ();

			EditorGUI.DrawRect (new Rect (355, 0, 1, 600), new Color (0.18f,0.18f,0.18f));
			EditorGUI.DrawRect (new Rect (355, 118, 700, 1), new Color (0.18f, 0.18f, 0.18f));

			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical (m_StyleColumns, GUILayout.Width (350));
			DrawCategories ();

			m_ScrollPosition = EditorGUILayout.BeginScrollView (m_ScrollPosition);
			m_AssetSettingsGUI.Draw ();
			GUILayout.Space (16);
			EditorGUILayout.EndScrollView ();
			GUILayout.EndVertical ();

			GUILayout.BeginVertical (m_StyleColumns);
			m_DefinitionsGUI.Draw ();
			GUILayout.Space (16);
			m_AssetFiltersGUI.Draw ();
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			m_DefinitionItem.ApplyModifiedProperties ();
			m_PipelineData.ApplyModifiedProperties ();
		}

		private void OnEnable ()
		{
			titleContent = new GUIContent ("Asset Pipeline");

			m_PipelineData = new SerializedObject (AssetPipeline.Data);
			m_DefinitionsGUI = new GUISelectableList (m_PipelineData, null)
			{
				onItemAdd = (SerializedProperty array) =>
				{
					int length = array.arraySize;
					array.InsertArrayElementAtIndex (length);
					array.GetArrayElementAtIndex (length).objectReferenceValue = onCreateDefinition ();
				},

				onItemRemove = (SerializedProperty array, int index) =>
				{
					SerializedProperty item = array.GetArrayElementAtIndex (index);
					DestroyImmediate (item.objectReferenceValue, true);

					item.objectReferenceValue = null;
					array.DeleteArrayElementAtIndex (index);
				},

				onCanRemove = (SerializedProperty array, int index) =>
				{
					return (array.arraySize > 0 && index > 0);
				},

				onItemSelect = (SerializedProperty item, int index) =>
				{
					SelectDefinition (index);
				},

				onItemRepaint = (SerializedProperty item, Rect rect) =>
				{
					EditorGUI.LabelField (rect, item.objectReferenceValue.name);
				}
			};

			SelectCategory (AssetCategory.Audio);
		}

		[MenuItem ("Window/Asset Pipeline Window")]
		public static void Open ()
		{
			GetWindow<AssetPipelineWindow> ().Show ();
		}
	}
}