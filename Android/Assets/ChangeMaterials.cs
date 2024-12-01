using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class ChangeMaterials : MonoBehaviourPunCallbacks
{
    // Словарь для сохранения оригинальных материалов (включая массивы материалов)
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // Полупрозрачные цвета
    [SerializeField] Material transparentBlue;
    [SerializeField] Material transparentRed;

    // Метод для сохранения оригинальных материалов и их изменения на прозрачный синий
    
    public void ChangeToTransparentBlue()
    {
        photonView.RPC("TransparentBlue", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TransparentBlue()
    {
        // Получаем все компоненты Renderer в дочерних объектах
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        // Сохраняем оригинальные материалы и применяем новый цвет
        foreach (var renderer in renderers)
        {
            // Проверяем, есть ли у объекта материалы
            if (renderer.materials.Length > 0)
            {
                // Сохраняем оригинальные материалы
                originalMaterials[renderer] = renderer.materials;

                // Создаем новый массив материалов на основе оригинальных
                Material[] newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    // Клонируем оригинальный материал
                    newMaterials[i] = new Material(renderer.materials[i]);

                    // Устанавливаем полупрозрачный цвет
                    newMaterials[i] = transparentBlue;

                    // Изменяем Surface Type на Transparent, если свойство существует
                    if (newMaterials[i].HasProperty("_Surface"))
                    {
                        newMaterials[i].SetFloat("_Surface", 1.0f); // 1.0f для Transparent
                        newMaterials[i].SetFloat("_Blend", 1.0f); // Убедитесь, что используется альфа-смешивание
                        newMaterials[i].SetInt("_ZWrite", 0); // Выключаем запись в Z
                        newMaterials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent; // Установите на Transparent
                    }
                }

                // Подменяем материалы на новом массиве
                renderer.materials = newMaterials;
            }
        }
    }

    // Метод для сохранения оригинальных материалов и их изменения на прозрачный красный
    public void ChangeToTransparentRed()
    {
        photonView.RPC("TransparentRed", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TransparentRed()
    {
        // Получаем все компоненты Renderer в дочерних объектах
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        // Сохраняем оригинальные материалы и применяем новый цвет
        foreach (var renderer in renderers)
        {
            // Проверяем, есть ли у объекта материалы
            if (renderer.materials.Length > 0)
            {
                // Сохраняем оригинальные материалы
                originalMaterials[renderer] = renderer.materials;

                // Создаем новый массив материалов на основе оригинальных
                Material[] newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    // Клонируем оригинальный материал
                    newMaterials[i] = new Material(renderer.materials[i]);

                    // Устанавливаем полупрозрачный цвет
                    newMaterials[i] = transparentRed;

                    // Изменяем Surface Type на Transparent, если свойство существует
                    if (newMaterials[i].HasProperty("_Surface"))
                    {
                        newMaterials[i].SetFloat("_Surface", 1.0f); // 1.0f для Transparent
                        newMaterials[i].SetFloat("_Blend", 1.0f); // Убедитесь, что используется альфа-смешивание
                        newMaterials[i].SetInt("_ZWrite", 0); // Выключаем запись в Z
                        newMaterials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent; // Установите на Transparent
                    }
                }

                // Подменяем материалы на новом массиве
                renderer.materials = newMaterials;
            }
        }
    }

    
    // Метод для возврата оригинальных материалов
    public void RestoreOriginalMaterials()
    {
        photonView.RPC("OriginalMaterials", RpcTarget.AllBuffered);
    }

    [PunRPC] void OriginalMaterials()
    {
        foreach (var kvp in originalMaterials)
        {
            // Восстанавливаем оригинальные материалы
            kvp.Key.materials = kvp.Value;
        }

        // Очищаем словарь после восстановления
        originalMaterials.Clear();
    }
}