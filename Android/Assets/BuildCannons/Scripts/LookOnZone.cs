using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnZone : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject panel;

    void Update()
    {
        //��������� ������� �� ����� �� ���� �������������
        if (IsLookingAtBuildingZone())
        {
            panel.gameObject.SetActive(true);
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }

    private bool IsLookingAtBuildingZone()
    {
        //��������� ��� ���
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

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
