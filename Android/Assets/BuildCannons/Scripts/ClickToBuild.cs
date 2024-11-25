using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToBuild : MonoBehaviour
{

    Spawner spawner;
    [SerializeField] private GameObject cannon;
    [SerializeField] private Camera camera;
    void Awake()
    {
        spawner = GameObject.FindWithTag("NetworkManager").GetComponent<Spawner>();
    }
    public void onClick()
    {

        //�������� �������
        transform.parent.gameObject.SetActive(false);

        //��������� �����, ����� ��������������� ���� ��� ��������� � ������� �����
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        //�����
        //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("BuildZone"))
            {
                //���� �� � ��� ��� �����?
                //������� ����� �� ������ �������� � ������������ �
                spawner.Spawn(0, new Vector3(hit.transform.position.x, hit.transform.position.y - 1, hit.transform.position.z), Quaternion.identity);
                hit.transform.gameObject.SetActive(false);
                hit.transform.parent.gameObject.GetComponent<MeshRenderer>().enabled = false;
                
            }
        }
    }

    

}
