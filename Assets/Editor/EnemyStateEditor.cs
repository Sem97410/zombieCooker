using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Zombie))]
public class EnemyStateEditor : Editor
{
    Zombie m_zombie;
    SerializedProperty m_zombieTarget;
    SerializedProperty m_zombieRadius;
    SerializedProperty m_zombieAngle;
    SerializedProperty m_zombiePlayerRef;
    SerializedProperty m_zombieWaypoints;
    SerializedProperty m_zombieWalkRadius;
    SerializedProperty m_zombieTargetMask;
    SerializedProperty m_zombieObstructionMask;
    
    private void OnEnable()
    {
        m_zombie = (Zombie)target;
        m_zombieWaypoints = serializedObject.FindProperty("_waypoints");
        m_zombieTarget = serializedObject.FindProperty("_target");
        m_zombieRadius = serializedObject.FindProperty("_radius");
        m_zombieAngle = serializedObject.FindProperty("_angle");
        m_zombiePlayerRef = serializedObject.FindProperty("_playerRef");
        m_zombieWalkRadius = serializedObject.FindProperty("_walkRadius");
        m_zombieTargetMask = serializedObject.FindProperty("_targetMask");
        m_zombieObstructionMask = serializedObject.FindProperty("_ObstructionMask");
    }

    public override void OnInspectorGUI()
    {
        m_zombie._zombieState = (Zombie.ZombieState)EditorGUILayout.EnumPopup("Zombie State", m_zombie._zombieState);
        serializedObject.FindProperty("_zombieState").enumValueIndex = (int)m_zombie._zombieState;
        serializedObject.ApplyModifiedProperties();

        switch (m_zombie._zombieState)
        {
            case Zombie.ZombieState.Guard:
                {
                    EditorGUILayout.PropertyField(m_zombieTarget, new GUIContent("Target"));
                    EditorGUILayout.PropertyField(m_zombieRadius, new GUIContent("Radius"));
                    EditorGUILayout.Slider(m_zombieAngle, 0, 360, new GUIContent("Angle"));
                    EditorGUILayout.PropertyField(m_zombiePlayerRef, new GUIContent("PlayerRef"));
                    EditorGUILayout.PropertyField(m_zombieTargetMask, new GUIContent("Target Mask"));
                    EditorGUILayout.PropertyField(m_zombieObstructionMask, new GUIContent("Obstruction Mask"));
                    serializedObject.ApplyModifiedProperties();
                    break;
                }
            case Zombie.ZombieState.Waypoint:
                {
                    EditorGUILayout.PropertyField(m_zombieTarget, new GUIContent("Target"));
                    EditorGUILayout.PropertyField(m_zombieRadius, new GUIContent("Radius"));
                    EditorGUILayout.Slider(m_zombieAngle, 0, 360, new GUIContent("Angle"));
                    EditorGUILayout.PropertyField(m_zombiePlayerRef, new GUIContent("PlayerRef"));
                    EditorGUILayout.PropertyField(m_zombieWaypoints, new GUIContent("Waypoints"));
                    EditorGUILayout.PropertyField(m_zombieTargetMask, new GUIContent("Target Mask"));
                    EditorGUILayout.PropertyField(m_zombieObstructionMask, new GUIContent("Obstruction Mask"));
                    serializedObject.ApplyModifiedProperties();
                    break;
                }
            case Zombie.ZombieState.Random:
                {
                    EditorGUILayout.PropertyField(m_zombieTarget, new GUIContent("Target"));
                    EditorGUILayout.PropertyField(m_zombieWalkRadius, new GUIContent("Walk Radius"));
                    EditorGUILayout.PropertyField(m_zombieRadius, new GUIContent("Radius"));
                    EditorGUILayout.Slider(m_zombieAngle, 0, 360, new GUIContent("Angle"));
                    EditorGUILayout.PropertyField(m_zombiePlayerRef, new GUIContent("PlayerRef"));
                    EditorGUILayout.PropertyField(m_zombieTargetMask, new GUIContent("Target Mask"));
                    EditorGUILayout.PropertyField(m_zombieObstructionMask, new GUIContent("Obstruction Mask"));
                    serializedObject.ApplyModifiedProperties();
                    break;
                }
        }
    }

    private void OnSceneGUI()
    {
        Zombie _zombieFOV = (Zombie)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(_zombieFOV.transform.position, Vector3.up, Vector3.forward, 360, _zombieFOV._radius);

        Vector3 viewAngle01 = DirectionFromAngle(_zombieFOV.transform.eulerAngles.y, -_zombieFOV._angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(_zombieFOV.transform.eulerAngles.y, _zombieFOV._angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(_zombieFOV.transform.position, _zombieFOV.transform.position + viewAngle01 * _zombieFOV._radius);
        Handles.DrawLine(_zombieFOV.transform.position, _zombieFOV.transform.position + viewAngle02 * _zombieFOV._radius);

        if (_zombieFOV._canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(_zombieFOV.transform.position, _zombieFOV._playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
