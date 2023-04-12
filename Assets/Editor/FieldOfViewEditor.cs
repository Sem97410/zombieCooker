using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Zombie))]
public class FieldOfViewEditor : Editor
{
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
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
