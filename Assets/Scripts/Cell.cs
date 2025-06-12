using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;

    private void OnMouseDown()
    {
        // ֪ͨ HandManager �õ�Ԫ�񱻵��
        HandManager.Instance.OnCellClick(this);
    }

    // �������ֲ�ﵽ�õ�Ԫ��
    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false;

        currentPlant = plant;
        // ��ֲ���λ������Ϊ��Ԫ���λ��
        currentPlant.transform.position = transform.position;
        plant.TransitionToEnable();
        return true;
    }
}
