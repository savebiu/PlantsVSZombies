using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ����̫�������
public class SunManager : MonoBehaviour
{
    // ����ģʽ
    public static SunManager Instance { get; private set; }

    // ̫������
    [SerializeField]
    private int sunPoint;
    public int SunPoint
    {
        get { return sunPoint; }
    }

    // ��ʾ̫������ı�
    public TextMeshProUGUI sunPointText;
    private Vector3 sunPointTextPosition; // ̫�����ı���λ��
    public float produceTime; // ����̫����ʱ����
    private float produceTimer; // ��ʱ��
    public GameObject sunPrefab; // ̫��Ԥ����

    private bool isStartProduce = false; // �Ƿ�ʼ����̫��

    private void Awake()
    {
        Instance = this; // ��ʼ������
    }

    private void Start()
    {
        UpdateSunPointText(); // ����̫�����ı�
        CalcSunPointTextPosition(); // ����̫�����ı���λ��
        //StartProduce(); // ��ѡ����ʼ����̫��
    }

    private void Update()
    {
        if (isStartProduce) // �����ʼ����
        {
            ProduceSun(); // ����̫��
        }
    }

    // ��ʼ����̫��
    public void StartProduce()
    {
        isStartProduce = true;
    }

    // ֹͣ����̫��
    public void StopProduce()
    {
        isStartProduce = false;
    }

    // ����̫�����ı���ʾ
    private void UpdateSunPointText()
    {
        sunPointText.text = SunPoint.ToString();
    }

    // ����̫����
    public void SubSun(int point)
    {
        sunPoint -= point;
        UpdateSunPointText(); // �����ı�
    }

    // ����̫����
    public void AddSun(int point)
    {
        sunPoint += point;
        UpdateSunPointText(); // �����ı�
    }

    // ��ȡ̫�����ı���λ��
    public Vector3 GetSunPointTextPosition()
    {
        return sunPointTextPosition;
    }

    // ����̫�����ı���λ��
    private void CalcSunPointTextPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(sunPointText.transform.position);
        position.z = 0; // ����z��Ϊ0
        sunPointTextPosition = position; // ����λ��
    }

    // ����̫��
    void ProduceSun()
    {
        produceTimer += Time.deltaTime; // ���Ӽ�ʱ��
        if (produceTimer > produceTime) // �����ʱ����������ʱ��
        {
            produceTimer = 0; // ���ü�ʱ��
            Vector3 position = new Vector3(Random.Range(-5, 6.5f), 6.2f, -1); // �������̫��λ��
            GameObject go = GameObject.Instantiate(sunPrefab, position, Quaternion.identity); // ʵ����̫��

            position.y = Random.Range(-4, 3f); // �������Ŀ��λ��
            go.GetComponent<Sun>().LinearTo(position); // ̫���ƶ���Ŀ��λ��
        }
    }
}
