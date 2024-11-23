using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 0.3f; // Чувствительность мыши
    public float maxYAngle = 80.0f; // Максимальный угол вращения по вертикали
    public GameObject playerCamera;
    
    private float rotationX = 0.0f;
    public CameraControllerPanel cameraControllerPanel;

    public float rayLength = 5f;

    public GameObject gameInterface_1;

    public GameObject canonInterface_1;
    public GameObject canonButtons_1;
    public GameObject canonCamera_1;

    private void Start()
    {
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

        shootRayCanon_1();
    }

    void shootRayCanon_1()
    {
        Vector3 rayStartPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = new Ray(rayStartPos, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.name == "Canon_1")
            {
                canonButtons_1.SetActive(true);
            }

        }
        else
        {
            canonButtons_1.SetActive(false);
        }
    }

    public void useCanon_1()
    {
        gameInterface_1.SetActive(false);
        playerCamera.SetActive(false);
        canonInterface_1.SetActive(true);
        canonCamera_1.SetActive(true);
    }

    public void leaveCanon_1()
    {
        gameInterface_1.SetActive(true);
        playerCamera.SetActive(true);
        canonInterface_1.SetActive(false);
        canonCamera_1.SetActive(false);
    }

    public void upgradeCanon_1()
    {

    }
}
