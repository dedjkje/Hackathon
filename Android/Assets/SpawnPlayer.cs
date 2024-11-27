using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Transform[] playerSpawn;
    [SerializeField] GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion[] rotations = { Quaternion.identity, new Quaternion(Quaternion.identity.x, -180, Quaternion.identity.z, Quaternion.identity.w) };
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(player[0].name, playerSpawn[0].position, rotations[0]);
        }
        else
        {
            PhotonNetwork.Instantiate(player[1].name, playerSpawn[1].position, rotations[1]);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
