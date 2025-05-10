using System;
using UnityEngine;

namespace IndexedAssets {
    [Serializable]
    public abstract class IndexedBase<IndexType> : IHasIndexProperty<IndexType> {
        // Operators

        public static implicit operator IndexType(IndexedBase<IndexType> obj) {
            return obj.Index;
        }

        // Fields

        [SerializeField] private IndexType index = default(IndexType);

        // Properties
        // Public

        public IndexType Index {
            get { return index; }
            set { index = value; }
        }

        // Constructor

        public IndexedBase() {
        }

        public IndexedBase(IndexType index) {
            this.index = index;
        }
    }
}