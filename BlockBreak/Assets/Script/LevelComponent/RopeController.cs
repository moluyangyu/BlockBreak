using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [Header("挂载点1")]
    public Transform attachmentPoint1;
    [Header("挂载点2")]
    public Transform attachmentPoint2;
    [Header("长度")]
    public float maxLength = 5f;
    [Header("材质")]
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
        // 设置绳子宽度
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // 添加纹理，在这里设置
        // lineRenderer.material.mainTexture = yourTexture;
        // 设置绳子两端的附着点位置
        Vector3[] ropePositions = new Vector3[2];
        ropePositions[0] = attachmentPoint1.position;
        ropePositions[1] = attachmentPoint2.position;
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
