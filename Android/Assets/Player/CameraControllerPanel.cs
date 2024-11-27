using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed = false;
    public int fingerId;
    PhotonView view;
    private void Start()
    {
        view = transform.parent.transform.parent.GetComponent<PhotonView>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            pressed = true;
            fingerId = eventData.pointerId;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
