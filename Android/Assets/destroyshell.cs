using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyshell : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Shell")
        {
            PhotonNetwork.Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
