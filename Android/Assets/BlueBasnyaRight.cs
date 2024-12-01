using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class BlueBasnyaRight : MonoBehaviourPunCallbacks
{
    public float health = 100;
    public Image hp;
    public Image damage;
    Vector2 width;
    public float maxhp;
    [SerializeField] GameObject[] Stage1;
    [SerializeField] GameObject[] Stage2;
    [SerializeField] GameObject[] Stage3;
    [SerializeField] GameObject[] Stage4;
    [SerializeField] GameObject[] Stage5;
    [SerializeField] GameObject[] Stage6;
    [SerializeField] GameObject[] Stage7;
    [SerializeField] GameObject[] Stage8;
    bool stage1play = true;
    bool stage2play = true;
    bool stage3play = true;
    bool stage4play = true;
    bool stage5play = true;
    bool stage6play = true;
    bool stage7play = true;
    bool stage8play = true;

    // Start is called before the first frame update
    void Start()
    {
        maxhp = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 100 && stage1play)
        {
            photonView.RPC("Stage_1", RpcTarget.All);
            stage1play = false;
        }
        if (health < 100 - 100f / 7 && stage2play)
        {
            photonView.RPC("Stage_2", RpcTarget.All);
            stage2play = false;
        }
        if (health < 100 - 100f / 7 * 2 && stage3play)
        {
            photonView.RPC("Stage_3", RpcTarget.All);
            stage3play = false;
        }
        if (health < 100 - 100f / 7 * 3 && stage4play)
        {
            photonView.RPC("Stage_4", RpcTarget.All);
            stage4play = false;
        }
        if (health < 100 - 100f / 7 * 4 && stage5play)
        {
            photonView.RPC("Stage_5", RpcTarget.All);
            stage5play = false;
        }
        if (health < 100 - 100f / 7 * 5 && stage6play)
        {
            photonView.RPC("Stage_6", RpcTarget.All);
            stage6play = false;
        }
        if (health < 100 - 100f / 7 * 6 && stage7play)
        {
            photonView.RPC("Stage_7", RpcTarget.All);
            stage7play = false;
        }
        if (health == 0 && stage8play)
        {
            photonView.RPC("Stage_8", RpcTarget.All);
            stage8play = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�����");
        if (collision.gameObject.tag == "Shell")
        {
            if (collision.gameObject.GetComponent<Damage>().damage > health)
            {
                GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += health;
                health -= health;
                hp.rectTransform.localScale = new Vector2(0, hp.rectTransform.localScale.y);
                StartCoroutine(DecreaseHealthBar());
                PhotonNetwork.Destroy(collision.gameObject);

            }
            else
            {
                health -= collision.gameObject.GetComponent<Damage>().damage;
                hp.rectTransform.localScale = new Vector2(health / maxhp, hp.rectTransform.localScale.y);
                StartCoroutine(DecreaseHealthBar());
                GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += collision.gameObject.GetComponent<Damage>().damage;
            }
        }

    }

    private IEnumerator DecreaseHealthBar()
    {
        // ���� 0.5 �������
        yield return new WaitForSeconds(0.5f);

        // ������� ���������� ������� � ������� 0.5 ������
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

        // ��������, ��� �� ���������� ������������� ��������
        damage.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
    }
    [PunRPC]
    void RemoveBoxColliderRPC()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }
    [PunRPC]
    void Delete(int ViewID)
    {
        Destroy(PhotonView.Find(ViewID).gameObject);
    }
    [PunRPC]
    void GiveRigidbody(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            if (child.gameObject.name != "�����(R)(Clone)")
            {
                child.GetComponent<MeshCollider>().convex = true;
                child.AddComponent<PhotonRigidbodyView>();
            }
        }
    }
    [PunRPC]
    void GiveRigidbodyToObject(int ViewID)
    {
        PhotonView.Find(ViewID).GetComponent<MeshCollider>().convex = true;
        PhotonView.Find(ViewID).AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_1()
    {
        //a.transform.parent = null;
        //a.transform.parent = gameObject.transform;

        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("RED BASNYA RIGHT").gameObject.GetComponent<PhotonView>().ViewID);
       //PhotonNetwork.Destroy(transform.Find("RED BASNYA RIGHT").gameObject);
        foreach (Transform child in transform.Find("�����(R)(Clone)"))
        {
            //child.gameObject.AddComponent<PhotonView>();
            child.gameObject.AddComponent<MeshCollider>();
            
        }
        transform.Find("�����(R)(Clone)").Find("1-1").AddComponent<PhotonRigidbodyView>();

    }
    [PunRPC]
    void Stage_2()
    {
        transform.Find("�����(R)(Clone)").Find("2-1").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_3()
    {
        transform.Find("�����(R)(Clone)").Find("3-1").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("3-2").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_4()
    {
        transform.Find("�����(R)(Clone)").Find("4-1").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("4-2").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_5()
    {
        transform.Find("�����(R)(Clone)").Find("5-1").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("5-2").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_6()
    {
        transform.Find("�����(R)(Clone)").Find("6-1").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_7()
    {
        transform.Find("�����(R)(Clone)").Find("7-1").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("7-2").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("7-3").AddComponent<PhotonRigidbodyView>();
        transform.Find("�����(R)(Clone)").Find("7-4").AddComponent<PhotonRigidbodyView>();
    }
    
    [PunRPC]
    void Stage_8()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)(Clone)").gameObject.GetComponent<PhotonView>().ViewID);
        //foreach (Transform child in transform.Find("�����(R)(Clone)"))
        //{
        //    child.gameObject.GetComponent<MeshCollider>().convex = true;
        //    child.gameObject.AddComponent<Rigidbody>();
        //}
        //PhotonNetwork.Destroy(transform.Find("�����(R)(Clone)").Find("floor").gameObject);
        //foreach (Transform child in transform)
        //{
        //    if (child.gameObject.name != "�����(R)(Clone)")
        //    {
        //        child.gameObject.GetComponent<MeshCollider>().convex = true;
        //        child.gameObject.AddComponent<Rigidbody>();
        //    }
        //}
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("�����(R)(Clone)").Find("Cube.046_cell.067").gameObject.GetComponent<PhotonView>().ViewID);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);
        }
        photonView.RPC("Delete", RpcTarget.AllBuffered, 164);
        photonView.RPC("Delete", RpcTarget.AllBuffered, 170);
        //PhotonView.Find(20).GetComponent<MeshCollider>().convex = true;
        //PhotonView.Find(20).AddComponent<Rigidbody>();
        //PhotonView.Find(21).GetComponent<MeshCollider>().convex = true;
        //PhotonView.Find(21).AddComponent<Rigidbody>();
        //PhotonView.Find(22).GetComponent<MeshCollider>().convex = true;
        //PhotonView.Find(22).AddComponent<Rigidbody>();
        //PhotonView.Find(23).GetComponent<MeshCollider>().convex = true;
        //PhotonView.Find(23).AddComponent<Rigidbody>();
        //PhotonView.Find(24).GetComponent<MeshCollider>().convex = true;
        //PhotonView.Find(24).AddComponent<Rigidbody>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 20);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 21);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 22);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 23);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 24);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 165);

    }
}