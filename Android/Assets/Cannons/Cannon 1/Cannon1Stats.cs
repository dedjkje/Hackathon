using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon1Stats : MonoBehaviour
{
    public float force;
    public float cooldown;
    public float damage;
    public bool onCooldown = false;
    private bool startCoolDown = true;
    private float time = 0;
    private float curTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            if (startCoolDown)
            {
                time = Time.time;
                startCoolDown = false;
            }
            curTime = Time.time;
            if (curTime - time > cooldown)
            { 
                onCooldown = false;
                startCoolDown = true;
            }
        }
    }
}
