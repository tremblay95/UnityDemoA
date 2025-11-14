using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ScratchPad.SubclassSelectorAttribute
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var baseType = ((SubclassSelectorAttribute)attribute).BaseType;
            // Build the derived type list
            var derivedTypes = TypeCacheUtility.GetSubclassesOf(baseType);
            
            if (derivedTypes.Count == 0)
            {
                EditorGUILayout.HelpBox($"No concrete subclasses of {baseType.Name} found.", MessageType.Warning);
                return;
            }
            
            var displayNames = TypeCacheUtility.GetSubclassNames(baseType);
            
            // Retrieve current object instance
            var currentValue = property.managedReferenceValue;
            int currentIndex = currentValue != null ? derivedTypes.IndexOf(currentValue.GetType()) : -1;

            if (currentIndex == -1)
            {
                property.managedReferenceValue = Activator.CreateInstance(derivedTypes[0]);
                currentIndex = 0;
            }
            
            // Draw the popup
            int selectedIndex = EditorGUILayout.Popup(label.text, currentIndex, displayNames);
            
            // Handle type change
            if (selectedIndex != currentIndex)
            {
                var selectedType = derivedTypes[selectedIndex];
                property.managedReferenceValue = Activator.CreateInstance(selectedType);
            }
            
            // Draw child fields
            if (property.managedReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                SerializedProperty iterator = property.Copy();
                SerializedProperty endProperty = iterator.GetEndProperty();

                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
                {
                    EditorGUILayout.PropertyField(iterator, true);
                    enterChildren = false;
                }
                EditorGUI.indentLevel--;
            }
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0f;
    }
}






















