using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[System.Serializable]
public class SerializableListList
{
    public List<List<GameObject>> a;
}

public class GroundEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public int nowStepNumber;//��ǰ������̨�׺���
    public int nowBollardNumber;//��ǰ�������������ĺ���
    public SerializableListList activityGroundGroup;//�ݳ�����ķ���
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestIssue()
    {
        //�����÷���
        MoveStartIssue(FloorClass.bollard);
    }
    //�ƶ���ʼ���¼�
    public delegate void MoveStartHandler(int i, FloorClass a);
    public event MoveStartHandler MoveStart;
    /// <summary>
    /// �ƶ���ʼ
    /// </summary>
    public void MoveStartIssue(FloorClass a)
    {
        switch(a)
        {
            case FloorClass.step: MoveStart?.Invoke(nowStepNumber,a); nowStepNumber++; break;
            case FloorClass.bollard: MoveStart?.Invoke(nowBollardNumber,a);nowBollardNumber++; break;
        }
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
    /// ��ʼ��
    /// </summary>
    public void StartGroup()
    {
        Debug.Log("��ʼ��");
        activityGroundGroup = new();
    }
    /// <summary>
    /// �����������������¿�����
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround1(GameObject a)
    {
        
        activityGroundGroup?.a.Add(new List<GameObject>());//�¿����ξ�������
        int i = activityGroundGroup.a.Count;
        activityGroundGroup.a[i].Add(a);
      //  EditorUtility.SetDirty(this);
        return activityGroundGroup.a.Count;
    }
    /// <summary>
    /// ����������������ͬһ����
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public int AddGround2(GameObject a)
    {
        int i = activityGroundGroup.a.Count;
        activityGroundGroup.a[i]?.Add(a);
      //  EditorUtility.SetDirty(this);
        return activityGroundGroup.a.Count;
    }
    /// <summary>
    /// ����Ŀǰ����������ˢ���������˳��
    /// </summary>
    public void UpdateGround()
    {
        int i = 0;
        foreach(List<GameObject> a in activityGroundGroup.a)
        {
            i++;
            foreach(GameObject b in a)//����Ƕ��һ��,b�ǻ���
            {
                b.GetComponent<ActivityGround>().serialNumber = i;
            }
        }
    }
}
