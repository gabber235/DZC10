using Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ItemAttribute))]
    public class ItemDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var items = ItemType.Values;
            // Make a drop down list of all the item types
            var currentIndex = items.FindIndex(item => item.Name == property.stringValue);

            if (currentIndex == -1)
            {
                currentIndex = 0;
                property.stringValue = items[currentIndex].Name;
            }

            EditorGUI.BeginChangeCheck();
            var itemIndex = EditorGUI.Popup(
                new Rect(position.x, position.y, 100, position.height),
                currentIndex,
                items.ConvertAll(item => item.Name).ToArray()
            );
            if (EditorGUI.EndChangeCheck()) property.stringValue = items[itemIndex].Name;


            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}