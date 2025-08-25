using System;
using UnityEngine;

using IndexedGameData;

namespace IndexedGameData
{
    
    [Serializable]
    public class AssetEntry<TIndex, TAsset> : IIndexed<TIndex> where TAsset : IndexedAsset<TIndex>
    {
        // Operators

        public static implicit operator TIndex(AssetEntry<TIndex, TAsset> obj) => obj.Index;

        // Fields
        [Tooltip("The indexed asset (scriptable object) associated with this entry. All indexed assets should use a unique index.")]
        [SerializeField] protected TAsset asset = null;

        // Properties
        // Public

        public TAsset Asset
        {
            get => asset;
            set => asset = value;
        }

        public TIndex Index
        {
            get => asset ? asset.Index : default;

            set
            {
                if (value != null)
                    throw new Exception("Entry `Index` cannot be assigned to a value.");

                asset = null;
            }
        }

        // Constructor

        public AssetEntry() { }

        public AssetEntry(TAsset asset) => this.asset = asset;
    }
}