using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDexTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("��������")]
    public int distance;//��������
    [Header("�ؿ��ڵı��")]
    public int number;//�ؿ��ڵı��
    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Vector2 a = new Vector2(distance, distance);
        SetColliderSize(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�����ҽ���ͷ����Զ���ͼ�����ź�
        if(collision.gameObject.tag=="Player")
        {
            UiStatic.GameDexTriggerIssue(number);
          //  PlayerController.Instance.SwitchStop(0);
        }
    }
    /// <summary>
    /// ������ײ���С
    /// </summary>
    /// <param name="newSize"></param>
    void SetColliderSize(Vector2 newSize)
    {
        if (boxCollider != null)
        {
            // ���� BoxCollider2D �Ĵ�С
            boxCollider.size = newSize;
         //   Debug.Log("BoxCollider2D size set to: " + newSize);
        }
        else
        {
        //    Debug.LogError("BoxCollider2D component not found.");
        }
    }
}
