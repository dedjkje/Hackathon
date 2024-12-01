using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField createInput;
    [SerializeField] InputField joinInput;
    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 2;
        Debug.Log("Connected to master server");
        
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }
        else
        {
            Debug.LogWarning("Cannot join room. Photon is not connected and ready.");
            // ћожно добавить дополнительные действи€ или сообщени€ об ошибке по желанию.
        }
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
