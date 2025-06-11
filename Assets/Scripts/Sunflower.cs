
using UnityEngine;

public class Sunflower : Plant
{
    // 生产阳光的持续时间
    public float produceDuration = 5;
    private float produceTimer = 0; // 计时器
    private Animator anim; // 动画控制器
    public GameObject sunPrefab; // 阳光预制体

    // 跳跃的最小和最大距离
    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 2;

    private void Awake()
    {
        // 获取动画组件
        anim = GetComponent<Animator>();
    }

    protected override void EnableUpdate()
    {
        // 更新计时器
        produceTimer += Time.deltaTime;

        // 如果计时器超过生产持续时间，触发发光动画
        if (produceTimer > produceDuration)
        {
            produceTimer = 0; // 重置计时器
            anim.SetTrigger("IsGlowing"); // 触发发光动画
        }
    }

    // 生产阳光的方法
    public void ProduceSun()
    {
        // 实例化阳光预制体
        GameObject go = GameObject.Instantiate(sunPrefab, transform.position, Quaternion.identity);

        // 随机生成跳跃距离
        float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
        distance = Random.Range(0, 2) < 1 ? -distance : distance; // 随机选择方向
        Vector3 positon = transform.position; // 获取当前植物位置
        positon.x += distance; // 更新阳光位置

        // 让阳光跳跃到指定位置
        go.GetComponent<Sun>().JumpTo(positon);
    }
}
