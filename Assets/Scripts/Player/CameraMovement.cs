using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerInput input;

    [Header("Camera Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private float camXRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        camXRotation += Time.deltaTime * input.mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -40f, 40f);

        transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }
}
