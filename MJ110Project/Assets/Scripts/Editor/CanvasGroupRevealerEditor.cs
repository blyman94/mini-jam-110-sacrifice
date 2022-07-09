using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CanvasGroupRevealer))]
[CanEditMultipleObjects]
public class CanvasGroupRevealerEditor : Editor
{
    private SerializedProperty startHiddenProperty;
    private SerializedProperty canvasFadeInProperty;
    private SerializedProperty canvasFadeOutProperty;
    private SerializedProperty fadeTimeProperty;
    private SerializedProperty fadeCurveProperty;
    private SerializedProperty shownAlphaProperty;
    private SerializedProperty shownBlockRaycastProperty;
    private SerializedProperty shownInteractableProperty;

    private void OnEnable()
    {
        startHiddenProperty = serializedObject.FindProperty("startHidden");
        canvasFadeInProperty = serializedObject.FindProperty("CanvasFadeIn");
        canvasFadeOutProperty = serializedObject.FindProperty("CanvasFadeOut");
        fadeTimeProperty = serializedObject.FindProperty("fadeTime");
        fadeCurveProperty = serializedObject.FindProperty("fadeCurve");
        shownAlphaProperty = serializedObject.FindProperty("shownAlpha");
        shownBlockRaycastProperty =
            serializedObject.FindProperty("shownBlockRaycast");
        shownInteractableProperty =
            serializedObject.FindProperty("shownInteractable");
    }

    public override void OnInspectorGUI()
    {
        var canvasGroupRevealer = target as CanvasGroupRevealer;

        // Draw the Unity-standard script field atop the inspector.
        using (new EditorGUI.DisabledScope(true))
            EditorGUILayout.ObjectField("Script",
            MonoScript.FromMonoBehaviour((MonoBehaviour)target),
            GetType(), false);

        EditorGUILayout.PropertyField(startHiddenProperty);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Fade Behaviour", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(canvasFadeInProperty);
        EditorGUILayout.PropertyField(canvasFadeOutProperty);

        if (canvasGroupRevealer.CanvasFadeIn ||
            canvasGroupRevealer.CanvasFadeOut)
        {
            EditorGUILayout.PropertyField(fadeTimeProperty);
            EditorGUILayout.PropertyField(fadeCurveProperty);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Shown State Properties",
            EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(shownAlphaProperty);
        EditorGUILayout.PropertyField(shownBlockRaycastProperty);
        EditorGUILayout.PropertyField(shownInteractableProperty);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Testing", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("While in Play Mode, use these buttons " + 
            "to test show and hide behaviour.");

        if (GUILayout.Button("Show"))
        {
            if (Application.isPlaying)
            {
                canvasGroupRevealer.ShowGroup();
            }
        }

        if (GUILayout.Button("Hide"))
        {
            if (Application.isPlaying)
            {
                canvasGroupRevealer.HideGroup();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
