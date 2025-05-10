using System;

namespace IndexedAssets {
    [Serializable]
    public class IndexedAssetEntry<AssetType> : IndexedAssetEntryBase<AssetType, uint> where AssetType : IndexedAsset {
        public IndexedAssetEntry() {
        }

        public IndexedAssetEntry(AssetType asset) : base(asset) {
        }
    }
}