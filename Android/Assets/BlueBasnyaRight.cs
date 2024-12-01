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
        if (health < 100 && stage1play)
        {
            photonView.RPC("Stage_1", RpcTarget.AllBuffered);
            stage1play = false;
            
        }
        if (health < 100 - 100f / 7 && stage2play)
        {
            photonView.RPC("Stage_2", RpcTarget.AllBuffered);
            stage2play = false;
        }
        if (health < 100 - 100f / 7 * 2 && stage3play)
        {
            photonView.RPC("Stage_3", RpcTarget.AllBuffered);
            stage3play = false;
        }
        if (health < 100 - 100f / 7 * 3 && stage4play)
        {
            photonView.RPC("Stage_4", RpcTarget.AllBuffered);
            stage4play = false;
        }
        if (health < 100 - 100f / 7 * 4 && stage5play)
        {
            photonView.RPC("Stage_5", RpcTarget.AllBuffered);
            stage5play = false;
        }
        if (health < 100 - 100f / 7 * 5 && stage6play)
        {
            photonView.RPC("Stage_6", RpcTarget.AllBuffered);
            stage6play = false;
        }
        if (health < 100 - 100f / 7 * 6 && stage7play)
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
        if (PhotonView.Find(ViewID) != null)
        {
            Destroy(PhotonView.Find(ViewID).gameObject);
        }
    }
    [PunRPC]
    void GiveRigidbody(int ViewID)
    {
        foreach (Transform child in PhotonView.Find(ViewID).gameObject.transform)
        {
            if (child.gameObject.name != "Право(R)(Clone)" && child.gameObject.name != "Pravaya")
            {
                if (child.TryGetComponent<MeshCollider>(out MeshCollider m))
                {
                    child.GetComponent<MeshCollider>().convex = true;
                }
                if (!child.TryGetComponent<PhotonRigidbodyView>(out PhotonRigidbodyView a))
                {
                    child.AddComponent<PhotonRigidbodyView>();
                }
                
            }
        }
    }
    
    [PunRPC]
    void DelTransform(int ViewID)
    {
        PhotonView.Find(ViewID).GetComponent<PhotonTransformViewClassic>().enabled = false;
    }
    [PunRPC]
    void GiveRigidbodyToObject(int ViewID)
    {
        if(PhotonView.Find(ViewID).TryGetComponent<MeshCollider>(out MeshCollider m))
        {
            PhotonView.Find(ViewID).GetComponent<MeshCollider>().convex = true;
        }
        
        if(!PhotonView.Find(ViewID).TryGetComponent<PhotonRigidbodyView>(out PhotonRigidbodyView a))
        {
            PhotonView.Find(ViewID).AddComponent<PhotonRigidbodyView>();
        }
        
    }
    [PunRPC]
    void Stage_1()
    {
        //a.transform.parent = null;
        //a.transform.parent = gameObject.transform;

        photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("RED BASNYA RIGHT").gameObject.GetComponent<PhotonView>().ViewID);
       //PhotonNetwork.Destroy(transform.Find("RED BASNYA RIGHT").gameObject);
       //foreach (Transform child in transform.Find("Право(R)(Clone)"))
        //{
            //child.gameObject.AddComponent<PhotonView>();
        //    child.gameObject.AddComponent<MeshCollider>();
            
       // }
       // transform.Find("Право(R)(Clone)").Find("1-1").AddComponent<PhotonRigidbodyView>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("1-1").GetComponent<PhotonView>().ViewID);

    }
    [PunRPC]
    void Stage_2()
    {
        //transform.Find("Право(R)(Clone)").Find("2-1").AddComponent<PhotonRigidbodyView>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("2-1").GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    void Stage_3()
    {
        //transform.Find("Право(R)(Clone)").Find("3-1").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("3-2").AddComponent<PhotonRigidbodyView>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("3-1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("3-2").GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    void Stage_4()
    {
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("4-1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("4-2").GetComponent<PhotonView>().ViewID);
        //transform.Find("Право(R)(Clone)").Find("4-1").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("4-2").AddComponent<PhotonRigidbodyView>();
    }
    [PunRPC]
    void Stage_5()
    {
        //transform.Find("Право(R)(Clone)").Find("5-1").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("5-2").AddComponent<PhotonRigidbodyView>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("5-1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("5-2").GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    private void stop(int ViewID)
    {
        PhotonView.Find(ViewID).gameObject.GetComponent<UseCannons>().stopUsingCannon();
    }
    [PunRPC]
    void Stage_6()
    {
        //transform.Find("Право(R)(Clone)").Find("6-1").AddComponent<PhotonRigidbodyView>();
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("6-1").GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    void Stage_7()
    {
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("7-1").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("7-2").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("7-3").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("7-4").GetComponent<PhotonView>().ViewID);
        //transform.Find("Право(R)(Clone)").Find("7-1").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("7-2").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("7-3").AddComponent<PhotonRigidbodyView>();
        //transform.Find("Право(R)(Clone)").Find("7-4").AddComponent<PhotonRigidbodyView>();
    }
    
    [PunRPC]
    void Stage_8()
    {
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").gameObject.GetComponent<PhotonView>().ViewID);
        //foreach (Transform child in transform.Find("Право(R)(Clone)"))
        //{
        //    child.gameObject.GetComponent<MeshCollider>().convex = true;
        //    child.gameObject.AddComponent<Rigidbody>();
        //}
        //PhotonNetwork.Destroy(transform.Find("Право(R)(Clone)").Find("floor").gameObject);
        //foreach (Transform child in transform)
        //{
        //    if (child.gameObject.name != "Право(R)(Clone)")
        //    {
        //        child.gameObject.GetComponent<MeshCollider>().convex = true;
        //        child.gameObject.AddComponent<Rigidbody>();
        //    }
        //}
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, GetComponent<PhotonView>().ViewID);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, transform.Find("Право(R)(Clone)").Find("Cube.046_cell.067").gameObject.GetComponent<PhotonView>().ViewID);
        photonView.RPC("Delete", RpcTarget.AllBuffered, 484);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            photonView.RPC("RemoveBoxColliderRPC", RpcTarget.AllBuffered);
        }
        //photonView.RPC("Delete", RpcTarget.AllBuffered, 164);
        //photonView.RPC("Delete", RpcTarget.AllBuffered, 170);
        photonView.RPC("Delete", RpcTarget.AllBuffered, 590);
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
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 20);
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 21);
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 22);
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 23);
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 24);
        //photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, 165);
        photonView.RPC("GiveRigidbody", RpcTarget.AllBuffered, 772);
        
        //GameObject.Find("Player 1(Clone)").GetComponent<UseCannons>().stopUsingCannon();
        photonView.RPC("stop", RpcTarget.AllBuffered, GameObject.Find("Player 1(Clone)").GetComponent<PhotonView>().ViewID);
        photonView.RPC("GiveRigidbodyToObject", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
        photonView.RPC("DelTransform", RpcTarget.AllBuffered, cannon.GetComponent<PhotonView>().ViewID);
       // photonView.RPC("Untag", RpcTarget.AllBuffered, cannonM.GetComponent<PhotonView>().ViewID);
    }
}