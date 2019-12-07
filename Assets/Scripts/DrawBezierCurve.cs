using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class DrawBezierCurve : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurve be = target as BezierCurve;

        be.startPoint = Handles.PositionHandle(be.startPoint, Quaternion.identity);
        be.endPoint = Handles.PositionHandle(be.endPoint, Quaternion.identity);
        be.startTangent = Handles.PositionHandle(be.startTangent, Quaternion.identity);
        be.endTangent = Handles.PositionHandle(be.endTangent, Quaternion.identity);

        Handles.DrawBezier(be.startPoint, be.endPoint, be.startTangent, be.endTangent, Color.red, null, 2f);
    }
    private void OnSceneGUI()
    {
        //SceneView.duringSceneGui += OnSceneViewGUI;
    }
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += OnSceneViewGUI;
    }

    void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= OnSceneViewGUI;
    }
}