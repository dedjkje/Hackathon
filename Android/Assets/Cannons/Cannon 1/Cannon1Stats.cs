using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            gameObject.transform.Find("BuildingCannonTimer(Clone)").GetComponent<Canvas>().enabled = true;
            gameObject.transform.Find("BuildingCannonTimer(Clone)").transform.Find("Timer").GetComponent<TMP_Text>().text = $"{Mathf.FloorToInt(cooldown - curTime + time + 1)}";
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
                gameObject.transform.Find("BuildingCannonTimer(Clone)").transform.Find("Timer").GetComponent<TMP_Text>().text = "";
            }
        }
    }
}
