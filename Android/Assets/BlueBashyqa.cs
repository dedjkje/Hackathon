using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BlueBashyqa : MonoBehaviourPunCallbacks
{
    public float health = 100;
    public Image hp;
    Vector2 width;
    public float maxhp;
    public Image damage;
    // Start is called before the first frame update
    void Start()
    {
        maxhp = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    [PunRPC] public void addCoins(int ViewID, float value)
    {
        PhotonView.Find(ViewID).GetComponent<Coins>().coins += value;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�����");
        if (collision.gameObject.tag == "Shell")
        {
            if (collision.gameObject.GetComponent<Damage>().damage > health)
            {
                //GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += health;
                photonView.RPC("addCoins", RpcTarget.AllBuffered, GameObject.Find("Player 2(Clone)").GetComponent<PhotonView>().ViewID, health);
                health -= health;
                hp.rectTransform.localScale = new Vector2(0, hp.rectTransform.localScale.y);
                //StartCoroutine(DecreaseHealthBar());
                photonView.RPC("DecreaseHealthBar", RpcTarget.All);
            }
            else
            {
                health -= collision.gameObject.GetComponent<Damage>().damage;
                hp.rectTransform.localScale = new Vector2(health / maxhp, hp.rectTransform.localScale.y);
                //StartCoroutine(DecreaseHealthBar());
                photonView.RPC("DecreaseHealthBar", RpcTarget.All);
                //GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += collision.gameObject.GetComponent<Damage>().damage;
                photonView.RPC("addCoins", RpcTarget.AllBuffered, GameObject.Find("Player 2(Clone)").GetComponent<PhotonView>().ViewID, damage);
            }
            
        }

    }
    [PunRPC]
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
            elapsedTime += Time.deltaTime;
            float newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / duration);
            damage.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            yield return null; // ���� ��������� ����
        }

        // ��������, ��� �� ���������� ������������� ��������
        damage.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
    }
}
