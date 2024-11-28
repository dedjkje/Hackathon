using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class UseCannons : MonoBehaviour
{
    [SerializeField] GameObject useUpgradePanel;
    [SerializeField] GameObject cannonInterface;
    [SerializeField] GameObject playerInterface;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] GameObject particleShootPrefab;
    public string currentTag;
    public Transform explosionPlace;
    private int player1Layer; // ���� ��� ������
    private int player2Layer; // ���� ��� ������
    public bool startShake = false; // ������
    int i = 0; // ������
    public float shakeIntensivity = 0.66f; // ������
    int count = 0; // ������
    public Vector3 startPositionCamera;
    public Quaternion startRotationCamera;

    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = transform.Find("Camera").GetComponent<Camera>();
        player1Layer = LayerMask.NameToLayer("player1");
        player2Layer = LayerMask.NameToLayer("player2");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (startShake)
        {
            int index = i % 12;
            if (index == 0) playerCamera.transform.Rotate(0, 0, shakeIntensivity);
            if (index == 1) playerCamera.transform.Rotate(0, 0, shakeIntensivity);
            if (index == 2) playerCamera.transform.Rotate(0, 0, shakeIntensivity);
            if (index == 3) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 4) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 5) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 6) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 7) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 8) playerCamera.transform.Rotate(0, 0, -shakeIntensivity);
            if (index == 9) playerCamera.transform.Rotate(0, 0, shakeIntensivity);
            if (index == 10) playerCamera.transform.Rotate(0, 0, shakeIntensivity);
            if (index == 11) playerCamera.transform.Rotate(0, 0, shakeIntensivity);

            i++;
            count++;
            if (count == 24)
            {
                count = 0;
                startShake = false;
            }
        }
        RaycastHit hit;
        Ray ray = transform.Find("Camera").GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 2f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Cannon1") ||
                objectHit.CompareTag("Cannon2") ||
                objectHit.CompareTag("Cannon3") ||
                objectHit.CompareTag("Cannon4") ||
                objectHit.CompareTag("Cannon5") ||
                objectHit.CompareTag("Cannon6"))
            {
                useUpgradePanel.SetActive(true);
                currentTag = objectHit.tag; // ���������� ��� �����
            }
            else
            {
                useUpgradePanel.SetActive(false);
            }

        }
        else
        {
            useUpgradePanel.SetActive(false); // ������� ;)
        }
        
    }

    public void useCannon()
    { 

        startPositionCamera = playerCamera.transform.position; // ���������� ������� ������ ������
        startRotationCamera = playerCamera.transform.rotation; // ���������� ������� ������ ������

        Camera canonCamera = GameObject.FindWithTag(currentTag).transform.Find("Camera").GetComponent<Camera>(); // ���� ������ ������� �� ����
        playerCamera.GetComponent<CameraController>().enabled = false;
        Vector3 cameraPosition = canonCamera.transform.position;
        Quaternion cameraRotation = canonCamera.transform.rotation;
        if (name == "Player 1(Clone)")
        {
            playerCamera.cullingMask &= ~(1 << player1Layer);
        }
        if (name == "Player 2(Clone)")
        {
            playerCamera.cullingMask &= ~(1 << player2Layer);
        }
        playerCamera.transform.position = cameraPosition; // ������ ������� ������ ������ ������ ������ �������
        playerCamera.transform.rotation = cameraRotation; // ������ ������� ������ ������ ������ ������ �������

        GameObject.FindWithTag(currentTag).transform.Find("cannon").transform.Find("stvol").GetComponent<Accemilator>().SetCannon(); // �������� �����������
        GetComponent<Canon1Trajectory>().OnCannon();
        playerInterface.SetActive(false);
        cannonInterface.SetActive(true);

    }

    public void stopUsingCannon()
    {
        playerCamera.GetComponent<CameraController>().enabled = true;
        playerCamera.transform.position = startPositionCamera;
        playerCamera.transform.rotation = startRotationCamera;

        GameObject.FindWithTag(currentTag).transform.Find("cannon").transform.Find("stvol").GetComponent<Accemilator>().UnsetCannon(); // ��������� �����������
        GetComponent<Canon1Trajectory>().OutCannon();
        if (name == "Player 1(Clone)")
        {
            playerCamera.cullingMask |= ~(1 << player1Layer);
        }
        if (name == "Player 2(Clone)")
        {
            playerCamera.cullingMask |= ~(1 << player2Layer);
        }
        playerInterface.SetActive(true);
        cannonInterface.SetActive(false);
    }

    public void Shoot()
    {
        if (GameObject.FindWithTag(currentTag).name == "Cannon 1(Clone)")
        {
            
            startShake = true; // ������
            GameObject canon = GameObject.FindWithTag(currentTag);
            Vector3 shellPos = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.position;
            Quaternion shellRot = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.rotation;
            explosionPlace = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("particlePos").transform;
            PhotonNetwork.Instantiate("Shoot", explosionPlace.position, explosionPlace.rotation);
            GameObject shell = PhotonNetwork.Instantiate("Cannon Shell", shellPos, shellRot);            
            shell.transform.parent = null;

            float force = canon.GetComponent<Cannon1Stats>().force;

            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * force, ForceMode.Impulse);

            Animator animator = canon.GetComponent<Animator>();
            //animator.SetTrigger("Shoot");
        }
        if (GameObject.FindWithTag(currentTag).name == "Cannon 2(Clone)")
        {

            startShake = true; // ������
            GameObject canon = GameObject.FindWithTag(currentTag);
            Vector3 shellPos = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.position;
            Quaternion shellRot = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.rotation;
            explosionPlace = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("particlePos").transform;
            PhotonNetwork.Instantiate("Shoot", explosionPlace.position, explosionPlace.rotation);
            GameObject shell = PhotonNetwork.Instantiate("Cannon Shell", shellPos, shellRot);
            shell.transform.localScale *= 2f;
            shell.transform.parent = null;

            float force = canon.GetComponent<Cannon1Stats>().force;

            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * force, ForceMode.Impulse);

            Animator animator = canon.GetComponent<Animator>();
            //animator.SetTrigger("Shoot");
        }
        if (GameObject.FindWithTag(currentTag).name == "Cannon 3(Clone)")
        {

            startShake = true; // ������
            GameObject canon = GameObject.FindWithTag(currentTag);
            Vector3 shellPos = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.position;
            Quaternion shellRot = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("ShellPos").transform.rotation;
            explosionPlace = canon.transform.Find("cannon").transform.Find("stvol").transform.Find("particlePos").transform;
            PhotonNetwork.Instantiate("Shoot", explosionPlace.position, explosionPlace.rotation);
            GameObject shell = PhotonNetwork.Instantiate("Cannon Shell", shellPos, shellRot);
            shell.transform.localScale *= 2.8f;
            shell.transform.parent = null;

            float force = canon.GetComponent<Cannon1Stats>().force;

            shell.GetComponent<Rigidbody>().AddForce(shell.transform.forward * force, ForceMode.Impulse);

            Animator animator = canon.GetComponent<Animator>();
            //animator.SetTrigger("Shoot");
        }
    }
}
