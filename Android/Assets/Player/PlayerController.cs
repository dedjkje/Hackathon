using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Скорость движения персонажа
    PhotonView view;
    private CharacterController controller;
    private Animator animator;
    private Joystick joystick;
    public float horizontalInput;
    public float verticalInput;
    private UseCannons useCannons;
    public float y;

    //[SerializeField] private Animator animator;

    private void Start()
    {
        y = gameObject.transform.position.y;
        useCannons = GetComponent<UseCannons>();
        view = GetComponent<PhotonView>();
        animator = transform.Find("Knight").GetComponent<Animator>();   
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
        if (gameObject.transform.position.y != y)
        {
            
            y = gameObject.transform.position.y;  
            if (useCannons.inCannon)
            {
                useCannons.stopUsingCannon();
            }
        }
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;

        if (verticalInput * verticalInput  + horizontalInput * horizontalInput > 0 && verticalInput * verticalInput + horizontalInput * horizontalInput != 1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (verticalInput * verticalInput + horizontalInput * horizontalInput == 1)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        // Вычисляем направление движения
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Применяем гравитацию
        moveDirection.y -= 2f;

        // Двигаем персонажа
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        // Получаем ввод от игрока

    }

    

}
