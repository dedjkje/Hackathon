using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMen : MonoBehaviour
{
    [SerializeField] public Scrollbar slider;
    [SerializeField] public float minSensa = 0.1f;
    [SerializeField] public float maxSensa = 1.0f;
    [SerializeField] CameraController cameraController;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSensaChange);
            slider.value = 0.3f;
        }

    }

    void OnSensaChange(float value)
    {
        if (cameraController != null)
        {
            cameraController.sensitivity = value;
        }
    }
}
