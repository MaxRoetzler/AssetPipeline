using UnityEngine;
using UnityEditor;

namespace MaxRoetzler.Extensions
{
	public class GUISelectableList : IGUIControl
	{
		private int m_Selection;
		private float m_ElementHeight;
		private Vector2 m_ScrollPosition;

		private GUIStyle m_textNormal;
		private GUIStyle m_textActive;
		private GUIStyle m_rectNormal;
		private GUIStyle m_rectActive;
		private GUIStyle m_listBorder;

		private GUIContent m_HeaderTitle;
		private SerializedObject m_SerializedObject;
		private SerializedProperty m_SerializedProperty;

		public GUISelectableList (SerializedObject serializedObject, SerializedProperty serializedProperty)
		{
			m_Selection = -1;
			m_ElementHeight = EditorGUIUtility.singleLineHeight;
			m_HeaderTitle = new GUIContent ("Header");

			// Setup default behaviors
			onItemAdd = OnItemAdd;
			onItemRemove = OnItemRemove;
			onItemSelect = OnItemSelect;
			onItemRepaint = OnItemRepaint;
			onCanRemove = OnCanRemove;

			m_SerializedObject = serializedObject;
			m_SerializedProperty = serializedProperty;
		}

		#region Delegates
		public delegate void ItemAddDelegate (SerializedProperty array);
		public delegate void ItemRemoveDelegate (SerializedProperty array, int index);
		public delegate void ItemSelectDelegate (SerializedProperty item, int index);
		public delegate void ItemRepaintDelegate (SerializedProperty item, Rect rect);
		public delegate bool CanRemoveDelegate (SerializedProperty array, int index);

		public ItemAddDelegate onItemAdd;
		public ItemRemoveDelegate onItemRemove;
		public ItemSelectDelegate onItemSelect;
		public ItemRepaintDelegate onItemRepaint;
		public CanRemoveDelegate onCanRemove;
		#endregion

		#region Properties
		public float ElementHeight
		{
			get
			{
				return m_ElementHeight;
			}
			set
			{
				m_ElementHeight = value;
			}
		}

		public int Selection
		{
			get
			{
				return m_Selection;
			}

			set
			{
				m_Selection = value;
				onItemSelect (m_SerializedProperty.GetArrayElementAtIndex (value), value);
			}
		}

		public SerializedProperty SerializedProperty
		{
			get
			{
				return m_SerializedProperty;
			}

			set
			{
				if (value.isArray)
				{
					m_SerializedProperty = value;
				}
			}
		}

		public GUIContent HeaderTitle
		{
			get
			{
				return m_HeaderTitle;
			}

			set
			{
				m_HeaderTitle = value;
			}
		}
		#endregion

		#region Default Delegates
		/// <summary>
		/// Adds a new item to the array.
		/// </summary>
		/// <param name="array">The item array.</param>
		private void OnItemAdd (SerializedProperty array)
		{
			int length = array.arraySize;
			array.InsertArrayElementAtIndex (length);
		}

		/// <summary>
		/// Removes the item at the specified index from the array.
		/// </summary>
		/// <param name="array">The item array.</param>
		/// <param name="index">The item index.</param>
		private void OnItemRemove (SerializedProperty array, int index)
		{
			array.DeleteArrayElementAtIndex (index);
		}

		/// <summary>
		/// Called when an item has been selected.
		/// </summary>
		/// <param name="item">The selected item.</param>
		private void OnItemSelect (SerializedProperty item, int index)
		{
			return;
		}

		/// <summary>
		/// Draws the item on top of the selection box.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="rect">The rect.</param>
		private void OnItemRepaint (SerializedProperty item, Rect rect)
		{
			EditorGUI.PropertyField (rect, item);
		}

		/// <summary>
		/// Checks if the item can be removed from the array.
		/// </summary>
		/// <param name="array">The item array.</param>
		/// <param name="index">The item index.</param>
		/// <returns>True, if the index is not out of range, otherwise false.</returns>
		private bool OnCanRemove (SerializedProperty array, int index)
		{
			return (index > -1 && index < array.arraySize);
		}

		#endregion

		// TODO : Create static class for custom GUIStyles
		// TODO : Handle colors for default and pro skin
		private void GetStyles ()
		{
			m_textNormal = new GUIStyle ("label");
			m_textActive = new GUIStyle ("label");
			m_rectNormal = new GUIStyle ("label");
			m_rectActive = new GUIStyle ("label");
			m_listBorder = new GUIStyle ("helpbox")
			{
				margin = new RectOffset (0, 0, 0, 0),
				padding = new RectOffset (1, 1, 1, 1),
			};

			if (EditorGUIUtility.isProSkin)
			{
				m_textActive.normal.textColor = Color.white;
				m_rectActive.normal.background = EditorGUIUtility.Load ("builtin skins/darkskin/images/selected.png") as Texture2D;
			}
			else
			{
				m_textActive.normal.textColor = Color.black;
				m_rectActive.normal.background = EditorGUIUtility.Load ("builtin skins/lightskin/images/selected.png") as Texture2D;
			}
		}

		private void DrawHeader ()
		{
			GUILayout.BeginHorizontal (EditorStyles.toolbar);
			GUILayout.Label (m_HeaderTitle);

			if (GUILayout.Button ("Add", EditorStyles.toolbarButton, GUILayout.MaxWidth (30)))
			{
				onItemAdd (m_SerializedProperty);

				// Update selection, fire callback
				m_Selection = m_SerializedProperty.arraySize - 1;
				onItemSelect (m_SerializedProperty.GetArrayElementAtIndex (m_Selection), m_Selection);
			}

			GUI.enabled = onCanRemove (m_SerializedProperty, m_Selection);

			if (GUILayout.Button ("Remove", EditorStyles.toolbarButton, GUILayout.MaxWidth (50)))
			{
				onItemRemove (m_SerializedProperty, m_Selection);
				m_SerializedObject.ApplyModifiedProperties ();

				if (m_Selection == m_SerializedProperty.arraySize)
				{
					m_Selection = 0;
					onItemSelect (m_SerializedProperty.GetArrayElementAtIndex (0), 0);
				}
				else
				{
					onItemSelect (m_SerializedProperty.GetArrayElementAtIndex (m_Selection), m_Selection);
				}
			}

			GUI.enabled = true;
			GUILayout.EndHorizontal ();
		}

		public void Draw ()
		{
			if (m_textActive == null)
			{
				GetStyles ();
			}

			if (m_SerializedProperty == null)
			{
				return;
			}

			GUILayout.BeginVertical (m_listBorder);
			DrawHeader ();

			Event e = Event.current;
			m_ScrollPosition = GUILayout.BeginScrollView (m_ScrollPosition, false, true, GUILayout.MaxHeight (80));

			for (int i = 0; i < m_SerializedProperty.arraySize; i++)
			{
				SerializedProperty item = m_SerializedProperty.GetArrayElementAtIndex (i);

				Rect rect = GUILayoutUtility.GetRect (GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth (true), GUILayout.MinHeight (m_ElementHeight));
				GUI.Box (rect, GUIContent.none, (i == m_Selection) ? m_rectActive : m_rectNormal);

				if (e.type == EventType.MouseDown && rect.Contains (e.mousePosition))
				{
					GUI.FocusControl (null);

					if (i != m_Selection)
					{
						m_Selection = i;
						onItemSelect (item, i);
					}

					e.Use ();
				}

				onItemRepaint (item, rect);
			}

			GUILayout.EndScrollView ();
			GUILayout.EndVertical ();
		}
	}
}
