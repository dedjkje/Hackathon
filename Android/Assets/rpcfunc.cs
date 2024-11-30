using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class rpcfunc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void RemoveBoxColliderRPC()
    {
        foreach(BoxCollider a in GetComponents<BoxCollider>()) {
            a.enabled = false;
        }
    }
    [PunRPC]
    public void Delete(int ViewID)
    {
        Destroy(PhotonView.Find(ViewID).gameObject);
    }
    [PunRPC]
    public void GiveRigidbody(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            child.GetComponent<MeshCollider>().convex = true;
            child.AddComponent<Rigidbody>();
        }
    }
}
