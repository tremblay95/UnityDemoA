using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace UnityDemoA
{
    [CustomEditor(typeof(StateInjectionSequenceExecutionStrategy))]
    public class StateInjectionSequenceExecutionStrategy_Inspector : Editor
    {
        private ReorderableList stepsList;
        private readonly string stepsListName = "stateSequence";

        private void OnEnable()
        {
            stepsList = new ReorderableList(serializedObject, serializedObject.FindProperty(stepsListName), true, true, true, true);

            stepsList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "State Sequence");
            };

            stepsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = stepsList.serializedProperty.GetArrayElementAtIndex(index);
                var name = element.FindPropertyRelative("name").stringValue;
                rect.y += 2;
                
                // Shift rect to the right to make room for the type dropdown button
                rect.x += 10;
                rect.width -= 10;
                
                EditorGUI.PropertyField(rect, element, new GUIContent(name), true);
            };

            stepsList.elementHeightCallback = (int index) =>
            {
                var element = stepsList.serializedProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(element, true) + 4;
            };

            // Replace "+" button with type-selection dropdown
            stepsList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
            {
                GenericMenu menu = new GenericMenu();
                
                // Find all non-abstract subclasses of TransitionStepData
                var stepTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(t => typeof(AbilityStateData).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var type in stepTypes)
                {
                    // Use the type name in the menu
                    menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(type.Name)), false, () =>
                    {
                        AddStep(type);
                    });
                }
                
                
                menu.ShowAsContext();
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            stepsList.DoLayoutList();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void AddStep(Type type)
        {
            var strategy = (StateInjectionSequenceExecutionStrategy)target;

            var newStep = Activator.CreateInstance(type);
            strategy.AddStep(newStep);
            EditorUtility.SetDirty(strategy);

            serializedObject.Update();
            
            var stepsProp = serializedObject.FindProperty(stepsListName);
            stepsProp.isExpanded = true;
            
            if (stepsProp.arraySize > 0)
            {
                var last = stepsProp.GetArrayElementAtIndex(stepsProp.arraySize - 1);
                last.isExpanded = true;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
