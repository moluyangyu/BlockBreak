using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TuJianAni : MonoBehaviour
{
    private DragonBones.UnityArmatureComponent animDB;
    // Start is called before the first frame update
    void Start()
    {
        animDB = GetComponent<DragonBones.UnityArmatureComponent>();
        if(animDB!=null)
        {
            animDB.animation.Play("start",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        // ע�᳡���������ʱ���¼�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �Ƴ������������ʱ���¼����Ա����ظ�ע��
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �����������ʱ���õķ���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
