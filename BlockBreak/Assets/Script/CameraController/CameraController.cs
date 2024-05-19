using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public float offset_min;
    public float offset_max;
    public float x_min;
    public float x_max;
    static GameObject SceneBlacking;
    public float openSpeed;
    public Image Image0;
    public Image Image1;
    public string idName;
    private void Awake()
    {
        player = GameObject.Find("Player");
        SceneBlacking=GameObject.Find("�䰵��Ч");
        SceneBlacking.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - player.transform.position;

        StartCoroutine(open());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, player.transform.position.x + offset_min, player.transform.position.x + offset_max), transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x_min, x_max), player.transform.position.y+7.0f, transform.position.z);
    }
    /// <summary>
    /// �ó����䰵���
    /// </summary>
    /// <param name="a"></param>��������״̬�ı���
    public  void SceneBlack(bool a)
    {
        if (a)
        {
            SceneBlacking.SetActive(true);
        }
        else
        {
            SceneBlacking.SetActive(false);
        }
    }

    private IEnumerator open()
    {
        print(Image0.rectTransform.position.y);
        while (Image0.rectTransform.position.y < 620)
        {
            Image0.rectTransform.Translate(0, openSpeed, 0);
            Image1.rectTransform.Translate(0, -openSpeed, 0);
            yield return null;
        }
        while (Image0.rectTransform.position.y > 470)
        {
            Image0.rectTransform.Translate(0, -openSpeed, 0);
            Image1.rectTransform.Translate(0, openSpeed, 0);
            yield return null;
        }
        while (Image0.rectTransform.position.y < 1000)
        {
            Image0.rectTransform.Translate(0, openSpeed, 0);
            Image1.rectTransform.Translate(0, -openSpeed, 0);
            yield return null;
        }
        UiStatic.TalkKickIssue(idName);
    }

}
