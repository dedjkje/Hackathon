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
        // ��� ��� ���������� ����� ������ �� �������
        Debug.Log("�� �������� �������.");

        // ��������, ����� ������������� �� ������� ���� ��� �����Lobby
        SceneManager.LoadScene("Loading");
        PhotonNetwork.Disconnect();
    }

    public void DestroyRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false; // ��������� �������
            PhotonNetwork.CurrentRoom.IsOpen = false; // ������� ������� ��� ����� �������
            // ����� ����� �� ������ �������� �����, ������� ��������� ���� ������� � �������
            PhotonNetwork.LoadLevel("Loading"); // ��������� �� ������ �����, ���� ����������
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