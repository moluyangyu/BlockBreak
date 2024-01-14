using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // 获取左右输入
        float horizontalInput = Input.GetAxis("Horizontal");

        // 移动球体
        MoveBall(horizontalInput);
    }

    void MoveBall(float horizontalInput)
    {
        // 计算球体的水平移动量
        float moveAmount = horizontalInput * moveSpeed * Time.deltaTime;

        // 获取球体当前位置
        Vector3 currentPosition = transform.position;

        // 计算新的位置
        Vector3 newPosition = new Vector3(currentPosition.x + moveAmount, currentPosition.y, currentPosition.z);

        // 将球体移动到新位置
        transform.position = newPosition;
    }
}
