using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 定义僵尸状态的枚举
enum ZombieState
{
    Move,   // 移动状态
    Eat,    // 吃东西状态
    Die,    // 死亡状态
    Pause   // 暂停状态
}
public class Zombie : MonoBehaviour
{
    // 当前僵尸状态
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rgd; // 僵尸的刚体组件
    public float moveSpeed = 2; // 僵尸移动速度
    private Animator anim; // 僵尸的动画控制器

    public int atkValue = 30; // 攻击值
    public float atkDuration = 2; // 攻击持续时间
    private float atkTimer = 0; // 攻击计时器

    private Plant currentEatPlant; // 当前正在吃的植物

    public int HP = 100; // 僵尸的最大生命值
    public int currentHP; // 当前生命值
    public GameObject zombieHeadPrefab; // 僵尸头部预制体

    private bool haveHead = true; // 是否有头部

    // 游戏开始时调用
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>(); // 获取刚体组件
        anim = GetComponent<Animator>(); // 获取动画控制器
        currentHP = HP; // 初始化当前生命值
    }

    // 每帧调用
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate(); // 更新移动状态
                break;
            case ZombieState.Eat:
                EatUpdate(); // 更新吃东西状态
                break;
            case ZombieState.Die:
                break; // 死亡状态不需要更新
            default:
                break;
        }
    }

    // 移动更新
    void MoveUpdate()
    {
        rgd.MovePosition(rgd.position + Vector2.left * moveSpeed * Time.deltaTime); // 向左移动
    }

    // 吃东西更新
    void EatUpdate()
    {
        atkTimer += Time.deltaTime; // 增加攻击计时器
        if (atkTimer > atkDuration && currentEatPlant != null)
        {
            AudioManager.Instance.PlayClip(Config.eat); // 播放吃东西音效
            currentEatPlant.TakeDamage(atkValue); // 对植物造成伤害
            atkTimer = 0; // 重置攻击计时器
        }
    }

    // 碰撞进入触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", true); // 设置攻击动画
            TransitionToEat(); // 转换到吃东西状态
            currentEatPlant = collision.GetComponent<Plant>(); // 获取当前植物
        }

        else if(collision.tag == "car"){
            collision.GetComponent<Car>().speed = 10;
            //currentHp = -1;
            Dead();
            //Destroy(collision.gameObject,3f);
        }

        else if (collision.tag == "House")
        {
            GameManager.Instance.GameEndFail(); // 游戏失败
        }
    }

    // 碰撞退出触发器
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            anim.SetBool("IsAttacking", false); // 取消攻击动画
            zombieState = ZombieState.Move; // 转换回移动状态
            currentEatPlant = null; // 清空当前植物
        }
    }

    // 转换到吃东西状态
    void TransitionToEat()
    {
        zombieState = ZombieState.Eat; // 设置状态为吃东西
        atkTimer = 0; // 重置攻击计时器
    }

    // 转换到暂停状态
    public void TransitionToPause()
    {
        zombieState = ZombieState.Pause; // 设置状态为暂停
        anim.enabled = false; // 禁用动画
    }

    // 受到伤害
    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return; // 如果已经死亡，返回

        this.currentHP -= damage; // 减少当前生命值
        if (currentHP <= 0)
        {
            currentHP = -1; // 设置为-1表示死亡
            Dead(); // 调用死亡方法
        }
        float hpPercent = currentHP * 1f / HP; // 计算生命值百分比
        anim.SetFloat("HPPercent", hpPercent); // 更新动画参数
        if (hpPercent < .5f && haveHead)
        {
            haveHead = false; // 设置为没有头部
            GameObject go = GameObject.Instantiate(zombieHeadPrefab, transform.position, Quaternion.identity); // 实例化头部
            Destroy(go, 2); // 2秒后销毁头部
        }
    }

    // 死亡处理
    private void Dead()
    {
        if (zombieState == ZombieState.Die) return; // 如果已经死亡，返回

        zombieState = ZombieState.Die; // 设置状态为死亡
        GetComponent<Collider2D>().enabled = false; // 禁用碰撞体
        ZombieManager.Instance.RemoveZombie(this); // 从僵尸管理器中移除

        Destroy(this.gameObject, 2); // 2秒后销毁僵尸
    }
}
