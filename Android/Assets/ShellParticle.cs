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
    [PunRPC]
    public void Delete(int ViewID)
    {
        Destroy(PhotonView.Find(ViewID).gameObject);
    }
    void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Cannon1" || collision.gameObject.tag != "Cannon2" || collision.gameObject.tag != "Cannon3" || collision.gameObject.tag != "Cannon4" || collision.gameObject.tag != "Cannon5" || collision.gameObject.tag != "Cannon6")
        Delete(GetComponent<PhotonView>().ViewID);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval && times > 0)
        {
            
            
            PhotonNetwork.Instantiate("Dim",transform.position, Quaternion.identity);
            
            times--;
            
            timer = 0.0f;
        }
    }
}
