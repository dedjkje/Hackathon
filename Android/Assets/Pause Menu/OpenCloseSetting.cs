using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class OpenCloseSetting : MonoBehaviourPunCallbacks
{
    [SerializeField] private Image pauseClickImage; // открытие настроек
    [SerializeField] private GameObject settingsMenu; //хранит сами внутренности настроек
    [SerializeField] private Image closeImage; // закрытие настроек
    [SerializeField] private Image exitRoomImage; // выход из комнаты

    void Start()
    {
        pauseClickImage.GetComponent<Image>().raycastTarget = true;
        EventTrigger eventTrigger = pauseClickImage.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPauseClickDelegate((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);


        EventTrigger closeEventTrigger = closeImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry closeEntry = new EventTrigger.Entry();
        closeEntry.eventID = EventTriggerType.PointerDown;
        closeEntry.callback.AddListener((data) => { OnCloseClickDelegate((PointerEventData)data); });
        closeEventTrigger.triggers.Add(closeEntry);


        EventTrigger exitEventTrigger = exitRoomImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerDown;
        exitEntry.callback.AddListener((data) => { OnExitRoomClickDelegate((PointerEventData)data); });
        exitEventTrigger.triggers.Add(exitEntry);
    }

    private void OnPauseClickDelegate(PointerEventData data)
    {
        ToggleSettingsMenu();
    }

    private void OnCloseClickDelegate(PointerEventData data)
    {
        ToggleSettingsMenu();
    }

    private void OnExitRoomClickDelegate(PointerEventData data)
    {
        ExitRoom();
    }

    private void ToggleSettingsMenu()
    {
        bool isActive = !settingsMenu.activeSelf;

        settingsMenu.SetActive(isActive);
        pauseClickImage.gameObject.SetActive(!isActive);
    }

    private void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
