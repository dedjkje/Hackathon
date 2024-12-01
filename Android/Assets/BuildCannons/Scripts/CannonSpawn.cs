using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonSpawn : MonoBehaviour
{
    Rules rules;
    private Material material; //�������� �����

    [SerializeField] private Canvas canvasPrefab; // ������ Canvas � �������
    private Vector3 offset = new Vector3(0.8f, 2, 0); // �������� ������� ������������ �������
    [SerializeField] private float countdownTime = 60f; // ������������ ������� � ��������

    private Canvas canvasInstance;
    private float remainingTime;
    private TMP_Text timerText;

    void Start()
    {
        //material = gameObject.GetComponent<Renderer>().material;
        ChangeTrans(0.5f);

        canvasInstance = Instantiate(canvasPrefab, transform.position + offset, Quaternion.identity);
        canvasInstance.transform.Rotate(0, -90, 0);
        if (transform.position.x < 0)
        {
            canvasInstance.transform.position -= new Vector3(offset.x * 2,0,0);
            canvasInstance.transform.Rotate(0, 180, 0);
        }
        canvasInstance.transform.SetParent(transform); // ����������� Canvas � �������
        canvasInstance.worldCamera = Camera.main; // ������������� ������ ��� UI

        // �������� ��������� Text
        timerText = canvasInstance.GetComponentInChildren<TMP_Text>();
        

        // ������������� ��������� �����
        remainingTime = countdownTime;
        UpdateTimerText();

        rules = GameObject.FindWithTag("Rules").GetComponent<Rules>();

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
            timerText.text = "";
            canvasInstance.enabled = false;
            gameObject.GetComponent<CannonSpawn>().enabled = false;
            
            gameObject.tag = $"Cannon{rules.numberOfCannon}";
            rules.numberOfCannon++;
            gameObject.GetComponent<ChangeMaterials>().RestoreOriginalMaterials();
            //gameObject.GetComponent<CannonSpawn>().enabled = false;
        }
    }

    void ChangeTrans(float val)
    {
        //������������� ������������ ���������
        //Color oldColor = material.color;
        //material.color = new Color(oldColor.r,oldColor.g,oldColor.b,val);
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // ����������� ���������� ����� � ������ � �������
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{seconds}";
        }
    }

}
