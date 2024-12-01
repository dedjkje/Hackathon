using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Accemilator : MonoBehaviour
{
    public Transform Cannon;
    public Transform Stvol;
    public float speedHor;
    public float speedVer;
    private bool gyroIsEnabled;
    float x;
    float y;
    private float maxRotateHorRight;
    private float maxRotateHorLeft;
    private float maxRotateVerUp;
    private float maxRotateVerDown;
    bool stopRotateHorRight;
    bool stopRotateHorLeft;
    bool stopRotateVerUp;
    bool stopRotateVerDown;
    float horCannonRotation;
    float verStvolRotation;
    bool onThisCannon;
    public float KolesaSpeed = 2f;
    public int typeOfCannon;
    [SerializeField] Transform ParavoeKoleso;
    [SerializeField] Transform LevoeKoleso;
    [SerializeField] Transform ParavoeKolesoLittle;
    [SerializeField] Transform ParavoeKolesoBig;
    [SerializeField] Transform LevoeKolesoLittle;
    [SerializeField] Transform LevoeKolesoBig;
    
    void Awake()
    {
        if (typeOfCannon == 2 && transform.parent.transform.parent.position.z > 10 && transform.parent.transform.parent.position.z < 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 2 && transform.parent.transform.parent.position.z < 10)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 2 && transform.parent.transform.parent.position.z > 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 1 && transform.parent.transform.parent.position.z > 10 && transform.parent.transform.parent.position.z < 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 1 && transform.parent.transform.parent.position.z < 10)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 1 && transform.parent.transform.parent.position.z > 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 3 && transform.parent.transform.parent.position.z > 10 && transform.parent.transform.parent.position.z < 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 3 && transform.parent.transform.parent.position.z < 10)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        if (typeOfCannon == 3 && transform.parent.transform.parent.position.z > 40)
        {
            maxRotateHorRight = 31;
            maxRotateHorLeft = -31;
            maxRotateVerUp = -20;
            maxRotateVerDown = 20;
        }
        Stvol = GetComponent<Transform>();
        Cannon = transform.parent;
        stopRotateHorLeft = false;
        stopRotateHorRight = false;
        x = Input.acceleration.x;
        y = Input.acceleration.y;
        horCannonRotation = 0;
        verStvolRotation = 0;
    }

    void FixedUpdate()
    {
        if (onThisCannon)
        {
            CannonRotate();
        }
        if(TryGetComponent<Rigidbody>(out Rigidbody r))
        {
            transform.parent.gameObject.tag = "Trash";
        }
    }
    void CannonRotate()
    {
        float detx = x - Input.acceleration.x;
        float dety = y - Input.acceleration.y;
        // блокировка по горизонтали  
        if (horCannonRotation > -maxRotateHorLeft)
        {
            stopRotateHorRight = true;
        }
        if (stopRotateHorRight && detx > 0)
        {
            detx = 0;
            stopRotateHorRight = false;
        }
        if (horCannonRotation < -maxRotateHorRight)
        {
            stopRotateHorLeft = true;
        }
        if (stopRotateHorLeft && detx < 0)
        {
            detx = 0;
            stopRotateHorLeft = false;
        }
        // блокировка по вертикали  
        if (verStvolRotation < -maxRotateVerDown)
        {
            stopRotateVerUp = true;
        }
        if (stopRotateVerUp && dety < 0)
        {
            dety = 0;
            stopRotateVerUp = false;
        }
        if (verStvolRotation > -maxRotateVerUp)
        {
            stopRotateVerDown = true;
        }
        if (stopRotateVerDown && dety > 0)
        {
            dety = 0;
            stopRotateVerDown = false;
        }
        if (typeOfCannon == 1)
        {
            if (detx < 0)
            {
                ParavoeKoleso.Rotate(0, 0, -detx * KolesaSpeed);
                LevoeKoleso.Rotate(0, 0, -detx * KolesaSpeed); // ? 
            }
            if (detx > 0)
            {
                LevoeKoleso.Rotate(0, 0, -detx * KolesaSpeed);
                ParavoeKoleso.Rotate(0, 0, -detx * KolesaSpeed); // ? 
            }
        }
        if (typeOfCannon == 2)
        {
            if (detx < 0)
            {
                ParavoeKolesoLittle.Rotate(0, 0, detx * KolesaSpeed);
                ParavoeKolesoBig.Rotate(0, 0, -detx * KolesaSpeed);
                LevoeKolesoLittle.Rotate(0, 0, -detx * KolesaSpeed); // ? 
                LevoeKolesoBig.Rotate(0, 0, -detx * KolesaSpeed);
            }
            if (detx > 0)
            {
                ParavoeKolesoLittle.Rotate(0, 0, detx * KolesaSpeed);
                ParavoeKolesoBig.Rotate(0, 0, -detx * KolesaSpeed);
                LevoeKolesoLittle.Rotate(0, 0, -detx * KolesaSpeed); // ? 
                LevoeKolesoBig.Rotate(0, 0, -detx * KolesaSpeed);
            }
        }
        if (typeOfCannon == 3)
        {
            if (detx < 0)
            {
                ParavoeKoleso.Rotate(detx * KolesaSpeed, 0, 0);
                LevoeKoleso.Rotate(-detx * KolesaSpeed, 0, 0); // ? 
            }
            if (detx > 0)
            {
                LevoeKoleso.Rotate(-detx * KolesaSpeed, 0, 0);
                ParavoeKoleso.Rotate(detx * KolesaSpeed, 0, 0); // ? 
            }
        }
        Stvol.Rotate(-1 * dety * speedVer, 0, 0);
        verStvolRotation += dety * speedVer;
        Cannon.Rotate(0, -1 * detx * speedHor, 0);
        horCannonRotation += detx * speedHor;
    }
    public void SetCannon()
    {
        onThisCannon = true;
        x = Input.acceleration.x;
        y = Input.acceleration.y;
    }
    public void UnsetCannon()
    {
        onThisCannon = false;
    }
}