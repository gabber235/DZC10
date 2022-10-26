using System;
using System.Collections.Generic;
using Tutorial;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Editor
{
    [HelpURL(
        "https://forum.unity.com/threads/finally-a-serializable-dictionary-for-unity-extracted-from-system-collections-generic.335797/page-2")]
    public abstract class DictionaryDrawer<TK, TV> : PropertyDrawer
    {
        private const float KButtonWidth = 22f;
        private const int KMargin = 3;

        private static readonly GUIContent IconToolbarMinus =
            EditorGUIUtility.IconContent("Toolbar Minus", "Remove selection from list");

        private static readonly GUIContent
            IconToolbarPlus = EditorGUIUtility.IconContent("Toolbar Plus", "Add to list");

        private static readonly GUIStyle PreButton = "RL FooterButton";
        private static readonly GUIStyle BoxBackground = "RL Background";

        private static readonly Dictionary<Type, Func<Rect, object, object>> Fields =
            new()
            {
                { typeof(int), (rect, value) => EditorGUI.IntField(rect, (int)value) },
                { typeof(float), (rect, value) => EditorGUI.FloatField(rect, (float)value) },
                { typeof(string), (rect, value) => EditorGUI.TextField(rect, (string)value) },
                { typeof(bool), (rect, value) => EditorGUI.Toggle(rect, (bool)value) },
                { typeof(Vector2), (rect, value) => EditorGUI.Vector2Field(rect, GUIContent.none, (Vector2)value) },
                { typeof(Vector3), (rect, value) => EditorGUI.Vector3Field(rect, GUIContent.none, (Vector3)value) },
                { typeof(Bounds), (rect, value) => EditorGUI.BoundsField(rect, (Bounds)value) },
                { typeof(Rect), (rect, value) => EditorGUI.RectField(rect, (Rect)value) }
            };

        private SerializableDictionary<TK, TV> _dictionary;
        private bool _foldout;


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            CheckInitialize(property, label);
            if (_foldout)
                return Mathf.Max((_dictionary.Count + 1) * 17f, 17 + 16) + KMargin * 2;
            return 17f + KMargin * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CheckInitialize(property, label);

            var backgroundRect = position;
            backgroundRect.xMin -= 7;
            backgroundRect.height += KMargin;
            if (Event.current.type == EventType.Repaint)
                BoxBackground.Draw(backgroundRect, false, false, false, false);

            position.y += KMargin;
            position.height = 17f;

            var foldoutRect = position;
            foldoutRect.width -= 2 * KButtonWidth;
            EditorGUI.BeginChangeCheck();
            _foldout = EditorGUI.Foldout(foldoutRect, _foldout, label, true);
            if (EditorGUI.EndChangeCheck())
                EditorPrefs.SetBool(label.text, _foldout);

            position.xMin += KMargin;
            position.xMax -= KMargin;

            var buttonRect = position;
            buttonRect.xMin = position.xMax - KButtonWidth;

            if (GUI.Button(buttonRect, IconToolbarMinus, PreButton)) ClearDictionary();

            buttonRect.x -= KButtonWidth - 1;

            if (GUI.Button(buttonRect, IconToolbarPlus, PreButton)) AddNewItem();

            if (!_foldout)
                return;

            var labelRect = position;
            labelRect.y += 16;
            if (_dictionary.Count == 0)
                GUI.Label(labelRect, "This dictionary doesn't have any items. Click + to add one!");

            foreach (var item in _dictionary)
            {
                var key = item.Key;
                var value = item.Value;

                position.y += 17f;

                var keyRect = position;
                keyRect.width /= 2;
                keyRect.width -= 4;
                EditorGUI.BeginChangeCheck();
                var newKey = DoField(keyRect, typeof(TK), key);
                if (EditorGUI.EndChangeCheck())
                {
                    try
                    {
                        _dictionary.Remove(key);
                        _dictionary.Add(newKey, value);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }

                    break;
                }

                var valueRect = position;
                valueRect.xMin = keyRect.xMax;
                valueRect.xMax = position.xMax - KButtonWidth;
                EditorGUI.BeginChangeCheck();
                value = DoField(valueRect, typeof(TV), value);
                if (EditorGUI.EndChangeCheck())
                {
                    _dictionary[key] = value;
                    break;
                }

                var removeRect = position;
                removeRect.xMin = removeRect.xMax - KButtonWidth;
                if (GUI.Button(removeRect, IconToolbarMinus, PreButton))
                {
                    RemoveItem(key);
                    break;
                }
            }
        }

        private void RemoveItem(TK key)
        {
            _dictionary.Remove(key);
        }

        private void CheckInitialize(SerializedProperty property, GUIContent label)
        {
            if (_dictionary != null) return;
            var target = property.serializedObject.targetObject;
            _dictionary = fieldInfo.GetValue(target) as SerializableDictionary<TK, TV>;
            if (_dictionary == null)
            {
                _dictionary = new SerializableDictionary<TK, TV>();
                fieldInfo.SetValue(target, _dictionary);
            }

            _foldout = EditorPrefs.GetBool(label.text);
        }

        private static T DoField<T>(Rect rect, Type type, T value)
        {
            if (Fields.TryGetValue(type, out var field))
                return (T)field(rect, value);

            if (type.IsEnum)
                return (T)(object)EditorGUI.EnumPopup(rect, (Enum)(object)value);

            if (typeof(UnityObject).IsAssignableFrom(type))
                return (T)(object)EditorGUI.ObjectField(rect, (UnityObject)(object)value, type, true);

            Debug.LogError("Type is not supported: " + type);
            return value;
        }

        private void ClearDictionary()
        {
            _dictionary.Clear();
        }

        private void AddNewItem()
        {
            TK key;
            if (typeof(TK) == typeof(string))
                key = (TK)(object)"";
            else key = default;

            var value = default(TV);
            try
            {
                if (key != null) _dictionary.Add(key, value);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    [CustomPropertyDrawer(typeof(StepInfoDictionary))]
    public class StepInfoDictionaryDrawer : DictionaryDrawer<TutorialStep, TutorialStepInfo>
    {
    }
}