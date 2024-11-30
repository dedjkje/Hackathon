using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
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
            
            
            PhotonNetwork.Instantiate("Dim",transform.position, Quaternion.identity);
            
            times--;
            PhotonNetwork.Instantiate("Dim",transform.position, Quaternion.identity);
            timer = 0.0f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
