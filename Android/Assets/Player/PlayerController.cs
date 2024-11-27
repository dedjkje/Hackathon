using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Скорость движения персонажа
    PhotonView view;
    private CharacterController controller;

    private Joystick joystick;
    //[SerializeField] private Animator animator;
    
    private void Start()
    {
        
        view = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        joystick = GameObject.Find("Canvas (Player Interface)").transform.Find("Floating Joystick").GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        if (!view.IsMine) return;
        else
        {
            tag = "Player2";
            if (GameObject.FindWithTag("Player1") != null )
            {
                GameObject.FindWithTag("Player1").transform.Find("Camera").GetComponent<Camera>().enabled = false;
                GameObject.FindWithTag("Player1").transform.Find("Canvas (Player Interface)").GetComponent<Canvas>().enabled = false;
                GameObject.FindWithTag("Player1").transform.Find("Canvas (Player Interface)").GetComponent<Canvas>().enabled = false;
            }
            
        }

        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (verticalInput + horizontalInput == 0)
        {
            //animator.SetBool("isWalking", false);
        }
        else
        {
            //animator.SetBool("isWalking", true);
        }
        // Вычисляем направление движения
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Применяем гравитацию
        moveDirection.y -= 9.81f * Time.deltaTime;

        // Двигаем персонажа
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        // Получаем ввод от игрока

    }
}
