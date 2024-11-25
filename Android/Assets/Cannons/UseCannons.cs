using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class UseCannons : MonoBehaviour
{
    [SerializeField] GameObject useUpgradePanel;
    [SerializeField] GameObject cannonInterface;
    [SerializeField] GameObject playerInterface;
    [SerializeField] GameObject shellPrefab;
    public string currentTag;

    public Vector3 startPositionCamera;
    public Quaternion startRotationCamera;

    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = transform.Find("Camera").GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 2f))
        {
            Transform objectHit = hit.transform;
            Debug.Log(hit.transform.tag);
            if (objectHit.CompareTag("Cannon1") ||
                objectHit.CompareTag("Cannon2") ||
                objectHit.CompareTag("Cannon3") ||
                objectHit.CompareTag("Cannon4") ||
                objectHit.CompareTag("Cannon5") ||
                objectHit.CompareTag("Cannon6"))
            {
                useUpgradePanel.SetActive(true);
                currentTag = objectHit.tag; // Запоминает тег пушки
            }
            else
            {
                useUpgradePanel.SetActive(false);
            }

        }
        else
        {
            useUpgradePanel.SetActive(false); // Костыль ;)
        }
        
    }

    public void useCannon()
    { 

        startPositionCamera = playerCamera.transform.position; // Запоминаем позицию камеры игрока
        startRotationCamera = playerCamera.transform.rotation; // Запоминаем поворот камеры игрока

        Camera canonCamera = GameObject.FindWithTag(currentTag).transform.Find("Camera").GetComponent<Camera>(); // Ищет камеру префаба по тегу
        playerCamera.GetComponent<CameraController>().enabled = false;
        Vector3 cameraPosition = canonCamera.transform.position;
        Quaternion cameraRotation = canonCamera.transform.rotation;
        playerCamera.transform.position = cameraPosition; // Делаем позицию камеры игрока равной камере префаба
        playerCamera.transform.rotation = cameraRotation; // Делаем поворот камеры игрока равной камере префаба

        GameObject.FindWithTag(currentTag).transform.Find("cannon").transform.Find("stvol").GetComponent<Accemilator>().SetCannon(); // Включаем аксемилятор
        playerInterface.SetActive(false);
        cannonInterface.SetActive(true);

    }

    public void stopUsingCannon()
    {
        playerCamera.GetComponent<CameraController>().enabled = true;
        playerCamera.transform.position = startPositionCamera;
        playerCamera.transform.rotation = startRotationCamera;

        GameObject.FindWithTag(currentTag).transform.Find("cannon").transform.Find("stvol").GetComponent<Accemilator>().UnsetCannon(); // Выключаем аксимилятор

        playerInterface.SetActive(true);
        cannonInterface.SetActive(false);
    }

    public void Shoot()
    {
        if (GameObject.FindWithTag(currentTag).name == "Cannon 1(Clone)")
        {
            GameObject canon = GameObject.FindWithTag(currentTag);
            Vector3 shellPos = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.position;
            Quaternion shellRot = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.rotation;

            GameObject shell = Instantiate(shellPrefab, shellPos, shellRot, transform);            
            shell.transform.parent = null;

            float force = canon.GetComponent<Cannon1Stats>().force;

            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * force, ForceMode.Impulse);

            Animator animator = canon.GetComponent<Animator>();
            animator.SetBool("Shoot", true);
        }
    }
}
