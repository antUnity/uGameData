using UnityEngine;

namespace IndexedGameData
{
    public abstract class AssetValuePair<TIndex, TAsset, ValueType> : AssetEntry<TIndex, TAsset> where TAsset : IndexedAsset<TIndex>
    {
        [Tooltip("A value associated with this asset.")]
        [SerializeField] protected ValueType value = default;

        public AssetValuePair(TAsset asset, ValueType value = default) : base(asset)
        {
            this.asset = asset;
            this.value = value;
        }

        public ValueType Value
        {
            get => value;
            set => this.value = value;
        }
    }

    public abstract class AssetRangePair<TIndex, TAsset, TValue> : AssetValuePair<TIndex, TAsset, TValue> where TAsset : IndexedAsset<TIndex>
    {
        [SerializeField] protected TValue minValue = default;
        [SerializeField] protected TValue maxValue = default;

        public AssetRangePair(TAsset asset, TValue value = default, TValue min = default, TValue max = default) : base(asset, value)
        {
            minValue = min;
            maxValue = max;
        }

        public TValue MinValue
        {
            get => minValue;
            set => minValue = value;
        }

        public TValue MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }
    }

    public abstract class AssetCapPair<TIndex, TAsset, TValue> : AssetRangePair<TIndex, TAsset, TValue> where TAsset : IndexedAsset<TIndex>
    {
        public AssetCapPair(TAsset asset, TValue value = default, TValue max = default) : base(asset, value, default, max) { }
    }
}
