using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public int nowSerialNumber;//��ǰ�����ĺ���
    public List<List<GameObject>> activityGroundGroup;//�ݳ�����ķ���
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�ƶ���ʼ���¼�
    public delegate void MoveStartHandler(int i);
    public event MoveStartHandler MoveStart;
    /// <summary>
    /// �ƶ���ʼ
    /// </summary>
    public void MoveStartIssue()
    {
        
        MoveStart?.Invoke(nowSerialNumber);
        nowSerialNumber++;
    }
    //�����ʵ����¼�
    public delegate void TriggerEggHandler(string finishName);
    public event TriggerEggHandler TriggerEgg;
    /// <summary>
    /// �ʵ�����
    /// </summary>
    /// <param name="finishName"></param>�����Ĳʵ�����
    public void TriggerEggIssue(string finishName)
    {
        TriggerEgg?.Invoke(finishName);
    }
    /// <summary>
    /// �����������������¿�����
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround1(GameObject a)
    {
        activityGroundGroup.Add(new List<GameObject>());//�¿����ξ�������
        int i = activityGroundGroup.Count;
        activityGroundGroup[i].Add(a);
        EditorUtility.SetDirty(this);
        return activityGroundGroup.Count;
    }
    /// <summary>
    /// ����������������ͬһ����
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround2(GameObject a)
    {
        int i = activityGroundGroup.Count;
        activityGroundGroup[i].Add(a);
        EditorUtility.SetDirty(this);
        return activityGroundGroup.Count;
    }
    /// <summary>
    /// ����Ŀǰ����������ˢ���������˳��
    /// </summary>
    public void UpdateGround()
    {
        int i = 0;
        foreach(List<GameObject> a in activityGroundGroup)
        {
            i++;
            foreach(GameObject b in a)//����Ƕ��һ��,b�ǻ���
            {
                b.GetComponent<ActivityGround>().serialNumber = i;
            }
        }
    }
}
