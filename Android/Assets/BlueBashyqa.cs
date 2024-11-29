using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBashyqa : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Попал");
        if (collision.gameObject.tag == "Shell")
        {
            health -= collision.gameObject.GetComponent<Damage>().damage;
            Debug.Log(collision.gameObject.GetComponent<Damage>().damage);
            hp.rectTransform.localScale = new Vector2(health / maxhp, hp.rectTransform.localScale.y);
            StartCoroutine(DecreaseHealthBar());
            GameObject.Find("Player 2(Clone)").GetComponent<Coins>().coins += collision.gameObject.GetComponent<Damage>().damage;
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
            elapsedTime += Time.deltaTime;
            float newScaleX = Mathf.Lerp(currentScaleX, targetScaleX, elapsedTime / duration);
            damage.rectTransform.localScale = new Vector2(newScaleX, hp.rectTransform.localScale.y);
            yield return null; // Ждем следующий кадр
        }

        // Убедимся, что мы установили окончательное значение
        damage.rectTransform.localScale = new Vector2(targetScaleX, hp.rectTransform.localScale.y);
    }
}
