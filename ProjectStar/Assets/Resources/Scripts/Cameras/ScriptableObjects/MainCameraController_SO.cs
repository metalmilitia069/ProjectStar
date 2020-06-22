using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCameraVariables", menuName = "ScriptableVariables/Type: MainCameraController")]
public class MainCameraController_SO : ScriptableObject
{
    public Transform followTransform;
    public Transform cameraTransform;

    public bool isLocked = true;
    public float speed = 5.0f;
    public float movementTime;
    public float rotationAmount;

    public Vector3 zoomAmount;
    public Vector3 newZoom;
    public Vector3 newPosition;
    public Quaternion newRotation;



    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public void UnlockCamera()
    {
        isLocked = false;
        followTransform = null;
    }

    public void LockCamera(Transform transform)
    {
        isLocked = true;
        followTransform = transform;
    }

}
