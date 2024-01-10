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
    public float maxLength = 5f;
    [Header("����")]
    public Material ropeMaterial;
    private SpringJoint2D springJoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = ropeMaterial;
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.distance = maxLength;
        UpdateRopePosition();
    }

    void Update()
    {
        UpdateRopePosition();
    }

    void UpdateRopePosition()
    {
        // �������ӿ��
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // �����������������
        // lineRenderer.material.mainTexture = yourTexture;
        // �����������˵ĸ��ŵ�λ��
        Vector3[] ropePositions = new Vector3[2];
        ropePositions[0] = attachmentPoint1.position;
        ropePositions[1] = attachmentPoint2.position;
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
