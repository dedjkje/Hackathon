using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    // Поле для ScrollBar
    [SerializeField] public Scrollbar volumeSlider;

    // Начальная громкость
    private float initialVolume = 30f;

    void Start()
    {
        // Устанавливаем начальную громкость при старте игры
        SetVolume(initialVolume);

        // Устанавливаем значение ползунка на начальной громкости
        if (volumeSlider != null)
        {
            volumeSlider.value = initialVolume / 100f;
        }
    }

    // Метод для изменения громкости
    public void OnVolumeChanged(float newValue)
    {
        // Преобразуем значение от 0 до 1 обратно в диапазон 0-100
        float volume = newValue * 100f;
        SetVolume(volume);
    }

    // Метод для установки громкости
    private void SetVolume(float volume)
    {
        AudioListener.volume = volume / 100f;
    }
}
