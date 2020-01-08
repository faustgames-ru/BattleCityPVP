using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CoreUtils.EnumsUtils.Editor
{
    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var field = GetField(property);
            var names = new List<string>();
            if (field == null) return;
            for (var i = 0; i < 31; i++)
            {
                var value = 1 << i;
                var valueName = Enum.GetName(field.FieldType, value);
                if (string.IsNullOrEmpty(valueName)) continue;
                names.Add(valueName);
            }
            property.intValue = EditorGUI.MaskField(position, label, property.intValue, names.ToArray());
            

            // bug with "None"
            //property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
        }

        private static FieldInfo GetFieldViaPath(Type type, string path)
        {
            Type parentType = type;
            FieldInfo fi = type.GetField(path);
            var perDot = path.Split('.');
            foreach (string fieldName in perDot)
            {
                fi = parentType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (fi != null)
                    parentType = fi.FieldType;
                else
                    return null;
            }
            if (fi != null)
                return fi;
            return null;
        }

        private static FieldInfo GetField(SerializedProperty property)
        {
            Type parentType = property.serializedObject.targetObject.GetType();
            return GetFieldViaPath(parentType, property.propertyPath);
        }
    }
}