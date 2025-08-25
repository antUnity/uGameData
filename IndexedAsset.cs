using System;
using UnityEngine;

namespace IndexedGameData
{
    public abstract class IndexedAsset<TIndex> : ScriptableObject, IIndexed<TIndex>
    {
        // Operators
        public static implicit operator TIndex(IndexedAsset<TIndex> obj) => obj ? obj.Index : default;

        // Fields
        [Tooltip("A unique index associated with this asset.")]
        [SerializeField] protected TIndex index = default;

        // Properties
        // Public

        public TIndex Index
        {
            get => index;
            set => throw new Exception($"`Index` property cannot be altered in `{GetType()}`.");
        }
    }
}
