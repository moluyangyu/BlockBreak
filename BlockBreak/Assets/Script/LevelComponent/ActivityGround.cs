using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
public enum FloorClass
{
    step,
    bollard
}

public class ActivityGround : MonoBehaviour
{
    [Tooltip("����ͼƬ")]
    public Sprite sprite;
    public FloorClass floorClass;
    // public Direction direction;
    [Tooltip("�������0��������1�Ļᱻ�޸�Ϊ3")]
    public float moveSpeed;
    public float distance;
    [Tooltip("��0��ʼ����")]
    public int serialNumber;
    [Tooltip("��λΪ��")]
    public float delayTime;
    public int[] a;
    [Header("-----�ָ���-----")]
    public bool isPlay;
    public Vector3 startPoint;//�����ƶ��õ�������λ��
    public Vector3 endPoint;
    public bool canCross;//�Ƿ����ͨ��
    public GameObject groundEvent;//�ذ��¼�
    public float t;//��ֵ����
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        groundEvent = GameObject.Find("GroundEvent");
        groundEvent.GetComponent<GroundEvent>().MoveStart += MoveStart;
        moveSpeed = (moveSpeed <= 0) ? 3 : moveSpeed;//���С��1�͵���3
        moveSpeed /= (distance*100);//���ٶȹ�һ�����ڼ���
    }
    // Update is called once per frame
    void Update()
    {
 
    }
    /// <summary>
    /// �����ƶ��¼�������Ԥ��ֵ�����Ŀ�ĵ�Ȼ�������ƶ��ĺ������ƶ�һ�κ���Ŀ�ͨ��״̬Ȼ��ȡ������
    /// </summary>
    /// <param name="i"></param>�����ƶ���ţ���֤�Ƿ�Ӧ���ƶ�
    public void MoveStart(int i, FloorClass a)
    {
        if(i==serialNumber&&a==floorClass)
        {
            Debug.Log("�ƶ���ʼ��" + serialNumber + "��"+"���ͣ�"+floorClass);
            startPoint = this.gameObject.transform.position;
            endPoint = this.gameObject.transform.position;//���㵥���ƶ���Ҫ������λ��
            float radiaAngle = Mathf.Deg2Rad * direction;
            endPoint += new Vector3(distance * Mathf.Cos(radiaAngle), distance*Mathf.Sin(radiaAngle), 0f);
            //switch (direction)
            //{
            //    case Direction.Up: endPoint += new Vector3(0f * distance, 1.0f * distance, 0f * distance); break;
            //    case Direction.Down: endPoint += new Vector3(0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.Left: endPoint += new Vector3(-1.0f * distance, 0f * distance, 0f * distance); break;
            //    case Direction.Right: endPoint += new Vector3(1.0f * distance, 0f * distance, 0f * distance); break;
            //    case Direction.DownLeft: endPoint += new Vector3(-1.0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.DownRight: endPoint += new Vector3(1.0f * distance, -1.0f * distance, 0f * distance); break;
            //    case Direction.UpLeft: endPoint += new Vector3(-1.0f * distance, 1.0f * distance, 0f * distance); break;
            //    case Direction.UpRight: endPoint += new Vector3(1.0f * distance, 1.0f * distance, 0f * distance); break;
            //}
            InvokeRepeating(nameof(Move), delayTime, 0.01f);
            groundEvent.GetComponent<GroundEvent>().MoveStart -= MoveStart; //�������ȡ������
            canCross = true;//�����ƶ�״̬
        }
    }
    /// <summary>
    /// ʵ���ƶ��ĺ�����ͨ����ֵƽ���ƶ����ִ�Ŀ��λ���Ժ��������
    /// </summary>
    public void Move()
    {
        if (this.gameObject.transform.position == endPoint)
        {
            CancelInvoke(nameof(Move));//ȡ������
        }
        t = (t > 1) ? 1 : t + moveSpeed;
        this.gameObject.transform.position = Vector3.Lerp(startPoint, endPoint, t);
    }
    /// <summary>
    /// ��������������,�¿�����
    /// </summary>
    /// <param name="a"></param>
    public void AddGround1()
    {
        if(!isPlay)
        {
            isPlay = true;
            groundEvent = GameObject.Find("GroundEvent");
            serialNumber = groundEvent.GetComponent<GroundEvent>().AddGround1(this.gameObject);
        }
    }
    public void AddGround2()
    {
        if(!isPlay)
        {
            isPlay = true;
            groundEvent = GameObject.Find("GroundEvent");
            serialNumber = groundEvent.GetComponent<GroundEvent>().AddGround2(this.gameObject);
        }
    }
}
