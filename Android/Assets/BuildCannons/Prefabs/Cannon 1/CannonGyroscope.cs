using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonGyroscope : MonoBehaviour
{
    public Transform Cannon;
    public Transform Stvol;
    public float speedHor;
    public float speedVer;
    private bool gyroIsEnabled;
    private Gyroscope gyroscope;
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

    void Start()
    {
        Stvol = GetComponent<Transform>();
        Cannon = transform.parent;
        stopRotateHorLeft = false;
        stopRotateHorRight = false;
        gyroscope = Input.gyro;
        gyroscope.enabled = false;
        x = gyroscope.attitude.x;
        y = gyroscope.attitude.y;
        horCannonRotation = 0;
        verStvolRotation = 0;
    }

    void Update()
    {
        gyroscope.enabled = true;
        if (onThisCannon)
        {
            CannonRotate();
        }
    }
    void CannonRotate()
    {
        float detx = x - gyroscope.attitude.x;
        float dety = y - gyroscope.attitude.y;
        // блокировка по горизонтали 
        if (horCannonRotation > maxRotateHorRight)
        {
            stopRotateHorRight = true;
        }
        if (stopRotateHorRight && detx > 0)
        {
            detx = 0;
            stopRotateHorRight = false;
        }
        if (horCannonRotation < maxRotateHorLeft)
        {
            stopRotateHorLeft = true;
        }
        if (stopRotateHorLeft && detx < 0)
        {
            detx = 0;
            stopRotateHorLeft = false;
        }
        // блокировка по вертикали 
        if (verStvolRotation < maxRotateVerUp)
        {
            stopRotateVerUp = true;
        }
        if (stopRotateVerUp && dety < 0)
        {
            dety = 0;
            stopRotateVerUp = false;
        }
        if (verStvolRotation > maxRotateVerDown)
        {
            stopRotateVerDown = true;
        }
        if (stopRotateVerDown && dety > 0)
        {
            dety = 0;
            stopRotateVerDown = false;
        }

        Stvol.Rotate(dety * speedVer, 0, 0);
        verStvolRotation += dety * speedVer;
        Cannon.Rotate(0, detx * speedHor, 0);
        horCannonRotation += detx * speedHor;
    }
    public void SetCannon()
    {
        onThisCannon = true;
        x = gyroscope.attitude.x;
        y = gyroscope.attitude.y;
    }
    public void UnsetCannon()
    {
        onThisCannon = false;
    }
}