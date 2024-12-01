using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlueBasnyaCenter : MonoBehaviourPunCallbacks
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
    [SerializeField] BoxCollider a;
    [SerializeField] BoxCollider b;
    [SerializeField] BoxCollider c;
    public GameObject cannon;
    public GameObject cannonM;
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
            photonView.RPC("Stage_1", RpcTarget.AllBuffered);
            stage1play = false;
        }
        if (health < 350 - 350f / 7 && stage2play)
        {
            photonView.RPC("Stage_2", RpcTarget.AllBuffered);
            stage2play = false;
        }
        if (health < 350 - 350f / 7 * 2 && stage3play)
        {
            photonView.RPC("Stage_3", RpcTarget.AllBuffered);
            stage3play = false;
        }
        if (health < 350 - 350f / 7 * 3 && stage4play)
        {
            photonView.RPC("Stage_4", RpcTarget.AllBuffered);
            stage4play = false;
        }
        if (health < 350 - 350f / 7 * 4 && stage5play)
        {
            photonView.RPC("Stage_5", RpcTarget.AllBuffered);
            stage5play = false;
        }
        if (health < 350 - 350f / 7 * 5 && stage6play)
        {
            photonView.RPC("Stage_6", RpcTarget.AllBuffered);
            stage6play = false;
        }
        if (health < 350 - 350f / 7 * 6 && stage7play)
        {
            photonView.RPC("Stage_7", RpcTarget.AllBuffered);
            stage7play = false;
        }
        if (health == 0 && stage8play)
        {
            photonView.RPC("Stage_8", RpcTarget.AllBuffered);
            stage8play = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�����");
        if (collision.gameObject.tag == "Shell")
        {
            Damage damageComponent = collision.gameObject.GetComponent<Damage>();
            float damageAmount = damageComponent.damage;

            if (damageAmount > health)
            {
                GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += health;
                health = 0;
                hp.rectTransform.localScale = new Vector2(0, hp.rectTransform.localScale.y);
            }
            else
            {
                health -= damageAmount;
                hp.rectTransform.localScale = new Vector2(health / maxhp, hp.rectTransform.localScale.y);
                GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += damageAmount;
            }

            // ����� ������ ��� ������������� ��������� ��������
            photonView.RPC("UpdateHealthAndDecreaseBar", RpcTarget.All, health, maxhp);
        }
    }
    [PunRPC]
    private void UpdateHealthAndDecreaseBar(float currentHealth, float maximumHealth)
    {
        health = currentHealth; // �������� ������� ��������
        hp.rectTransform.localScale = new Vector2(currentHealth / maximumHealth, hp.rectTransform.localScale.y);

        // ��������� �������� ��� �������� ��������� ������ ��������
        StartCoroutine(DecreaseHealthBar(currentHealth, maximumHealth));
    }
    private IEnumerator DecreaseHealthBar(float currentHealth, float maximumHealth)
    {
        yield return new WaitForSeconds(0.5f);

        float targetScaleX = currentHealth / maximumHealth;
        float currentScaleX = hp.rectTransform.localScale.x;
        float duration = 0.5f;
        float elapsedTime = 0f;


        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // ���������� ����� � ���� ��� ����������� ����������
            float newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / duration);
            hp.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            damage.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            yield return new WaitForSeconds(0.05f); // ��� ���������� �����
        }

        hp.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
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
    void GiveRigidbodyToObject(int ViewID)
    {
        if (PhotonView.Find(ViewID).TryGetComponent<MeshCollider>(out MeshCollider m))
        {
            PhotonView.Find(ViewID).GetComponent<MeshCollider>().convex = true;
        }

        if (!PhotonView.Find(ViewID).TryGetComponent<PhotonRigidbodyView>(out PhotonRigidbodyView a))
        {
            PhotonView.Find(ViewID).AddComponent<PhotonRigidbodyView>();
        }

    }
    [PunRPC]
    void DelTransform(int ViewID)
    {
        PhotonView.Find(ViewID).GetComponent<PhotonTransformViewClassic>().enabled = false;
    }
    [PunRPC]
    public void GiveRigidbody(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            child.GetComponent<MeshCollider>().convex = true;
            if (!child.TryGetComponent<PhotonRigidbodyView>(out PhotonRigidbodyView photonRigidbodyView))
            {
                child.AddComponent<PhotonRigidbodyView>();
            }
            
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
    [PunRPC]
    void Stage_1()
    {

        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("MAIN BASHNYA RED").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("1").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("1").GetComponent<PhotonView>().gameObject);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("1").GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    void Stage_2()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("2").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("2").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    void Stage_3()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("3").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("3").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    void Stage_4()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("4").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("4").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    void Stage_5()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("5").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("5").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    void Stage_6()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("6").GetComponent<PhotonView>().ViewID);
       // GiveRigidBody(transform.Find("�����(R)").Find("6").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    void Stage_7()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("7").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("7").GetComponent<PhotonView>().gameObject);
    }
    [PunRPC]
    private void stop(int ViewID)
    {
        PhotonView.Find(ViewID).gameObject.GetComponent<UseCannons>().stopUsingCannon();
    }
    [PunRPC]
    void Stage_8()
    {
        //photonView.RPC("TargetBoxColliderRPC",RpcTarget.AllBuffered,a,b,c);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("other").GetComponent<PhotonView>().ViewID);
        photonView.RPC("AddConvex", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("other").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("another").GetComponent<PhotonView>().ViewID);
        photonView.RPC("AddConvex", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("other").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("�����(R)").Find("other").GetComponent<PhotonView>().gameObject);
       // GiveRigidBody(transform.Find("�����(R)").Find("another").GetComponent<PhotonView>().gameObject);
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("del1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("�����(R)").Find("another").Find("Cube (1)").GetComponent<PhotonView>().ViewID);
        photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);
        
        //GameObject.Find("Player 1(Clone)").GetComponent<UseCannons>().stopUsingCannon();
        photonView.RPC("stop", RpcTarget.AllBuffered, GameObject.Find("Player 1(Clone)").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
        photonView.RPC("DelTransform", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
       // photonView.RPC("Untag", RpcTarget.AllBuffered, cannonM.GetComponent<PhotonView>().ViewID);
    }
}
