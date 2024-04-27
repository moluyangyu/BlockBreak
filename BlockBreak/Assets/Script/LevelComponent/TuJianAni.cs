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
        // 注册场景加载完成时的事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 移除场景加载完成时的事件，以避免重复注册
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载完成时调用的方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
}
