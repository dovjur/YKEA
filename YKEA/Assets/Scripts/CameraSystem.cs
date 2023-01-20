using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float sensitivity = 5f;

    private float rotationX;
    private float rotationY;

    public bool reset = false;

    private void Update()
    {
        RotateCamera();
        ZoomCamera();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            float inputX = Input.GetAxis("Mouse X") * sensitivity;
            float inputY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX += inputY;
            rotationY += inputX;

            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }

    private void ZoomCamera()
    {
        float input = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = Camera.main.orthographicSize - input;
        newZoom = Mathf.Clamp(newZoom, minZoom,maxZoom);
        Camera.main.orthographicSize = newZoom;
    }

    public void Reset()
    {
        transform.localEulerAngles = new Vector3(30,45,0);
        Camera.main.orthographicSize = 5;
    }


}
