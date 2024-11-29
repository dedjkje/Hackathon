using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePanel : MonoBehaviour
{
    [SerializeField] GameObject updatePanel;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject touchPad;
    [SerializeField] UseCannons useCannos;
    Cannon1Stats cannon1Stats;
    [SerializeField] GameObject damagelvl1;
    [SerializeField] GameObject damagelvl2;
    [SerializeField] GameObject damagelvl3;
    [SerializeField] GameObject cooldownlvl1;
    [SerializeField] GameObject cooldownlvl2;
    [SerializeField] GameObject cooldownlvl3;
    [SerializeField] GameObject distancelvl1;
    [SerializeField] GameObject distancelvl2;
    [SerializeField] GameObject distancelvl3;
    [SerializeField] GameObject damageUpdate;
    [SerializeField] GameObject damageFull;
    [SerializeField] GameObject cooldownUpdate;
    [SerializeField] GameObject cooldownFull;
    [SerializeField] GameObject distanceUpdate;
    [SerializeField] GameObject distanceFull;
    [SerializeField] Text damageCoins;
    [SerializeField] Text cooldownCoins;
    [SerializeField] Text distanceCoins;
    [SerializeField] GameObject UseUpdatePanel;
    public bool flag;
    Coins coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<Coins>();
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            Debug.Log(useCannos.currentTag);
            cannon1Stats = GameObject.FindWithTag(useCannos.currentTag).GetComponent<Cannon1Stats>();
            damageCoins.text = $"{(double)coins.coins}/30";
            cooldownCoins.text = $"{(double)coins.coins}/25";
            distanceCoins.text = $"{(double)coins.coins}/20";

            

            if (cannon1Stats.damage_lvl == 3)
            {
                damagelvl1.SetActive(true);
                damagelvl2.SetActive(true);
                damagelvl3.SetActive(true);
                damageUpdate.SetActive(false);
                damageFull.SetActive(true);
            }

            if (cannon1Stats.cooldown_lvl == 3)
            {
                cooldownlvl1.SetActive(true);
                cooldownlvl2.SetActive(true);
                cooldownlvl3.SetActive(true);
                cooldownUpdate.SetActive(false);
                cooldownFull.SetActive(true);
            }

            if (cannon1Stats.force_lvl == 3)
            {
                distancelvl1.SetActive(true);
                distancelvl2.SetActive(true);
                distancelvl3.SetActive(true);
                distanceUpdate.SetActive(false);
                distanceFull.SetActive(true);
            }

            if (cannon1Stats.damage_lvl == 2)
            {
                damagelvl1.SetActive(true);
                damagelvl2.SetActive(true);
                damagelvl3.SetActive(false);
                damageUpdate.SetActive(true);
                damageFull.SetActive(false);
            }

            if (cannon1Stats.cooldown_lvl == 2)
            {
                cooldownlvl1.SetActive(true);
                cooldownlvl2.SetActive(true);
                cooldownlvl3.SetActive(false);
                cooldownUpdate.SetActive(true);
                cooldownFull.SetActive(false);
            }

            if (cannon1Stats.force_lvl == 2)
            {
                distancelvl1.SetActive(true);
                distancelvl2.SetActive(true);
                distancelvl3.SetActive(false);
                distanceUpdate.SetActive(true);
                distanceFull.SetActive(false);
            }

            if (cannon1Stats.damage_lvl == 1)
            {
                damagelvl1.SetActive(true);
                damagelvl2.SetActive(false);
                damagelvl3.SetActive(false);
                damageUpdate.SetActive(true);
                damageFull.SetActive(false);
            }

            if (cannon1Stats.cooldown_lvl == 1)
            {
                cooldownlvl1.SetActive(true);
                cooldownlvl2.SetActive(false);
                cooldownlvl3.SetActive(false);
                cooldownUpdate.SetActive(true);
                cooldownFull.SetActive(false);
            }

            if (cannon1Stats.force_lvl == 1)
            {
                distancelvl1.SetActive(true);
                distancelvl2.SetActive(false);
                distancelvl3.SetActive(false);
                distanceUpdate.SetActive(true);
                distanceFull.SetActive(false);
            }

            if (cannon1Stats.damage_lvl == 0)
            {
                damagelvl1.SetActive(false);
                damagelvl2.SetActive(false);
                damagelvl3.SetActive(false);
                damageUpdate.SetActive(true);
                damageFull.SetActive(false);
            }

            if (cannon1Stats.cooldown_lvl == 0)
            {
                cooldownlvl1.SetActive(false);
                cooldownlvl2.SetActive(false);
                cooldownlvl3.SetActive(false);
                cooldownUpdate.SetActive(true);
                cooldownFull.SetActive(false);
            }

            if (cannon1Stats.force_lvl == 0)
            {
                distancelvl1.SetActive(false);
                distancelvl2.SetActive(false);
                distancelvl3.SetActive(false);
                distanceUpdate.SetActive(true);
                distanceFull.SetActive(false);
            }
            if (coins.coins >= 20 && cannon1Stats.damage_lvl != 3)
            {
                distanceUpdate.SetActive(true);
            }
            else
            {
                distanceUpdate.SetActive(false);
            }

            if (coins.coins >= 25 && cannon1Stats.cooldown_lvl != 3)
            {
                cooldownUpdate.SetActive(true);
            }
            else
            {
                cooldownUpdate.SetActive(false);
            }

            if (coins.coins >= 30 && cannon1Stats.force_lvl != 3)
            {
                damageUpdate.SetActive(true);
            }
            else
            {
                damageUpdate.SetActive(false);
            }
        }
        

    }

    public void onClickUpdate()
    {
        UseUpdatePanel.SetActive(false );
        flag = true;
        updatePanel.SetActive(true);
        crosshair.SetActive(false);
        joystick.SetActive(false);
        touchPad.SetActive(false);
    }

    public void onKrestick()
    {
        UseUpdatePanel.SetActive(true);
        flag = false;
        updatePanel.SetActive(false);
        crosshair.SetActive(true);
        joystick.SetActive(true);
        touchPad.SetActive(true);
    }

    public void DamageUpdate()
    {
        cannon1Stats.damage_lvl++;
        coins.coins -= 30;
        cannon1Stats.damage *= 1.26f;
    }
    public void CooldownUpdate()
    {
        cannon1Stats.cooldown_lvl++;
        coins.coins -= 25;
        cannon1Stats.cooldown /= 1.26f;
    }
    public void DistanceUpdate()
    {
        cannon1Stats.force_lvl++;
        coins.coins -= 20;
        cannon1Stats.force *= 1.26f;
    }

}
