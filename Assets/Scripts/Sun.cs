using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 定义 Sun 类，继承自 MonoBehaviour
public class Sun : MonoBehaviour
{
    // 移动持续时间
    public float moveDuration = 1;
    // 积分值
    public int point = 50;

    // 线性移动到目标位置的方法
    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration); // 使用 DOTween 进行移动
    }

    // 跳跃移动到目标位置的方法
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1; // 设置 z 轴位置
        Vector3 centerPos = (transform.position + targetPos) / 2; // 计算中心位置
        float distance = Vector3.Distance(transform.position, targetPos); // 计算距离

        centerPos.y += (distance / 2); // 提升中心位置的 y 值

        // 使用 DOTween 创建路径动画
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    // 鼠标点击事件
    void OnMouseDown()
    {
        // 移动到太阳点文本位置并在完成后执行回调
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(
            () =>
            {
                Destroy(this.gameObject); // 销毁当前游戏对象
                SunManager.Instance.AddSun(point); // 增加积分
            }
            );
    }
}
