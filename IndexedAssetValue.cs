using System;
using UnityEngine;

namespace IndexedAssets {
    [Serializable]
    public class IndexedAssetValue<AssetType, ValueType> : IndexedAssetEntry<AssetType> where AssetType : IndexedAsset {
        [Tooltip("A value associated with this asset.")]
        [SerializeField] protected ValueType value = default;

        public IndexedAssetValue(AssetType asset, ValueType value = default) : base(asset) {
            this.asset = asset;
            this.value = value;
        }

        public ValueType Value {
            get { return value; }
            set { this.value = value; }
        }
    }
}
