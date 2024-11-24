using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonSpawn : MonoBehaviour
{

    private Material material; //�������� �����

    [SerializeField] private Canvas canvasPrefab; // ������ Canvas � �������
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // �������� ������� ������������ �������
    [SerializeField] private float countdownTime = 60f; // ������������ ������� � ��������

    private Canvas canvasInstance;
    private float remainingTime;
    private TMP_Text timerText;

    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
        ChangeTrans(0.5f);

        canvasInstance = Instantiate(canvasPrefab, transform.position + offset, Quaternion.identity);
        canvasInstance.transform.SetParent(transform); // ����������� Canvas � �������
        canvasInstance.worldCamera = Camera.main; // ������������� ������ ��� UI

        // �������� ��������� Text
        timerText = canvasInstance.GetComponentInChildren<TMP_Text>();

        // ������������� ��������� �����
        remainingTime = countdownTime;
        UpdateTimerText();

    }


    void Update()
    {
        // ����������� �����
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0); // �� ��������� ������������� ��������
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
        //������������� ������������ ���������
        Color oldColor = material.color;
        material.color = new Color(oldColor.r,oldColor.g,oldColor.b,val);
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // ����������� ���������� ����� � ������ � �������
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

}
