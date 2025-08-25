#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace IndexedGameData {
    public static class PropertyDrawerExtensions {
        public static Rect HorizontalFieldPosition(this PropertyDrawer propertyDrawer, Rect origin, int i, int numItems) {
            float widthPerProperty = (float)origin.width / numItems;

            float spacer = 0.05f * widthPerProperty;
            float propertyWidth = widthPerProperty - (1f - 1f / numItems) * spacer;
            return new Rect(origin.x + i * (propertyWidth + spacer), origin.y, propertyWidth, origin.height);
        }

        public static void HorizontalPropertyField(this PropertyDrawer propertyDrawer, Rect origin, SerializedProperty property, GUIContent label, int i, int numItems) {
            GUIStyle GUIStyle = GUI.skin.box;
            EditorGUIUtility.labelWidth = GUIStyle.CalcSize(label).x;
            Rect fieldPosition = HorizontalFieldPosition(propertyDrawer, origin, i, numItems);
            EditorGUI.PropertyField(fieldPosition, property, label);
        }

        public static void HorizontalSlider(this PropertyDrawer propertyDrawer, Rect origin, SerializedProperty property, float valueMin, float valueMax, GUIContent label, int i, int numItems) {
            GUIStyle GUIStyle = GUI.skin.box;
            EditorGUIUtility.labelWidth = GUIStyle.CalcSize(label).x;
            Rect fieldPosition = HorizontalFieldPosition(propertyDrawer, origin, i, numItems);
            EditorGUI.Slider(fieldPosition, property, valueMin, valueMax, label);
        }
        
        public static Rect VerticalFieldPosition(this PropertyDrawer propertyDrawer, Rect origin, int i, int numItems, float offsetY) {
            //float heightPerProperty = (float)origin.height / numItems;
            float heightPerProperty = 18f;

            float spacer = 0.05f * heightPerProperty;
            float propertyHeight = heightPerProperty - (1f - 1f / numItems) * spacer;
            return new Rect(origin.x, origin.y + i * (propertyHeight + spacer) + offsetY, origin.width, propertyHeight);
        }

        public static void VerticalPropertyField(this PropertyDrawer propertyDrawer, Rect origin, SerializedProperty property, GUIContent label, int i, int numItems, float labelWidth, float offsetY) {
            EditorGUIUtility.labelWidth = labelWidth;
            Rect fieldPosition = VerticalFieldPosition(propertyDrawer, origin, i, numItems, offsetY);
            EditorGUI.PropertyField(fieldPosition, property, label);
        }

    }
}

#endif