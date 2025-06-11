using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 管理太阳点的类
public class SunManager : MonoBehaviour
{
    // 单例模式
    public static SunManager Instance { get; private set; }

    // 太阳点数
    [SerializeField]
    private int sunPoint;
    public int SunPoint
    {
        get { return sunPoint; }
    }

    // 显示太阳点的文本
    public TextMeshProUGUI sunPointText;
    private Vector3 sunPointTextPosition; // 太阳点文本的位置
    public float produceTime; // 生产太阳的时间间隔
    private float produceTimer; // 计时器
    public GameObject sunPrefab; // 太阳预制体

    private bool isStartProduce = false; // 是否开始生产太阳

    private void Awake()
    {
        Instance = this; // 初始化单例
    }

    private void Start()
    {
        UpdateSunPointText(); // 更新太阳点文本
        CalcSunPointTextPosition(); // 计算太阳点文本的位置
        //StartProduce(); // 可选：开始生产太阳
    }

    private void Update()
    {
        if (isStartProduce) // 如果开始生产
        {
            ProduceSun(); // 生产太阳
        }
    }

    // 开始生产太阳
    public void StartProduce()
    {
        isStartProduce = true;
    }

    // 停止生产太阳
    public void StopProduce()
    {
        isStartProduce = false;
    }

    // 更新太阳点文本显示
    private void UpdateSunPointText()
    {
        sunPointText.text = SunPoint.ToString();
    }

    // 减少太阳点
    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText(); // 更新文本
    }

    // 增加太阳点
    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText(); // 更新文本
    }

    // 获取太阳点文本的位置
    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    // 计算太阳点文本的位置
    private void CalcSunPointTextPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0; // 设置z轴为0
        sunPointTextPosition = position; // 保存位置
    }

    // 生产太阳
    void ProduceSun()
    {
        produceTimer += Time.deltaTime; // 增加计时器
        if (produceTimer > produceTime) // 如果计时器超过生产时间
        {
            produceTimer = 0; // 重置计时器
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1); // 随机生成太阳位置
            GameObject go = GameObject.Instantiate(sunPrefab, position, Quaternion.identity); // 实例化太阳

            position.y = Random.Range(-4, 3f); // 随机生成目标位置
            go.GetComponent<Sun>().LinearTo(position); // 太阳移动到目标位置
        }
    }
}
