using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ListList))]
public class ListEditor : Editor
{
    SerializedProperty integers, strings;

    void OnEnable()
    {
        integers = serializedObject.FindProperty("integers");
        strings = serializedObject.FindProperty("strings");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(integers);
        EditorGUILayout.PropertyField(strings);
        serializedObject.ApplyModifiedProperties();
    }
}