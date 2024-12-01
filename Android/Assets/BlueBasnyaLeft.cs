using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlueBasnyaLeft : MonoBehaviourPunCallbacks
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
    bool stage7play;
    bool stage8play;
    bool canDelete;
    public GameObject cannon;
    public GameObject cannonM;
    // Start is called before the first frame update
    void Start()
    {
        canDelete = true;
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
        Debug.Log("Попал");
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

            // Вызов метода для синхронизации состояния здоровья
            photonView.RPC("UpdateHealthAndDecreaseBar", RpcTarget.All, health, maxhp);
        }
    }
    [PunRPC]
    private void UpdateHealthAndDecreaseBar(float currentHealth, float maximumHealth)
    {
        health = currentHealth; // Обновить текущее здоровье
        hp.rectTransform.localScale = new Vector2(currentHealth / maximumHealth, hp.rectTransform.localScale.y);

        // Запустить корутину для анимации изменения полосы здоровья
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
            elapsedTime += Time.deltaTime; // Использует время в игре для корректного управления
            float newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / duration);
            hp.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            damage.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            yield return new WaitForSeconds(0.05f); // Ждём следующего кадра
        }

        hp.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
        damage.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
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
            child.AddComponent<PhotonRigidbodyView>();
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
    [PunRPC] void DelTransform(int ViewID)
    {
        PhotonView.Find(ViewID).GetComponent<PhotonTransformViewClassic>().enabled = false;
    }
    [PunRPC]
    void Stage_1()
    {
        if (canDelete)
        {
            photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("RED BASHNYA LEFT").GetComponent<PhotonView>().ViewID);
            canDelete = false;
        }
       
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("1").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("1").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("1").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
       // }
    }
    [PunRPC]
    void Stage_2()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("2").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("2").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("2").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
    }
    [PunRPC]
    void Stage_3()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("3").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("3").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("3").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
    }
    [PunRPC]
    void Stage_4()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("4").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("4").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("4").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
    }
    [PunRPC] void Untag(int ViewID)
    {
        PhotonView.Find(ViewID).gameObject.tag = null;
    }
    [PunRPC]
    void Stage_5()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("5").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("5").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("5").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
    }
    [PunRPC]
    void Stage_6()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("6").GetComponent<PhotonView>().ViewID);
        // GiveRigidBody(transform.Find("Левая(R)").Find("6").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("6").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
    }
    [PunRPC]
    void Stage_7()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("7").GetComponent<PhotonView>().ViewID);
        //GiveRigidBody(transform.Find("Левая(R)").Find("7").GetComponent<PhotonView>().gameObject);
        //foreach (Transform t in transform.Find("Левая(R)").Find("7").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
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
    private void stop(int ViewID)
    {
        PhotonView.Find(ViewID).gameObject.GetComponent<UseCannons>().stopUsingCannon();
    }
    [PunRPC]
    void Stage_8()
    {
        photonView.RPC("Delete", RpcTarget.AllBuffered, GameObject.Find("ZAMOOOOMK (2)").transform.Find("delete").GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, GameObject.Find("ZAMOOOOMK (2)").transform.Find("ЛевоЦентр(R)").transform.Find("del").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("TargetBoxColliderRPC",RpcTarget.AllBuffered,a,b,c);
        // photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Центр(R)").Find("other").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("AddConvex", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("other").GetComponent<PhotonView>().ViewID);
        
        //foreach (Transform t in transform.Find("Левая(R)").Find("other").transform)
        //{
        //    t.GetComponent<MeshCollider>().convex = true;
        //    t.AddComponent<Rigidbody>();
        //}
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("other").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("another").GetComponent<PhotonView>().ViewID);
        
        //GiveRigidBody(transform.Find("Левая(R)").Find("another").GetComponent<PhotonView>().gameObject);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("del1").GetComponent<PhotonView>().ViewID);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("Левая(R)").Find("another").Find("Cube (1)").GetComponent<PhotonView>().ViewID);

       // foreach (Transform t in GameObject.Find("ZAMOOOOMK (2)").transform.Find("ЛевоЦентр(R)").transform.Find("GameObject (2)").transform)
       // {
       //     t.GetComponent<MeshCollider>().convex = true;
       //     t.AddComponent<Rigidbody>();
       // }
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, GameObject.Find("ZAMOOOOMK (2)").transform.Find("ЛевоЦентр(R)").transform.Find("GameObject (2)").GetComponent<PhotonView>().ViewID);
        photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);

        //GameObject.Find("Player 1(Clone)").GetComponent<UseCannons>().stopUsingCannon();
        photonView.RPC("stop", RpcTarget.AllBuffered, GameObject.Find("Player 1(Clone)").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
        photonView.RPC("DelTransform", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
        //photonView.RPC("Untag", RpcTarget.AllBuffered, cannonM.GetComponent<PhotonView>().ViewID);
    }
}

