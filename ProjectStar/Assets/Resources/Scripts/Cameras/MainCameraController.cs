using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("INSERT MAIN CAMERA CONTROLLER SO :")]
    public MainCameraController_SO MainCameraControllerVariables;


    public Transform cameraPrefab;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraControllerVariables.cameraTransform = cameraPrefab;
        MainCameraControllerVariables.isLocked = true;

        MainCameraControllerVariables.newRotation = transform.rotation;


        MainCameraControllerVariables.dragStartPosition = default;
        MainCameraControllerVariables.dragCurrentPosition = default;
        MainCameraControllerVariables.rotateStartPosition = default;
        MainCameraControllerVariables.rotateCurrentPosition = default;
    }

    // Update is called once per frame
    void Update()
    {
        BindCameraToCharacter();
        CameraMoveKeyboardInput();
        CameraZoomKeyboardInput();

        MainCameraControllerVariables.cameraTransform.localPosition = Vector3.Lerp(MainCameraControllerVariables.cameraTransform.localPosition, MainCameraControllerVariables.newZoom, MainCameraControllerVariables.speed * Time.deltaTime);
        
        CameraRotationKeyBoardInput();

        transform.rotation = Quaternion.Lerp(transform.rotation, MainCameraControllerVariables.newRotation, Time.deltaTime * MainCameraControllerVariables.speed);

        if (MainCameraControllerVariables.canMouseInput)
        {
            MouseInput();
        }
    }

    private void CameraRotationKeyBoardInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            MainCameraControllerVariables.newRotation *= Quaternion.Euler(Vector3.up * -MainCameraControllerVariables.rotationAmount);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            MainCameraControllerVariables.newRotation *= Quaternion.Euler(Vector3.up * MainCameraControllerVariables.rotationAmount);
        }
    }

    private void CameraZoomKeyboardInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            MainCameraControllerVariables.newZoom += MainCameraControllerVariables.zoomAmount;
        }
        if (Input.GetKey(KeyCode.T))
        {
            MainCameraControllerVariables.newZoom -= MainCameraControllerVariables.zoomAmount;
        }
    }

    private void CameraMoveKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MainCameraControllerVariables.UnlockCamera();
            transform.position += (transform.forward * MainCameraControllerVariables.speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MainCameraControllerVariables.UnlockCamera();
            transform.position += (-transform.right * MainCameraControllerVariables.speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MainCameraControllerVariables.UnlockCamera();
            transform.position += (transform.right * MainCameraControllerVariables.speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MainCameraControllerVariables.UnlockCamera();
            transform.position += (-transform.forward * MainCameraControllerVariables.speed * Time.deltaTime);
        }
    }

    private void BindCameraToCharacter()
    {
        if (MainCameraControllerVariables.followTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, MainCameraControllerVariables.followTransform.position, MainCameraControllerVariables.speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, MainCameraControllerVariables.followTransform.position) <= 0.5f)
            {
                MainCameraControllerVariables.canMouseInput = true;
            }
        }
    }

    

    public void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = MainCameraControllerVariables.cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                MainCameraControllerVariables.dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = MainCameraControllerVariables.cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                MainCameraControllerVariables.dragCurrentPosition = ray.GetPoint(entry);

                transform.position = transform.position + (MainCameraControllerVariables.dragStartPosition - MainCameraControllerVariables.dragCurrentPosition);

                DragMouseToUnlockCamera();
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            MainCameraControllerVariables.newZoom += Input.mouseScrollDelta.y * MainCameraControllerVariables.zoomAmount;
        }

        if (Input.GetMouseButtonDown(2))
        {
            MainCameraControllerVariables.rotateStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            MainCameraControllerVariables.rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = MainCameraControllerVariables.rotateStartPosition - MainCameraControllerVariables.rotateCurrentPosition;

            MainCameraControllerVariables.rotateStartPosition = MainCameraControllerVariables.rotateCurrentPosition;

            MainCameraControllerVariables.newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    public void DragMouseToUnlockCamera()
    {
        if (Vector3.Distance(MainCameraControllerVariables.dragCurrentPosition, MainCameraControllerVariables.dragStartPosition) > 1.0f)
        {
            MainCameraControllerVariables.UnlockCamera();
        }
    }


}
