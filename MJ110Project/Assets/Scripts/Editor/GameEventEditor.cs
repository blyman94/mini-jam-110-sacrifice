using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
[CanEditMultipleObjects]
public class GameEventEditor : Editor
{
    SerializedProperty listenersProperty;

    private void OnEnable()
    {
        listenersProperty = serializedObject.FindProperty("listeners");
    }

    public override void OnInspectorGUI()
    {
        GameEvent gameEvent = target as GameEvent;

        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script",
            MonoScript.FromScriptableObject((ScriptableObject)target),
            GetType(), false);

        
        using (new EditorGUI.DisabledGroupScope(true)) EditorGUILayout.PropertyField(listenersProperty);

        EditorGUILayout.LabelField("Testing", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Use this button to manually raise this " +
            "event while in Play Mode.");
        if (GUILayout.Button("Raise"))
        {
            if (Application.isPlaying)
            {
                gameEvent.Raise();
            }
        }
    }
}
