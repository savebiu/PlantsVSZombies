using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallNut : Plant
{
    [Header("���ǽ����")]
    public int maxHealth = 300;   // ���ǽ���Ѫ��
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // �ü��ǽ�ܵ��˺��ķ���
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
       
    }

    private void Die()
    {

        // ����������ӳ����٣���������������
        Destroy(gameObject, 0.5f);
    }

    // ������Ը�����Ľ�ʬ��ƣ����赲����
    // ���磬��ʬ�������ǽʱ���ý�ʬֹͣ�ƶ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // �ý�ʬֹͣ�ƶ��򲥷Ź�������
            ZombieManager zombie = collision.gameObject.GetComponent<ZombieManager>();
           
        }
    }

}
