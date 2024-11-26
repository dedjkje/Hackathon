using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Скорость движения персонажа

    private CharacterController controller;

    public Joystick joystick;
    //[SerializeField] private Animator animator;
    private Alteruna.Avatar avatar;
    private void Start()
    {

        
        avatar = GetComponent<Alteruna.Avatar>();
        if (!avatar.IsMe) return;
        
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!avatar.IsMe) return;
        // Получаем ввод от игрока
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
    }
}
