using System;
using UnityEditor;
using UnityEngine;

namespace Utility
{
    [CustomPropertyDrawer(typeof(RequireInterface))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is RequireInterface requireInterface)
            {
                var requireTypes = requireInterface.RequireTypes;

                foreach (var type in requireTypes)
                {
                    if (IsValid(property, type))
                    {
                        label.tooltip = "Require " + requireInterface.RequireTypes + " interface";
                        CheckProperty(property, type);
                    }
                }
            }

            EditorGUI.PropertyField(position, property, label);
        }

        private bool IsValid(SerializedProperty property, Type targetType)
        {
            return targetType.IsInterface && property.propertyType == SerializedPropertyType.ObjectReference;
        }

        private void CheckProperty(SerializedProperty property, Type targetType)
        {
            if (property.objectReferenceValue == null)
                return;
            if (property.objectReferenceValue as GameObject)
                CheckGameObject(property, targetType);
            else if (property.objectReferenceValue as ScriptableObject)
                CheckScriptableObject(property, targetType);
        }

        private void CheckGameObject(SerializedProperty property, Type targetType)
        {
            var field = property.objectReferenceValue as GameObject;

            if (field.GetComponent(targetType) != null)
                return;

            property.objectReferenceValue = null;
            Debug.LogError("GameObject must contain component implemented " + targetType + " interface");
        }

        private void CheckScriptableObject(SerializedProperty property, Type targetType)
        {
            var field = property.objectReferenceValue as ScriptableObject;
            var fieldType = field.GetType();

            if (targetType.IsAssignableFrom(fieldType))
                return;

            property.objectReferenceValue = null;
            Debug.LogError("ScriptableObject must implement " + targetType + " interface");
        }
    }
}