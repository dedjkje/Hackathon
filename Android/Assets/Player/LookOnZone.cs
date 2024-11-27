using Alteruna;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnZone : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject panel;

    Alteruna.Avatar avatar;
    private void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();

        if (!avatar.IsMe) return;
    }
    void Update()
    {
        if (!avatar.IsMe) return;
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
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //�����
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        //���� �������� ����� � ����� ������� ���������� ���� 
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("BuildZoneBlue") || objectHit.CompareTag("BuildZoneRed"))
            {
                return true;
            }
        }
        return false;
    }

}
