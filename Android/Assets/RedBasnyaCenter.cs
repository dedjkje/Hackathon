using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RedBasnyaCenter : MonoBehaviourPunCallbacks
{ 
    public float health = 350;
    public Image hp;
    public Image damage;
    Vector2 width;
    public float maxhp;
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
        if (health < 350 && stage1play)
        {
            Stage_1();
            stage1play = false;
        }
        if (health < 350 - 350f / 7 && stage2play)
        {
            Stage_2();
            stage2play = false;
        }
        if (health < 350 - 350f / 7 * 2 && stage3play)
        {
            Stage_3();
            stage3play = false;
        }
        if (health < 350 - 350f / 7 * 3 && stage4play)
        {
            Stage_4();
            stage4play = false;
        }
        if (health < 350 - 350f / 7 * 4 && stage5play)
        {
            Stage_5();
            stage5play = false;
        }
        if (health < 350 - 350f / 7 * 5 && stage6play)
        {
            Stage_6();
            stage6play = false;
        }
        if (health < 350 - 350f / 7 * 6 && stage7play)
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
        Debug.Log("�����");
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
    //[PunRPC]
    //void RemoveBoxColliderRPC()
    //{
    //    BoxCollider boxCollider = GetComponent<BoxCollider>();
    //    if (boxCollider != null)
    //    {
    //        boxCollider.enabled = false;
    //    }
    //}
    //[PunRPC]
    //void Delete(int ViewID)
    //{
    //    Destroy(PhotonView.Find(ViewID).gameObject);
    //}
    //[PunRPC]
    //void GiveRigidbody(int ViewID)
    //{
    //    foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
    //    {
    //        child.GetComponent<MeshCollider>().convex = true;
    //        child.AddComponent<Rigidbody>();
    //    }
    //}
    void Stage_1()
    {
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("MAIN BASHNYA RED").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("1").GetComponent<PhotonView>().ViewID);
    }
    void Stage_2()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("2").GetComponent<PhotonView>().ViewID);
    }
    void Stage_3()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("3").GetComponent<PhotonView>().ViewID);
    }
    void Stage_4()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("4").GetComponent<PhotonView>().ViewID);
    }
    void Stage_5()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("5").GetComponent<PhotonView>().ViewID);
    }
    void Stage_6()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("6").GetComponent<PhotonView>().ViewID);
    }
    void Stage_7()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("7").GetComponent<PhotonView>().ViewID);
    }
    void Stage_8()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("other").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("another").GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("del1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);
    }
}
