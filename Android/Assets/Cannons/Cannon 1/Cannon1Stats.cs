using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon1Stats : MonoBehaviourPunCallbacks
{
    public float force;
    public float cooldown;
    public float damage;
    public int force_lvl =0;
    public int cooldown_lvl =0;
    public int damage_lvl =0;
    public bool onCooldown = false;
    private bool startCoolDown = true;
    private float time = 0;
    private float curTime = 0;
    [SerializeField] GameObject cannonC;
    [SerializeField] BlueBasnyaLeft blueBasnyaLeft; [SerializeField] BlueBasnyaCenter blueBasnyaCenter;
    [SerializeField] BlueBasnyaRight blueBasnyaRight; [SerializeField] RedBasnyaCenter redBasnyaCenter;
    [SerializeField] RedBasnyaLeft redBasnyaLeft; [SerializeField] RedBasnyaRight redBasnyaRight;
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("SetCannonToBuildZone", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void SetCannonToBuildZone()
    {
        blueBasnyaRight = GameObject.Find("ZAMOOOOMK (2)").transform.Find("Красная Башня Право").gameObject.GetComponent<BlueBasnyaRight>();
        blueBasnyaLeft = GameObject.Find("ZAMOOOOMK (2)").transform.Find("Левая").gameObject.GetComponent<BlueBasnyaLeft>();
        blueBasnyaCenter = GameObject.Find("ZAMOOOOMK (2)").transform.Find("Центральная").gameObject.GetComponent<BlueBasnyaCenter>();

        redBasnyaRight = GameObject.Find("ZAMOOOOMK (1)").transform.Find("Красная Башня Право").gameObject.GetComponent<RedBasnyaRight>();
        redBasnyaLeft = GameObject.Find("ZAMOOOOMK (1)").transform.Find("Левая").gameObject.GetComponent<RedBasnyaLeft>();
        redBasnyaCenter = GameObject.Find("ZAMOOOOMK (1)").transform.Find("Центральная").gameObject.GetComponent<RedBasnyaCenter>();

        if (transform.position.x > 10 && transform.position.z > 42)
        {
            blueBasnyaRight.cannon = cannonC;
        }
        if (transform.position.x > 10 && transform.position.z < 42 && transform.position.z > 14)
        {
            blueBasnyaCenter.cannon = cannonC;
        }
        if (transform.position.x > 10 && transform.position.z < 14)
        {
            blueBasnyaLeft.cannon = cannonC;
        }

        if (transform.position.x < 10 && transform.position.z > 42)
        {
            redBasnyaLeft.cannon = cannonC;
        }
        if (transform.position.x < 10 && transform.position.z < 42 && transform.position.z > 14)
        {
            redBasnyaCenter.cannon = gameObject;
        }
        if (transform.position.x < 10 && transform.position.z < 14)
        {
            redBasnyaRight.cannon = gameObject;
        }
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
