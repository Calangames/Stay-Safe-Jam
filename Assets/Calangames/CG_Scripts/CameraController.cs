using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform pivotTransform;

    private Vector3 cameraRotation = new Vector3(0, 50, 0);

    private float cameraDistance = 25f;

    public float mouseSensitivity = 4f;
    public float scrollSensitivity = 2f;
    public float orbitDampening = 10f;
    public float scrollDampening = 6f;

    public bool cameraDisabled = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = transform;
        pivotTransform = transform.parent;


    }

    void LateUpdate()
    {
        if (!cameraDisabled)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                cameraRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                cameraRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                cameraRotation.y = Mathf.Clamp(cameraRotation.y, 30f, 60f);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

                scrollAmount *= cameraDistance * 0.3f;

                cameraDistance -= scrollAmount;

                cameraDistance = Mathf.Clamp(cameraDistance, 20f, 35f);
            }
        }

        Quaternion targetRotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0);

        pivotTransform.rotation = Quaternion.Lerp(pivotTransform.rotation, targetRotation, Time.deltaTime * orbitDampening);

        if (cameraTransform.localPosition.z != -cameraDistance)
        {
            cameraTransform.localPosition = new Vector3(0f, 2f, Mathf.Lerp(cameraTransform.localPosition.z, -cameraDistance, Time.deltaTime * scrollDampening));
        }
    }
}