using System;
using UnityEngine;

namespace IndexedAssets {
    [Serializable]
    public class IndexedAssetValueRange<AssetType, ValueType> : IndexedAssetValue<AssetType, ValueType> where AssetType : IndexedAsset {
        [SerializeField] protected ValueType minValue = default;
        [SerializeField] protected ValueType maxValue = default;

        public IndexedAssetValueRange(AssetType asset, ValueType value = default, ValueType min = default, ValueType max = default) : base(asset, value) {
            minValue = min;
            maxValue = max;
        }

        public ValueType MinValue {
            get { return minValue; }
            set { minValue = value; }
        }

        public ValueType MaxValue {
            get { return maxValue; }
            set { maxValue = value; }
        }
    }
}
