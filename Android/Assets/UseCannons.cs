using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class UseCannons : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject useUpgradePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 1f))
        {
            Transform objectHit = hit.transform;
            if (objectHit.CompareTag("Cannon1") ||
                objectHit.CompareTag("Cannon2") ||
                objectHit.CompareTag("Cannon3") ||
                objectHit.CompareTag("Cannon4") ||
                objectHit.CompareTag("Cannon5") ||
                objectHit.CompareTag("Cannon6"))
            {
                useUpgradePanel.SetActive(true);
            }
            else
            {
                useUpgradePanel.SetActive(false);
            }

        }
    }
}
