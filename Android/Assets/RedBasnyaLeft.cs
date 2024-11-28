using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBasnyaLeft : MonoBehaviour
{ 
    public float health = 100;
    public Image hp;
    Vector2 width;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попал");
        if (collision.gameObject.tag == "Shell")
        {
            health -= collision.gameObject.GetComponent<Damage>().damage;
            Debug.Log(collision.gameObject.GetComponent<Damage>().damage);
            hp.fillAmount = health / 100;
        }
        
    }

    
}
