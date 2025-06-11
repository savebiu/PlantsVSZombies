using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalapenoAttack : MonoBehaviour
{
    private int atkValue; // 攻击力
    private Animator animator;

    private void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        // 播放攻击动画
        if (animator != null)
        {
            animator.SetTrigger("JalapenoAttack"); // 假设动画触发器名为 "Explode"
        }

        // 在动画播放完成后销毁对象
        Destroy(gameObject, 1f); // 假设动画持续 1 秒
    }

    public void SetATKValue(int value)
    {
        atkValue = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测是否碰到僵尸
        if (collision.CompareTag("Zombie"))
        {
            // 对僵尸造成伤害
            Zombie zombie = collision.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(atkValue);
            }
        }
    }
}