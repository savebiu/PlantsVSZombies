using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 手部控制器:实现鼠标点击植物卡片跟随,点击地块时种植植物
 * 
 */

public class HandManager : MonoBehaviour
{
    // 单例模式，方便其他类访问
    public static HandManager Instance { get; private set; }

    // 植物预制体列表
    public List<Plant> plantPrefabList;

    // 当前手中持有的植物
    private Plant currentPlant;

    private void Awake()
    {
        // 初始化单例
        Instance = this;
    }

    private void Update()
    {
        // 每帧更新，跟随鼠标移动
        FollowCursor();
    }

    // 将植物添加到手上
    public bool AddPlant(PlantType plantType)
    {
        // 判断手上是否有植物，不为空则种植失败
        if (currentPlant != null) return false;

        // 获取植物预制体
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            print("要种植的植物不存在"); return false;
        }
        // 实例化当前需要种植的植物
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    // 根据植物类型获取植物预制体
    private Plant GetPlantPrefab(PlantType plantType)
    {
        // 遍历植物集合
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        // 若未找到对应的植物类型则返回null
        return null;
    }

    // 跟随鼠标移动
    void FollowCursor()
    {
        // 如果当前没有植物则返回
        if (currentPlant == null) return;

        // 将屏幕坐标转换为世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 将z轴设置为0使图像在一个平面上
        mouseWorldPosition.z = 0;
        // 将鼠标位置设置为植物位置
        currentPlant.transform.position = mouseWorldPosition;
    }

    // 处理地块点击事件
    public void OnCellClick(Cell cell)
    {
        // 如果当前没有植物则返回
        if (currentPlant == null) return;

        // 尝试将植物添加到地块
        bool isSuccess = cell.AddPlant(currentPlant);

        // 如果成功，则清空当前植物并播放音效
        if (isSuccess)
        {
            currentPlant = null;
            AudioManager.Instance.PlayClip(Config.plant);
        }
    }
}


/*
    public static HandManager Instance { get; private set; }

    public List<Plant> plantPrefabList;

    private Plant currentPlant;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowCursor();
    }

    //将植物添加到手上
    public bool AddPlant(PlantType plantType)
    {
        //判断手上是否有植物，不为空则种植失败
        if (currentPlant != null) return false;

        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            print("要种植的植物不存在"); return false;
        }
        //实例化当前需要种植的植物
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    private Plant GetPlantPrefab(PlantType plantType)
    {   
        //遍历植物集合
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        //若未找到对应的植物类型则还未制作
        return null;
    }

    //跟随鼠标移动
    void FollowCursor()
    {
        if (currentPlant == null) return;

        //ScreenToWorldPoint将屏幕坐标转换为世界坐标
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //将z轴设置为0使图像在一个平面上
        mouseWorldPosition.z = 0;
        //将鼠标位置设置为植物位置
        currentPlant.transform.position = mouseWorldPosition;

    }

   /* public void OnCellClick(Cell cell)
    {
        if (currentPlant == null) return;
        currentPlant.transform.position = cell.transform.position;
        *//*bool isSuccess = cell.AddPlant(currentPlant);*//*
        currentPlant = null;
       *//* if (isSuccess)
        {
            currentPlant = null;
            *//*AudioManager.Instance.PlayClip(Config.plant);*//*
        }*//*
    }
*/
