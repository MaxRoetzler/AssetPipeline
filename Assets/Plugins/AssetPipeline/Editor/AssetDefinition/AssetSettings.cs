using UnityEditor;

namespace MaxRoetzler.AssetPipeline
{
	public abstract class AssetSettings
	{
		public AssetSettings ()
		{
			Reset ();
		}

		public abstract void Apply (AssetImporter assetImporter, AssetSettings template);
		public abstract void Reset ();
	}
}