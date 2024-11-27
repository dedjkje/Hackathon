using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 0.3f; // Чувствительность мыши
    public float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали
    PhotonView view;
    private float rotationX = 0.0f;
    public CameraControllerPanel cameraControllerPanel;
    private void Start()
    {
        view = transform.parent.GetComponent<PhotonView>();
    }
    private void Update()
    {
        
        float mouseX = 0;
        float mouseY = 0;

        if (cameraControllerPanel.pressed)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == cameraControllerPanel.fingerId)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        mouseY = touch.deltaPosition.y * sensitivity;
                        mouseX = touch.deltaPosition.x * sensitivity;
                    }
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        mouseY = 0;
                        mouseX = 0;
                    }
                }
            }
        }
        // Вращаем персонажа в горизонтальной плоскости
        transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

        // Вращаем камеру в вертикальной плоскости
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);

    }
}
