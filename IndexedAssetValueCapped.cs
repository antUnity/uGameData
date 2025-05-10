using System;
using UnityEngine;

namespace IndexedAssets {
    [Serializable]
    public class IndexedAssetValueCapped<AssetType, ValueType> : IndexedAssetValue<AssetType, ValueType> where AssetType : IndexedAsset {
        [SerializeField] protected ValueType maxValue = default;

        public IndexedAssetValueCapped(AssetType asset, ValueType value = default, ValueType max = default) : base(asset, value) {
            maxValue = max;
        }

        public ValueType MaxValue {
            get { return maxValue; }
            set { maxValue = value; }
        }
    }
}
