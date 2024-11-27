using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accemilator : MonoBehaviour
{
    public Transform Cannon;
    public Transform Stvol;
    public float speedHor;
    public float speedVer;
    private bool gyroIsEnabled;
    float x;
    float y;
    public float maxRotateHorRight;
    public float maxRotateHorLeft;
    public float maxRotateVerUp;
    public float maxRotateVerDown;
    bool stopRotateHorRight;
    bool stopRotateHorLeft;
    bool stopRotateVerUp;
    bool stopRotateVerDown;
    float horCannonRotation;
    float verStvolRotation;
    bool onThisCannon;
    public float KolesaSpeed = 2f;
    [SerializeField] Transform ParavoeKoleso;
    [SerializeField] Transform LevoeKoleso;
    [SerializeField] ParticleSystem shoot;

    void Awake()
    {
        Stvol = GetComponent<Transform>();
        Cannon = transform.parent;
        stopRotateHorLeft = false;
        stopRotateHorRight = false;
        x = Input.acceleration.x;
        y = Input.acceleration.y;
        horCannonRotation = 0;
        verStvolRotation = 0;
    }

    void Update()
    {
        if (onThisCannon)
        {
            CannonRotate();
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