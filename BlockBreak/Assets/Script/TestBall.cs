using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // ��ȡ��������
        float horizontalInput = Input.GetAxis("Horizontal");

        // �ƶ�����
        MoveBall(horizontalInput);
    }

    void MoveBall(float horizontalInput)
    {
        // ���������ˮƽ�ƶ���
        float moveAmount = horizontalInput * moveSpeed * Time.deltaTime;

        // ��ȡ���嵱ǰλ��
        Vector3 currentPosition = transform.position;

        // �����µ�λ��
        Vector3 newPosition = new Vector3(currentPosition.x + moveAmount, currentPosition.y, currentPosition.z);

        // �������ƶ�����λ��
        transform.position = newPosition;
    }
}
