using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class ChangeMaterials : MonoBehaviourPunCallbacks
{
    // ������� ��� ���������� ������������ ���������� (������� ������� ����������)
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // �������������� �����
    [SerializeField] Material transparentBlue;
    [SerializeField] Material transparentRed;

    // ����� ��� ���������� ������������ ���������� � �� ��������� �� ���������� �����
    
    public void ChangeToTransparentBlue()
    {
        photonView.RPC("TransparentBlue", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TransparentBlue()
    {
        // �������� ��� ���������� Renderer � �������� ��������
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        // ��������� ������������ ��������� � ��������� ����� ����
        foreach (var renderer in renderers)
        {
            // ���������, ���� �� � ������� ���������
            if (renderer.materials.Length > 0)
            {
                // ��������� ������������ ���������
                originalMaterials[renderer] = renderer.materials;

                // ������� ����� ������ ���������� �� ������ ������������
                Material[] newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    // ��������� ������������ ��������
                    newMaterials[i] = new Material(renderer.materials[i]);

                    // ������������� �������������� ����
                    newMaterials[i] = transparentBlue;

                    // �������� Surface Type �� Transparent, ���� �������� ����������
                    if (newMaterials[i].HasProperty("_Surface"))
                    {
                        newMaterials[i].SetFloat("_Surface", 1.0f); // 1.0f ��� Transparent
                        newMaterials[i].SetFloat("_Blend", 1.0f); // ���������, ��� ������������ �����-����������
                        newMaterials[i].SetInt("_ZWrite", 0); // ��������� ������ � Z
                        newMaterials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent; // ���������� �� Transparent
                    }
                }

                // ��������� ��������� �� ����� �������
                renderer.materials = newMaterials;
            }
        }
    }

    // ����� ��� ���������� ������������ ���������� � �� ��������� �� ���������� �������
    public void ChangeToTransparentRed()
    {
        photonView.RPC("TransparentRed", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void TransparentRed()
    {
        // �������� ��� ���������� Renderer � �������� ��������
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        // ��������� ������������ ��������� � ��������� ����� ����
        foreach (var renderer in renderers)
        {
            // ���������, ���� �� � ������� ���������
            if (renderer.materials.Length > 0)
            {
                // ��������� ������������ ���������
                originalMaterials[renderer] = renderer.materials;

                // ������� ����� ������ ���������� �� ������ ������������
                Material[] newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    // ��������� ������������ ��������
                    newMaterials[i] = new Material(renderer.materials[i]);

                    // ������������� �������������� ����
                    newMaterials[i] = transparentRed;

                    // �������� Surface Type �� Transparent, ���� �������� ����������
                    if (newMaterials[i].HasProperty("_Surface"))
                    {
                        newMaterials[i].SetFloat("_Surface", 1.0f); // 1.0f ��� Transparent
                        newMaterials[i].SetFloat("_Blend", 1.0f); // ���������, ��� ������������ �����-����������
                        newMaterials[i].SetInt("_ZWrite", 0); // ��������� ������ � Z
                        newMaterials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent; // ���������� �� Transparent
                    }
                }

                // ��������� ��������� �� ����� �������
                renderer.materials = newMaterials;
            }
        }
    }

    
    // ����� ��� �������� ������������ ����������
    public void RestoreOriginalMaterials()
    {
        photonView.RPC("OriginalMaterials", RpcTarget.AllBuffered);
    }

    [PunRPC] void OriginalMaterials()
    {
        foreach (var kvp in originalMaterials)
        {
            // ��������������� ������������ ���������
            kvp.Key.materials = kvp.Value;
        }

        // ������� ������� ����� ��������������
        originalMaterials.Clear();
    }
}