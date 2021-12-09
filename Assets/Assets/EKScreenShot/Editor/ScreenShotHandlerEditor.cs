using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ScreenShotHandler))]
public class ScreenShotHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ScreenShotHandler handler = (ScreenShotHandler)target;
        SerializedObject so = new SerializedObject(handler);
        DimensionSettings(so, handler);
        Key(so);
        Camera(so);
        Path(so, handler);
        TakeScreenShot(handler);

        so.ApplyModifiedProperties();
    }

    private void DimensionSettings(SerializedObject so, ScreenShotHandler handler)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(so.FindProperty("device"));
        if (handler.device == DeviceInfo.custom)
        {
            EditorGUILayout.PropertyField(so.FindProperty("width"));
            EditorGUILayout.PropertyField(so.FindProperty("height"));
        }

        GUILayout.EndVertical();
    }

    private void Path(SerializedObject so, ScreenShotHandler handler)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(so.FindProperty("path"));
        if (GUILayout.Button("Select Path"))
        {
            handler.path = EditorUtility.OpenFolderPanel("Select Directory", "", "") + "/";
        }

        GUILayout.EndVertical();
    }

    private void Camera(SerializedObject so)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(so.FindProperty("cam"));

        GUILayout.EndVertical();
    }

    private void TakeScreenShot(ScreenShotHandler handler)
    {
        GUILayout.Space(50);
        GUILayout.BeginVertical();

        if (GUILayout.Button("Take Screenshot", GUILayout.Height(50)))
        {
            handler.TakeScreenShot();
        }

        GUILayout.EndVertical();
    }

    private void Key(SerializedObject so)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.PropertyField(so.FindProperty("screenshotKey"));

        GUILayout.EndVertical();
    }

    private Type GetGameViewType()
    {
        Assembly unityEditorAssembly = typeof(EditorWindow).Assembly;
        Type gameViewType = unityEditorAssembly.GetType("UnityEditor.GameView");
        return gameViewType;
    }

    private EditorWindow GetGameViewWindow(Type gameViewType)
    {
        Object[] obj = Resources.FindObjectsOfTypeAll(gameViewType);
        if (obj.Length > 0)
        {
            return obj[0] as EditorWindow;
        }
        return null;
    }
}