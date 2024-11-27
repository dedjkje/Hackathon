using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ClickToBuild : AttributesSync
{

    Spawner spawner;
    [SynchronizableField] Vector3 pos;
    [SerializeField] private Camera camera;
    [SerializeField] GameObject buildMenu;
    void Awake()
    {
        spawner = GameObject.FindWithTag("NetworkManager").GetComponent<Spawner>();
    }
    public void onClick1()
    {

        //Скрываем менюшку
        buildMenu.SetActive(false);

        //Выпускаем лучик, чтобы дезактивировать зону для постройки и создать пушку
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //Дебаг
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("BuildZoneBlue"))
            {
                
                pos = new Vector3(hit.transform.position.x, hit.transform.position.y - 1, hit.transform.position.z);
                GameObject.FindWithTag("NetworkManager").GetComponent<CanonPos>().position = pos;
                GameObject.FindWithTag("NetworkManager").GetComponent<CanonPos>().Spawn(0, Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
                
            }
            if (objectHit.CompareTag("BuildZoneRed"))
            {
                pos = new Vector3(hit.transform.position.x, hit.transform.position.y - 1, hit.transform.position.z);
                GameObject.FindWithTag("NetworkManager").GetComponent<CanonPos>().Spawn(0, new Quaternion(Quaternion.identity.x, -180, Quaternion.identity.z, Quaternion.identity.z));
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
        }
    }

    

}
