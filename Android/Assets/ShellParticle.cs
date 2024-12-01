using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShellParticle : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem system;
    public float interval = 0.03f; // Интервал в секундах
    private float timer = 0.0f;
    public int times = 5;
    public float lifeTime;
    [PunRPC]
    public void Delete(int ViewID)
    {
        Destroy(PhotonView.Find(ViewID).gameObject);
    }
    void Start()
    {
        lifeTime = Time.time;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(lifeTime>1)
    //    {
    //        Delete(GetComponent<PhotonView>().ViewID);
    //    }
        
    //}
    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= interval && times > 0)
        {
            
            
            PhotonNetwork.Instantiate("Dim",transform.position, Quaternion.identity);
            
            times--;
            
            timer = 0.0f;
        }
    }
}
