using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RedBasnyaLeft : MonoBehaviourPunCallbacks
{ 
    public float health = 100;
    public Image hp;
    public Image damage;
    Vector2 width;
    public float maxhp;
    bool stage1play;
    bool stage2play;
    bool stage3play;
    bool stage4play;
    bool stage5play;
    bool stage6play;
    bool stage7play ;
    bool stage8play;
    // Start is called before the first frame update
    void Start()
    {
        maxhp = health;
        stage1play = true;
        stage2play = true;
        stage3play = true;
        stage4play = true;
        stage5play = true;
        stage6play = true;
        stage7play = true;
        stage8play = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 100 && stage1play)
        {
            Stage_1();
            stage1play = false;
        }
        if (health < 100 - 100f / 7 && stage2play)
        {
            Stage_2();
            stage2play = false;
        }
        if (health < 100 - 100f / 7 * 2 && stage3play)
        {
            Stage_3();
            stage3play = false;
        }
        if (health < 100 - 100f / 7 * 3 && stage4play)
        {
            Stage_4();
            stage4play = false;
        }
        if (health < 100 - 100f / 7 * 4 && stage5play)
        {
            Stage_5();
            stage5play = false;
        }
        if (health < 100 - 100f / 7 * 5 && stage6play)
        {
            Stage_6();
            stage6play = false;
        }
        if (health < 100 - 100f / 7 * 6 && stage7play)
        {
            Stage_7();
            stage7play = false;
        }
        if (health == 0 && stage8play)
        {
            Stage_8();
            stage8play = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попал");
        if (collision.gameObject.tag == "Shell")
        {
            if (collision.gameObject.GetComponent<Damage>().damage > health)
            {
                GameObject.Find("Player 1(Clone)").GetComponent<Coins>().coins += health;
                health -= health;
                hp.rectTransform.localScale = new Vector2(0, hp.rectTransform.localScale.y);
                StartCoroutine(DecreaseHealthBar());
                
            }
            else
            {
                health -= collision.gameObject.GetComponent<Damage>().damage;
                hp.rectTransform.localScale = new Vector2(health / maxhp, hp.rectTransform.localScale.y);
                StartCoroutine(DecreaseHealthBar());
                GameObject.Find("Player 1(Clone)").GetComponent<Coins>().coins += collision.gameObject.GetComponent<Damage>().damage;
            }
        }
        
    }

    private IEnumerator DecreaseHealthBar()
    {
        // Ждем 0.5 секунды
        yield return new WaitForSeconds(0.5f);

        // Плавное уменьшение размера в течение 0.5 секунд
        float targetScaleX = health / maxhp;
        float currentScaleX = hp.rectTransform.localScale.x;
        float duration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += 0.05f;
            float newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / duration);
            damage.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            yield return new WaitForSeconds(0.05f);
        }

        // Убедимся, что мы установили окончательное значение
        damage.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
    }
    [PunRPC]
    public void RemoveBoxColliderRPC()
    {
        foreach (BoxCollider a in GetComponents<BoxCollider>())
        {
            a.enabled = false;
        }
    }
    [PunRPC]
    public void Delete(int ViewID)
    {
        Destroy(PhotonView.Find(ViewID).gameObject);
    }
    [PunRPC]
    public void AddConvex(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            child.GetComponent<MeshCollider>().convex = true;

        }
    }
    [PunRPC]
    public void GiveRigidbody(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            child.GetComponent<MeshCollider>().convex = true;
            child.AddComponent<Rigidbody>();
        }
    }
    void GiveRigidBody(GameObject a)
    {
        foreach (Transform t in a.transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();

        }
    }
    void Stage_1()
    {
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("RED BASHNYA LEFT").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("1").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("1").GetComponent<PhotonView>().gameObject);
        foreach(Transform t in transform.Find("Левая(R)").Find("1").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_2()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("2").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("2").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("2").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_3()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("3").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("3").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("3").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_4()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("4").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("4").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("4").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_5()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("5").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("5").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("5").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_6()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("6").GetComponent<PhotonView>().ViewID);
        // GiveRigidBody(transform.Find("Левая(R)").Find("6").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("6").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_7()
    {
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("7").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("7").GetComponent<PhotonView>().gameObject);
        foreach (Transform t in transform.Find("Левая(R)").Find("7").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
    }
    void Stage_8()
    {
        photonView.RPC("Delete", RpcTarget.AllBuffered, GameObject.Find("ZAMOOOOMK (1)").transform.Find("delete").GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, GameObject.Find("ZAMOOOOMK (1)").transform.Find("ЛевоЦентр(R)").transform.Find("del").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("TargetBoxColliderRPC",RpcTarget.AllBuffered,a,b,c);
        // photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("other").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("AddConvex", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("other").GetComponent<PhotonView>().ViewID);
        // photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("another").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("AddConvex", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("other").GetComponent<PhotonView>().ViewID);
        foreach (Transform t in transform.Find("Левая(R)").Find("other").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }

        //GiveRigidBody(transform.Find("Левая(R)").Find("another").GetComponent<PhotonView>().gameObject);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("del1").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("another").Find("Cube (1)").GetComponent<PhotonView>().ViewID);
        
        foreach (Transform t in GameObject.Find("ZAMOOOOMK (1)").transform.Find("ЛевоЦентр(R)").transform.Find("GameObject (2)").transform)
        {
            t.GetComponent<MeshCollider>().convex = true;
            t.AddComponent<Rigidbody>();
        }
        photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);
    }
}
