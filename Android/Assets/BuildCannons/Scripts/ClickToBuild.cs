using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour
{

    Spawner spawner;
    [SerializeField] private GameObject cannon;
    [SerializeField] private Camera camera;
    void Awake()
    {
        spawner = GameObject.FindWithTag("NetworkManager").GetComponent<Spawner>();
    }
    public void onClick()
    {

        //Скрываем менюшку
        transform.parent.gameObject.SetActive(false);

        //Выпускаем лучик, чтобы дезактивировать зону для постройки и создать пушку
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //Дебаг
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("BuildZone1"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(-14.1999998f, 23.6942387f, -9.29000092f);
                spawner.Spawn(0, pos, Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
                
            }
            if (objectHit.CompareTag("BuildZone2"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(3.1400001f, 23.6942387f, -9.29000092f);
                spawner.Spawn(0, pos, Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
            if (objectHit.CompareTag("BuildZone3"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(20.4000015f, 23.6942387f, -9.29000092f);
                spawner.Spawn(0, pos, Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
            if (objectHit.CompareTag("BuildZone4"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(20.4000015f, 23.6942387f, -13.8099995f);
                spawner.Spawn(0, pos, new Quaternion(Quaternion.identity.x, -180, Quaternion.identity.y, Quaternion.identity.w));
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
            if (objectHit.CompareTag("BuildZone5"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(3.1400001f, 23.6942387f, -13.8100004f);
                spawner.Spawn(0, pos, new Quaternion(Quaternion.identity.x, -180, Quaternion.identity.y, Quaternion.identity.w));
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;



            }
            if (objectHit.CompareTag("BuildZone6"))
            {
                //Чего ты в мой код полез?
                //Спваним пушку по кордам билдзоны и деактивируем её
                Vector3 pos = new Vector3(-14.1999998f, 23.6942387f, -13.8099995f);
                spawner.Spawn(0, pos, new Quaternion(Quaternion.identity.x, -180, Quaternion.identity.y, Quaternion.identity.w));
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;

            }
        }
    }

    

}
