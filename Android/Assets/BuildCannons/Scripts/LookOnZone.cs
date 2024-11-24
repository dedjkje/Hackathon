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
        //Проверяем смотрил ли игрок на зону строительства
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
        //Выпускаем кек луч
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //Дебаг
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        //Если касается фигню с тегом билдзон возвращаем труе 
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
