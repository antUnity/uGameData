using System;
using UnityEngine;

namespace IndexedGameData
{
    public interface ICopyable<T>
    {
        T Copy();
    }

    public interface IGameData
    {
        object Index { get; }
    }

    [Serializable]
    public abstract class GameData<TIndex> : IGameData
    {
        public static implicit operator TIndex(GameData<TIndex> obj) => obj.index;

        [SerializeField] private TIndex index = default;

        public object Index
        {
            get => index;
            set => index = (TIndex)value;
        }

        public GameData(TIndex index) => this.index = index;
    }

    public abstract class GameDataInstance<TIndex, TValue> : GameData<TIndex> where TValue : struct, ICopyable<TValue>
    {
        protected TValue template = default;

        public TValue Template
        {
            get => template;
            set => template = value;
        }

        public GameDataInstance(TIndex index, TValue template) : base(index) => this.template = template.Copy();
    }
}