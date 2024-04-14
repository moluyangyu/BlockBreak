using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public GameObject pick;
    public GameObject upstairs;
    public GameObject turn;
    public GameObject speedUp;
    public GameObject pause;
    public GameObject walk;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Sprite smallStar;
    public Sprite largeStar;

    private GameObject player;
    private PlayerController playerC;

    private GameObject nowState = null;
    private float t;
    private bool[] check = new bool[3] {false, false ,false};
    private void Awake()
    {
        player = GameObject.Find("Player");
        playerC = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        SetImage();
        t = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        t += Time.deltaTime;
        if (t > 1f)
        {
            SetImage();
            t -= 1;
        }

        slider.value = Mathf.InverseLerp(-30f, 140f, player.transform.position.x);
        if (!check[0] && slider.value >= 0.33)
        {
            check[0] = true;
            StartCoroutine(Star(star1));
        }
        if (!check[1] && slider.value >= 0.67)
        {
            check[1] = true;
            StartCoroutine(Star(star2));
        }
        if (!check[2] && slider.value >= 0.99)
        {
            check[2] = true;
            StartCoroutine(Star(star3));
        }
    }

    public void SetImage()
    {
        if(nowState)nowState.SetActive(false);
        if (playerC.isStair)
        {
            nowState = upstairs;
        }
        else if (playerC.stop)
        {
            nowState = pause;
        }
        else if (playerC.direction == -1)
        {
            nowState = turn;
        }
        else if (playerC.isFast)
        {
            nowState = speedUp;
        }
        else
        {
            nowState = walk;
        }
        nowState.SetActive(true);
    }
    
    private IEnumerator Star(GameObject star)
    {
        star.SetActive(true);
        while (star.transform.localScale.x < 1.5f)
        {
            star.transform.localScale = new Vector3(star.transform.localScale.x + 0.1f, star.transform.localScale.y + 0.1f, 1);
            yield return new WaitForSeconds(0.1f);
        }
        while (star.transform.localScale.x > 1f)
        {
            star.transform.localScale = new Vector3(star.transform.localScale.x - 0.1f, star.transform.localScale.y - 0.1f, 1);
            yield return new WaitForSeconds(0.1f);
        }
        star.GetComponent<Image>().sprite = smallStar;
    }

}
