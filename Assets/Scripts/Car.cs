using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // 汽车的移动速度
    public float speed = 3;

    // 关联的汽车游戏对象
    public GameObject car;

    // Start 是 Unity 的生命周期方法，在游戏开始时调用一次
    void Start()
    {
        // 此处可以初始化相关逻辑
    }

    // Update 是 Unity 的生命周期方法，每帧调用一次
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.right);
       // Translate 的作用是移动物体，这里通过速度和 Time.deltaTime 实现平滑移动
    }


}