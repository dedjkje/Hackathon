using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject pausemenu;
    [SerializeField] GameObject pause;
    public void LeaveCurrentRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        // Код для выполнения после выхода из комнаты
        Debug.Log("Вы покинули комнату.");

        // Например, можно переключиться на главное меню или сценуLobby
        SceneManager.LoadScene("Loading");
        PhotonNetwork.Disconnect();
    }

    public void DestroyRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false; // Скрытость комнаты
            PhotonNetwork.CurrentRoom.IsOpen = false; // Закрыть комнату для новых игроков
            // После этого вы можете вызывать метод, который разлучает всех игроков в комнате
            PhotonNetwork.LoadLevel("Loading"); // Перейдите на другую сцену, если необходимо
            PhotonNetwork.Disconnect();
            
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void Pause()
    {
        pause.SetActive(false);
        pausemenu.SetActive(true);
    }
    public void Krestik()
    {
        pause.SetActive(true);
        pausemenu.SetActive(false);
    }

    public void Menu()
    {
        DestroyRoom();
    }

}