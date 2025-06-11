using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �ֲ�������:ʵ�������ֲ�￨Ƭ����,����ؿ�ʱ��ֲֲ��
 * 
 */

public class HandManager : MonoBehaviour
{
    // ����ģʽ���������������
    public static HandManager Instance { get; private set; }

    // ֲ��Ԥ�����б�
    public List<Plant> plantPrefabList;

    // ��ǰ���г��е�ֲ��
    private Plant currentPlant;

    private void Awake()
    {
        // ��ʼ������
        Instance = this;
    }

    private void Update()
    {
        // ÿ֡���£���������ƶ�
        FollowCursor();
    }

    // ��ֲ����ӵ�����
    public bool AddPlant(PlantType plantType)
    {
        // �ж������Ƿ���ֲ���Ϊ������ֲʧ��
        if (currentPlant != null) return false;

        // ��ȡֲ��Ԥ����
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            print("Ҫ��ֲ��ֲ�ﲻ����"); return false;
        }
        // ʵ������ǰ��Ҫ��ֲ��ֲ��
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    // ����ֲ�����ͻ�ȡֲ��Ԥ����
    private Plant GetPlantPrefab(PlantType plantType)
    {
        // ����ֲ�Ｏ��
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        // ��δ�ҵ���Ӧ��ֲ�������򷵻�null
        return null;
    }

    // ��������ƶ�
    void FollowCursor()
    {
        // �����ǰû��ֲ���򷵻�
        if (currentPlant == null) return;

        // ����Ļ����ת��Ϊ��������
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ��z������Ϊ0ʹͼ����һ��ƽ����
        mouseWorldPosition.z = 0;
        // �����λ������Ϊֲ��λ��
        currentPlant.transform.position = mouseWorldPosition;
    }

    // ����ؿ����¼�
    public void OnCellClick(Cell cell)
    {
        // �����ǰû��ֲ���򷵻�
        if (currentPlant == null) return;

        // ���Խ�ֲ����ӵ��ؿ�
        bool isSuccess = cell.AddPlant(currentPlant);

        // ����ɹ�������յ�ǰֲ�ﲢ������Ч
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

    //��ֲ����ӵ�����
    public bool AddPlant(PlantType plantType)
    {
        //�ж������Ƿ���ֲ���Ϊ������ֲʧ��
        if (currentPlant != null) return false;

        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab == null)
        {
            print("Ҫ��ֲ��ֲ�ﲻ����"); return false;
        }
        //ʵ������ǰ��Ҫ��ֲ��ֲ��
        currentPlant = GameObject.Instantiate(plantPrefab);
        return true;
    }

    private Plant GetPlantPrefab(PlantType plantType)
    {   
        //����ֲ�Ｏ��
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType == plantType)
            {
                return plant;
            }
        }
        //��δ�ҵ���Ӧ��ֲ��������δ����
        return null;
    }

    //��������ƶ�
    void FollowCursor()
    {
        if (currentPlant == null) return;

        //ScreenToWorldPoint����Ļ����ת��Ϊ��������
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //��z������Ϊ0ʹͼ����һ��ƽ����
        mouseWorldPosition.z = 0;
        //�����λ������Ϊֲ��λ��
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
