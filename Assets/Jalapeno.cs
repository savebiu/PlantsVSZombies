using System.Collections.Generic;
using UnityEngine;

public class Jalapeno : Plant
{
    public float attackDelay = 1f; // 攻击延迟时间
    public JalapenoAttack jalapenoAttackPrefab; // Jalapeno 攻击预制体
    public int atkValue = 100; // 攻击力

    private Animator animator;

    private void Start()
    {
        // 获取 Animator 组件
        animator = GetComponent<Animator>();

        // 播放 Jalapeno 的动画
        if (animator != null)
        {
            animator.SetTrigger("Activate"); // 假设动画触发器名为 "Activate"
        }

        // 在延迟后调用攻击方法
        Invoke(nameof(Attack), attackDelay);
    }

    void Attack()
    {
        // 生成攻击预制体
        if (jalapenoAttackPrefab != null)
        {
            JalapenoAttack attack = Instantiate(jalapenoAttackPrefab, transform.position, Quaternion.identity);
            attack.SetATKValue(atkValue); // 设置攻击力
        }

        // 销毁当前 Jalapeno 对象
        Destroy(gameObject);
    }
}