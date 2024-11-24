using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonSpawn : MonoBehaviour
{

    private Material material; //Материал пушки

    [SerializeField] private Canvas canvasPrefab; // Префаб Canvas с текстом
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // Смещение таймера относительно объекта
    [SerializeField] private float countdownTime = 60f; // Длительность таймера в секундах

    private Canvas canvasInstance;
    private float remainingTime;
    private TMP_Text timerText;

    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        ChangeTrans(0.5f);

        canvasInstance = Instantiate(canvasPrefab, transform.position + offset, Quaternion.identity);
        canvasInstance.transform.SetParent(transform); // Привязываем Canvas к объекту
        canvasInstance.worldCamera = Camera.main; // Устанавливаем камеру для UI

        // Получаем компонент Text
        timerText = canvasInstance.GetComponentInChildren<TMP_Text>();

        // Устанавливаем начальное время
        remainingTime = countdownTime;
        UpdateTimerText();

    }


    void Update()
    {
        // Отсчитываем время
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0); // Не допускаем отрицательных значений
            UpdateTimerText();
        }else{ 
            ChangeTrans(1f);
            canvasInstance.enabled = false;
            gameObject.GetComponent<CannonSpawn>().enabled = false;
            //gameObject.GetComponent<CannonSpawn>().enabled = false;
        }
    }

    void ChangeTrans(float val)
    {
        //Устанавливаем прозрачность материала
        Color oldColor = material.color;
        material.color = new Color(oldColor.r,oldColor.g,oldColor.b,val);
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Преобразуем оставшееся время в минуты и секунды
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

}
