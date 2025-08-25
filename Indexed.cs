using System;
using UnityEngine;

namespace IndexedGameData
{
    [Serializable]
    public abstract class Indexed<TIndex> : IIndexed<TIndex>
    {
        // Operators

        public static implicit operator TIndex(Indexed<TIndex> obj) => obj.Index;

        // Fields

        [SerializeField] private TIndex index = default;

        // Properties
        // Public

        public TIndex Index {
            get => index;
            set => index = value;
        }

        // Constructor

        public Indexed() { }

        public Indexed(TIndex index) => this.index = index;
    }
}