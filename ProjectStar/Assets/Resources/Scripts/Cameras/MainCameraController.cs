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

        MainCameraControllerVariables.newRotation = transform.rotation;


        MainCameraControllerVariables.dragStartPosition = default;
        MainCameraControllerVariables.dragCurrentPosition = default;
        MainCameraControllerVariables.rotateStartPosition = default;
        MainCameraControllerVariables.rotateCurrentPosition = default;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCameraControllerVariables.followTransform != null)
        {            
            transform.position = Vector3.Lerp(transform.position, MainCameraControllerVariables.followTransform.position, MainCameraControllerVariables.speed * Time.deltaTime);
            //transform.position = MainCameraControllerVariables.followTransform.position;
        }

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

        if (Input.GetKey(KeyCode.R))
        {
            MainCameraControllerVariables.newZoom += MainCameraControllerVariables.zoomAmount;
        }
        if (Input.GetKey(KeyCode.T))
        {
            MainCameraControllerVariables.newZoom -= MainCameraControllerVariables.zoomAmount;
        }

        MainCameraControllerVariables.cameraTransform.localPosition = Vector3.Lerp(MainCameraControllerVariables.cameraTransform.localPosition, MainCameraControllerVariables.newZoom, MainCameraControllerVariables.speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.E))
        {
            MainCameraControllerVariables.newRotation *= Quaternion.Euler(Vector3.up * -MainCameraControllerVariables.rotationAmount);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            MainCameraControllerVariables.newRotation *= Quaternion.Euler(Vector3.up * MainCameraControllerVariables.rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, MainCameraControllerVariables.newRotation, Time.deltaTime * MainCameraControllerVariables.speed);


        MouseInput();

    }

    //public void UnlockCamera()
    //{
    //    MainCameraControllerVariables.isLocked = false;
    //    MainCameraControllerVariables.followTransform = null;
    //}

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


}
