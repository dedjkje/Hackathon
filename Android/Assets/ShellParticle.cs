using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellParticle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem system;
    public float interval = 0.03f; // Интервал в секундах
    private float timer = 0.0f;
    public int times = 5;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval && times > 0)
        {
            times--;
            Instantiate(system,transform.position, Quaternion.identity);
            timer = 0.0f;
        }
    }
}
