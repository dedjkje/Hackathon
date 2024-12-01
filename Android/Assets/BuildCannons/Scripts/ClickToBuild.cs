using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClickToBuild : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject cannon;
    [SerializeField] private Camera camera;
    private GameObject tools;
    PhotonView View;
    
    public void onClick()
    {
        View = transform.parent.transform.parent.transform.parent.GetComponent<PhotonView>();
        if (!View.IsMine) return
                ;
        //Скрываем менюшку
        transform.parent.gameObject.SetActive(false);

        //Выпускаем лучик, чтобы дезактивировать зону для постройки и создать пушку
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //Дебаг
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            //hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameObject objectHit = hit.transform.gameObject;
            objectHit.transform.parent.GetComponent<SelfDisrtuct>().StartRpcDeleteObj(objectHit.GetComponent<PhotonView>().ViewID);

            if (objectHit.CompareTag("BuildZone"))
            {
                
                GameObject cannon = PhotonNetwork.Instantiate(name, new Vector3(hit.transform.position.x, hit.transform.position.y - 1, hit.transform.position.z), hit.transform.rotation);
                if (transform.parent.transform.parent.transform.parent.name == "Player 1(Clone)")
                {
                    
                    
                    cannon.GetComponent<ChangeMaterials>().ChangeToTransparentBlue();
                    
                }
                else
                {
                    
                    cannon.GetComponent<ChangeMaterials>().ChangeToTransparentRed();
                   
                }
                
                
                
            }
        }
    }

    



    
}
