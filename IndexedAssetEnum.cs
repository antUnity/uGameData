using UnityEngine;

namespace IndexedAssets {
    public abstract class IndexedAsset<T> : ScriptableObject, IHasIndexProperty<T> where T : System.Enum {
        // Operators
        public static implicit operator T(IndexedAsset<T> obj) {
            return obj ? obj.Index : default;
        }

        // Fields
        [Tooltip("A unique index associated with this asset. Uses an enum value.")]
        [Header("ID")]
        [SerializeField] protected T index = default;

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

        public T Index {
            get {
                return index;
            }
            set { throw new System.Exception($"`Index` property cannot be altered in `{GetType()}`."); }
        }
    }
}
