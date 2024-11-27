using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawn;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, playerSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
