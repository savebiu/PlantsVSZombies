using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;

    private void OnMouseDown()
    {
        // 通知 HandManager 该单元格被点击
        HandManager.Instance.OnCellClick(this);
    }

    // 尝试添加植物到该单元格
    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false;

        currentPlant = plant;
        // 将植物的位置设置为单元格的位置
        currentPlant.transform.position = transform.position;
        plant.TransitionToEnable();
        return true;
    }
}
