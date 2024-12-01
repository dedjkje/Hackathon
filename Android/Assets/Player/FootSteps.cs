using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] AudioListener audioListener;
    [SerializeField] AudioClip step1;
    [SerializeField] AudioClip step2;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void footStep1()
    {
        audioSource.PlayOneShot(step1);

    }
    public void footStep2()
    {

        audioSource.PlayOneShot(step2);

    }
}
