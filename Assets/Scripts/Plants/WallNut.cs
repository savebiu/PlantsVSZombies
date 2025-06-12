using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallNut : Plant
{
    [Header("坚果墙属性")]
    public int maxHealth = 300;   // 坚果墙最大血量
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 让坚果墙受到伤害的方法
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

        // 这里可以做延迟销毁，动画播完再销毁
        Destroy(gameObject, 0.5f);
    }

    // 这里可以根据你的僵尸设计，做阻挡功能
    // 比如，僵尸碰到坚果墙时，让僵尸停止移动或减速
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // 让僵尸停止移动或播放攻击动画
            ZombieManager zombie = collision.gameObject.GetComponent<ZombieManager>();
           
        }
    }

}
