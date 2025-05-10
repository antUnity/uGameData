using System;
using UnityEngine;

namespace IndexedAssets {

    [Serializable]
    public class IndexedAssetEntryBase<AssetType, T> : IHasIndexProperty<T> where AssetType : IndexedAssetBase<T> {
        // Operators

        public static implicit operator T(IndexedAssetEntryBase<AssetType, T> obj) {
            return obj.Index;
        }

        // Fields
        [Tooltip("The indexed asset (scriptable object) associated with this entry. All indexed assets should use a unique index.")]
        [SerializeField] protected AssetType asset = null;

        // Properties
        // Public

        public AssetType Asset {
            get { return asset; }
            set { asset = value; }
        }

        public T Index {
            get {
                if (!asset)
                    return default;

                return asset.Index;
            }
            set {
                if (value != null)
                    throw new Exception("Entry `Index` cannot be assigned to a value.");

                asset = null;
            }
        }

        // Constructor

        public IndexedAssetEntryBase() {
        }

        public IndexedAssetEntryBase(AssetType asset) {
            this.asset = asset;
        }
    }
}
