using UnityEngine;

namespace IndexedAssets {
    public abstract class IndexedAssetBase<T> : ScriptableObject, IHasIndexProperty<T> {
        // Operators
        public static implicit operator T(IndexedAssetBase<T> obj) {
            return obj ? obj.Index : default(T);
        }

        // Fields
        [Tooltip("A unique index associated with this asset. Uses a 32-bit unsigned integer value (range of 0 to 4294967295). 0 is not a valid index.")]
        [Header("ID")]
        [SerializeField] protected T index = default(T);

        [Header("Display")]

        [Tooltip("A sprite icon that can be used to display this asset in-game.")]
        [SerializeField] private Sprite icon;

        [Tooltip("A display name that can be used to describe this asset in-game.")]
        [SerializeField] private string displayName = string.Empty;

        [Tooltip("A description for how this asset is meant to be used.")]
        [SerializeField][TextArea] private string description = string.Empty;

        // Properties
        // Public

        public string DisplayName {
            get { return displayName; }
        }

        public string Description {
            get { return description; }
        }

        public Sprite Icon => icon;

        public T Index {
            get {
                return index;
            }
            set { throw new System.Exception($"`Index` property cannot be altered in `{GetType()}`."); }
        }
    }
}
