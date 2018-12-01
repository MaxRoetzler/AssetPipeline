using UnityEditor;
using UnityEngine;
using MaxRoetzler.Extensions;

namespace MaxRoetzler.AssetPipeline
{
	public sealed class TextureSettingsGUI : AssetSettingsGUI
	{
		public TextureSettingsGUI (SerializedObject serializedObject, bool isDefault) : base (serializedObject, isDefault) { }

		public override void Draw ()
		{
			#region General Settings
			ParameterOverrideGUI.DrawTitle ("General Settings");
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_TextureType"), typeof (TextureImporterType));
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_TextureShape"), typeof (TextureImporterShape));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_AlphaIsTransparency"));
			#endregion

			#region Advanced
			ParameterOverrideGUI.DrawTitle ("Advanced");
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_NpotScale"), typeof (TextureImporterNPOTScale));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_IsReadable"));
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_WrapMode"), typeof (TextureWrapMode));
			ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_FilterMode"), typeof (FilterMode));
			ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_AnisoLevel"));
			#endregion

			#region Mip Maps
			ParameterOverrideGUI.DrawTitle ("Mip Maps");
			if (ParameterOverrideGUI.DrawToggle (m_Iterator.Select ("m_MipmapEnabled")))
			{
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_BorderMipmap"));
				ParameterOverrideGUI.DrawEnum (m_Iterator.Select ("m_MipmapFilter"), typeof (TextureImporterMipFilter));
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_MipMapsPreserveCoverage"));
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_MipmapFadeDistanceStart"));
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_MipmapFadeDistanceEnd"));
				ParameterOverrideGUI.DrawGeneric (m_Iterator.Select ("m_Fadeout"));
			}
			#endregion
		}
	}
}
