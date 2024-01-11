using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [Header("挂载点1")]
    public Transform attachmentPoint1;
    [Header("挂载点2")]
    public Transform attachmentPoint2;
    [Header("材质")]
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
    /// 剪断绳子
    /// </summary>
    public void Cut()
    {
        briageLong -= 1;
    }
    /// <summary>
    /// 动态更新绳子
    /// </summary>
    void UpdateRopePosition()
    {
        // 设置绳子宽度
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // 添加纹理，在这里设置
        // lineRenderer.material.mainTexture = yourTexture;
        // 设置绳子两端的附着点位置
        Vector3[] ropePositions = new Vector3[briageLong];

        for(int i=0;i<briageLong;i++)
        {
            ropePositions[i] = briages[i].position;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
