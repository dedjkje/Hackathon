using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisrtuct : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void DeleteObj(int objectId)
    {
        GameObject objToRemove = PhotonView.Find(objectId).gameObject;
        if (objToRemove != null)
        {
            // Удалите объект из сети
            Destroy(objToRemove);
        }
    }

    public void StartRpcDeleteObj(int viewID)
    {
        photonView.RPC("DeleteObj", RpcTarget.AllBuffered, viewID);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
