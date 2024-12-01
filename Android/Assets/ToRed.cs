using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ToRed : MonoBehaviourPunCallbacks
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toRed()
    {
        StartCoroutine(red());
    }
    public void toBlue()
    {
        StartCoroutine(blue());
    }

    IEnumerator red()
    {
        yield return 3;
        PhotonNetwork.LoadLevel("Red");
    }
    IEnumerator blue()
    {
        yield return 3;
        PhotonNetwork.LoadLevel("Blue");
    }
    
}
