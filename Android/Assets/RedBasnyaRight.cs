using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;

public class RedBasnyaRight : MonoBehaviour
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
    public PhotonView photonView;
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
            Stage_1();
            stage1play = false;
        }
        if (health < 100 - 100f/7 && stage2play)
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
    
    void Stage_1()
    {
        //a.transform.parent = null;
        //a.transform.parent = gameObject.transform;
        photonView = transform.Find("RED BASNYA LEFT").GetComponent<PhotonView>();
        transform.Find("Право(R)(Clone)").transform.localPosition = new Vector3(0,0,0);
        PhotonNetwork.Destroy(photonView.gameObject);
        foreach (Transform child in transform.Find("Право(R)(Clone)"))
        {
            //child.gameObject.AddComponent<PhotonView>();
            child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.GetComponent<MeshCollider>().convex = true;
            //PhotonView photonViewChild = child.gameObject.GetComponent<PhotonView>();
            // child.gameObject.AddComponent<PhotonTransformViewClassic>();
            child.gameObject.GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = true ;
            child.gameObject.GetComponent<PhotonTransformViewClassic>().m_RotationModel.SynchronizeEnabled = true;
            //photonViewChild.ObservedComponents = new List<Component>() { child.gameObject.GetComponent<PhotonTransformViewClassic>() };
            //photonViewChild.ViewID = PhotonNetwork.AllocateViewID(0);
            //child.gameObject.GetComponent<PhotonTransformViewClassic>().m_PositionModel.SynchronizeEnabled = true;
            //child.gameObject.GetComponent<PhotonTransformViewClassic>().m_RotationModel.SynchronizeEnabled = true;
        }
        transform.Find("Право(R)(Clone)").Find("1-1").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").transform.localPosition = new Vector3(0, 0, 0);
    }
    void Stage_2()
    {
        transform.Find("Право(R)(Clone)").Find("2-1").AddComponent<Rigidbody>();
    }

    void Stage_3()
    {
        transform.Find("Право(R)(Clone)").Find("3-1").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("3-2").AddComponent<Rigidbody>();
    }
    void Stage_4()
    {
        transform.Find("Право(R)(Clone)").Find("4-1").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("4-2").AddComponent<Rigidbody>();
    }
    void Stage_5()
    {
        transform.Find("Право(R)(Clone)").Find("5-1").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("5-2").AddComponent<Rigidbody>();
    }
    void Stage_6()
    {
        transform.Find("Право(R)(Clone)").Find("6-1").AddComponent<Rigidbody>();
    }
    void Stage_7()
    {
        transform.Find("Право(R)(Clone)").Find("7-1").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("7-2").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("7-3").AddComponent<Rigidbody>();
        transform.Find("Право(R)(Clone)").Find("7-4").AddComponent<Rigidbody>();
    }
    void Stage_8()
    {
        foreach(Transform child in transform.Find("Право(R)(Clone)"))
        {
            child.gameObject.AddComponent<Rigidbody>();
        }
        PhotonNetwork.Destroy(transform.Find("Право(R)(Clone)").Find("floor").gameObject);
        foreach (Transform child in transform)
        { 
            child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.AddComponent<Rigidbody>();
        }
        PhotonNetwork.Destroy(transform.Find("Право(R)(Clone)").Find("Cube.046_cell.067").gameObject);
    }
}
