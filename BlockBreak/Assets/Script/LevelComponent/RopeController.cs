using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [Header("���ص�1")]
    public Transform attachmentPoint1;
    [Header("���ص�2")]
    public Transform attachmentPoint2;
    [Header("����")]
    public Material ropeMaterial;
    public int briageLong;
    private LineRenderer lineRenderer;
    public Transform[] briages;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = ropeMaterial;
        UpdateRopePosition();
    }

    void Update()
    {
        UpdateRopePosition();
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void Cut()
    {
        briageLong -= 1;
    }
    /// <summary>
    /// ��̬��������
    /// </summary>
    void UpdateRopePosition()
    {
        // �������ӿ��
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // �����������������
        // lineRenderer.material.mainTexture = yourTexture;
        // �����������˵ĸ��ŵ�λ��
        Vector3[] ropePositions = new Vector3[briageLong];

        for(int i=0;i<briageLong;i++)
        {
            ropePositions[i] = briages[i].position;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
