using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Zombie))]
public class EnemyStateEditor : Editor
{
    Zombie _zombie;
    SerializedProperty _zombieWaypoints;
    private void OnEnable()
    {
        _zombie = (Zombie)target;
        _zombieWaypoints = serializedObject.FindProperty("_waypoints");
    }

    public override void OnInspectorGUI()
    {
        _zombie._zombieState = (Zombie.ZombieState)EditorGUILayout.EnumPopup("Zombie State", _zombie._zombieState);
        switch (_zombie._zombieState)
        {
            case Zombie.ZombieState.Guard:
                {
                    _zombie._target = (Transform)EditorGUILayout.ObjectField("Target", _zombie._target, typeof(Transform), true);
                    break;
                }
            case Zombie.ZombieState.Waypoint:
                {
                    _zombie._target = (Transform)EditorGUILayout.ObjectField("Target", _zombie._target, typeof(Transform), true);
                    EditorGUILayout.PropertyField(_zombieWaypoints, new GUIContent("Waypoints"));
                    break;
                }
        }
    }
}
