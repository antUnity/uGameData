using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace IndexedAssets {
    public class AddressableManager : MonoBehaviour {
        private static Dictionary<Type, object> assetDictionaries = new();

        [SerializeField] private int NumAssets = 0;

        #region MonoBehaviour Methods

        private void Awake() {
            assetDictionaries = new();
        }

        private void FixedUpdate() {
            NumAssets = 0;

            foreach (var kvp in assetDictionaries) {
                var dictionaryType = kvp.Value.GetType(); // Get the dictionary's actual type
                var elementType = dictionaryType.GetGenericArguments()[1]; // Get the type of the values (e.g., Sprite, Texture2D)

                // Use reflection to iterate over the dictionary
                var enumerator = (System.Collections.IDictionary)kvp.Value;
                NumAssets += enumerator.Count;
            }
        }

        #endregion MonoBehaviour Methods

        #region Public Methods

        public static T LoadAsset<T>(string ID) where T : UnityEngine.Object {
            if (string.IsNullOrEmpty(ID)) {
                Debug.LogWarning($"(Warning) AddressableManager: Null or empty ID provided for type {typeof(T).Name}.");
                return null;
            }

            // Get or create the dictionary for the type `T`
            var cache = GetOrCreateCache<T>();

            // Check if the asset is already loaded
            if (cache.TryGetValue(ID, out T asset)) {
                return asset;
            }

            // Load the asset using Addressables
            AsyncOperationHandle<T> asyncHandle = Addressables.LoadAssetAsync<T>(ID);
            T loadedAsset = asyncHandle.WaitForCompletion();

            if (loadedAsset && asyncHandle.Status == AsyncOperationStatus.Succeeded) {
                cache[ID] = loadedAsset;
                return loadedAsset;
            } else {
                Debug.LogWarning($"(Warning) AddressableManager: Failed to load asset of type {typeof(T).Name} with ID: '{ID}'.");
                return null;
            }
        }

        public static async Task<T> LoadAssetAsync<T>(string ID) where T : UnityEngine.Object {
            if (string.IsNullOrEmpty(ID)) {
                Debug.LogWarning($"(Warning) AddressableManager: Null or empty ID provided for type {typeof(T).Name}.");
                return null;
            }

            var cache = GetOrCreateCache<T>();

            if (cache.TryGetValue(ID, out T asset)) {
                return asset;
            }

            AsyncOperationHandle<T> asyncHandle = Addressables.LoadAssetAsync<T>(ID);
            await asyncHandle.Task;

            if (asyncHandle.Status == AsyncOperationStatus.Succeeded) {
                cache[ID] = asyncHandle.Result;
                return asyncHandle.Result;
            } else {
                Debug.LogWarning($"(Warning) AddressableManager: Failed to load asset of type {typeof(T).Name} with ID: '{ID}'.");
                return null;
            }
        }

        public static T[] LoadAssets<T>(string ID) where T : UnityEngine.Object {
            if (string.IsNullOrEmpty(ID)) {
                Debug.LogWarning($"(Warning) AddressableManager: Null or empty ID provided for type {typeof(T).Name}.");
                return null;
            }

            // Get or create the dictionary for the type `T`
            var cache = GetOrCreateArrayCache<T>();

            // Check if the asset is already loaded
            if (cache.TryGetValue(ID, out T[] asset)) {
                return asset;
            }

            // Load the asset using Addressables
            AsyncOperationHandle<IList<T>> asyncHandle = Addressables.LoadAssetsAsync<T>(ID);
            IList<T> loadedAsset = asyncHandle.WaitForCompletion();

            if (loadedAsset != null && asyncHandle.Status == AsyncOperationStatus.Succeeded) {
                cache[ID] = loadedAsset.ToArray();
                return loadedAsset.ToArray();
            } else {
                Debug.LogWarning($"(Warning) AddressableManager: Failed to load asset of type {typeof(T).Name} with ID: '{ID}'.");
                return null;
            }
        }

        public static async Task<T[]> LoadAssetsAsync<T>(string ID) where T : UnityEngine.Object {
            if (string.IsNullOrEmpty(ID)) {
                Debug.LogWarning($"(Warning) AddressableManager: Null or empty ID provided for type {typeof(T).Name}.");
                return null;
            }

            // Get or create the dictionary for the type `T`
            var cache = GetOrCreateArrayCache<T>();

            // Check if the asset is already loaded
            if (cache.TryGetValue(ID, out T[] asset)) {
                return asset;
            }

            // Load the assets using Addressables
            AsyncOperationHandle<IList<T>> asyncHandle = Addressables.LoadAssetsAsync<T>(ID);

            try {
                IList<T> loadedAssets = await asyncHandle.Task; // Await the asset-loading process

                if (asyncHandle.Status == AsyncOperationStatus.Succeeded && loadedAssets != null) {
                    T[] assetArray = loadedAssets.ToArray();
                    cache[ID] = assetArray; // Cache the result
                    return assetArray;
                } else {
                    Debug.LogWarning($"(Warning) AddressableManager: Failed to load assets of type {typeof(T).Name} with ID: '{ID}'.");
                    return null;
                }
            } finally {
                // Optionally release the handle if you're done with it
                Addressables.Release(asyncHandle);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static Dictionary<string, T[]> GetOrCreateArrayCache<T>() where T : UnityEngine.Object {
            // Check if a dictionary for type `T` exists
            if (!assetDictionaries.TryGetValue(typeof(T[]), out var dictionary)) {
                // Create a new dictionary if it doesn't exist
                dictionary = new Dictionary<string, T[]>();
                assetDictionaries[typeof(T[])] = dictionary;
            }

            // Safely cast the object to the correct dictionary type
            return (Dictionary<string, T[]>)dictionary;
        }

        private static Dictionary<string, T> GetOrCreateCache<T>() where T : UnityEngine.Object {
            // Check if a dictionary for type `T` exists
            if (!assetDictionaries.TryGetValue(typeof(T), out var dictionary)) {
                // Create a new dictionary if it doesn't exist
                dictionary = new Dictionary<string, T>();
                assetDictionaries[typeof(T)] = dictionary;
            }

            // Safely cast the object to the correct dictionary type
            return (Dictionary<string, T>)dictionary;
        }

        #endregion Private Methods
    }
}
