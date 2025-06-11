using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���彩ʬ״̬��ö��
enum ZombieState
{
    Move,   // �ƶ�״̬
    Eat,    // �Զ���״̬
    Die,    // ����״̬
    Pause   // ��ͣ״̬
}
public class Zombie : MonoBehaviour
{
    // ��ǰ��ʬ״̬
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rgd; // ��ʬ�ĸ������
    public float moveSpeed = 2; // ��ʬ�ƶ��ٶ�
    private Animator anim; // ��ʬ�Ķ���������

    public int atkValue = 30; // ����ֵ
    public float atkDuration = 2; // ��������ʱ��
    private float atkTimer = 0; // ������ʱ��

    private Plant currentEatPlant; // ��ǰ���ڳԵ�ֲ��

    public int HP = 100; // ��ʬ���������ֵ
    public int currentHP; // ��ǰ����ֵ
    public GameObject zombieHeadPrefab; // ��ʬͷ��Ԥ����

    private bool haveHead = true; // �Ƿ���ͷ��

    // ��Ϸ��ʼʱ����
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>(); // ��ȡ�������
        anim = GetComponent<Animator>(); // ��ȡ����������
        currentHP = HP; // ��ʼ����ǰ����ֵ
    }

    // ÿ֡����
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate(); // �����ƶ�״̬
                break;
            case ZombieState.Eat:
                EatUpdate(); // ���³Զ���״̬
                break;
            case ZombieState.Die:
                break; // ����״̬����Ҫ����
            default:
                break;
        }
    }

    // �ƶ�����
    void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime); // �����ƶ�
    }

    // �Զ�������
    void EatUpdate()
    {
        atkTimer += Time.deltaTime; // ���ӹ�����ʱ��
        if (atkTimer > atkDuration && currentEatPlant != null)
        {
            AudioManager.Instance.PlayClip(Config.eat); // ���ųԶ�����Ч
            currentEatPlant.TakeDamage(atkValue); // ��ֲ������˺�
            atkTimer = 0; // ���ù�����ʱ��
        }
    }

    // ��ײ���봥����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", true); // ���ù�������
            TransitionToEat(); // ת�����Զ���״̬
            currentEatPlant = collision.GetComponent<Plant>(); // ��ȡ��ǰֲ��
        }

        else if(collision.tag == "car"){
            collision.GetComponent<Car>().speed = 10;
            //currentHp = -1;
            Dead();
            //Destroy(collision.gameObject,3f);
        }

        else if (collision.tag == "House")
        {
            GameManager.Instance.GameEndFail(); // ��Ϸʧ��
        }
    }

    // ��ײ�˳�������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", false); // ȡ����������
            zombieState = ZombieState.Move; // ת�����ƶ�״̬
            currentEatPlant = null; // ��յ�ǰֲ��
        }
    }

    // ת�����Զ���״̬
    void TransitionToEat()
    {
        zombieState = ZombieState.Eat; // ����״̬Ϊ�Զ���
        atkTimer = 0; // ���ù�����ʱ��
    }

    // ת������ͣ״̬
    public void TransitionToPause()
    {
        zombieState = ZombieState.Pause; // ����״̬Ϊ��ͣ
        anim.enabled = false; // ���ö���
    }

    // �ܵ��˺�
    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return; // ����Ѿ�����������

        this.currentHP -= damage; // ���ٵ�ǰ����ֵ
        if (currentHP <= 0)
        {
            currentHP = -1; // ����Ϊ-1��ʾ����
            Dead(); // ������������
        }
        float hpPercent = currentHP * 1f / HP; // ��������ֵ�ٷֱ�
        anim.SetFloat("HPPercent", hpPercent); // ���¶�������
        if (hpPercent < .5f && haveHead)
        {
            haveHead = false; // ����Ϊû��ͷ��
            GameObject go = GameObject.Instantiate(zombieHeadPrefab, transform.position, Quaternion.identity); // ʵ����ͷ��
            Destroy(go, 2); // 2�������ͷ��
        }
    }

    // ��������
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return; // ����Ѿ�����������

        zombieState = ZombieState.Die; // ����״̬Ϊ����
        GetComponent<Collider2D>().enabled = false; // ������ײ��
        ZombieManager.Instance.RemoveZombie(this); // �ӽ�ʬ���������Ƴ�

        Destroy(this.gameObject, 2); // 2������ٽ�ʬ
    }
}
