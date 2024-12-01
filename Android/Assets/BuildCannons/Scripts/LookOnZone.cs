using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnZone : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject crosshair;
    private void Start()
    {
        
    }
    void Update()
    {
        //��������� ������� �� ����� �� ���� �������������
        if (IsLookingAtBuildingZone())
        {
            panel.gameObject.SetActive(true);
            crosshair.SetActive(false);
        }
        else
        {
            panel.gameObject.SetActive(false);
            crosshair.SetActive(true);
        }
    }

    private bool IsLookingAtBuildingZone()
    {
        //��������� ��� ���
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //�����
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        //���� �������� ����� � ����� ������� ���������� ���� 
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("BuildZone"))
            {
                return true;
            }
        }
        return false;
    }

    

}
