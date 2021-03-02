using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 10f;

    public Transform PlayerBody;

    public GameObject AttackEnd;

    float Xrotate = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        AttackEnd.transform.position.Set(MouseX, MouseY, 0f);

        Xrotate -= MouseY;
        Xrotate = Mathf.Clamp(Xrotate, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotate, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * MouseX);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }
}
