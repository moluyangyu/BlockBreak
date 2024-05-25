using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class WUIStarVideoPanel : MonoBehaviour
{
    public VideoPlayer mVideoPlayer;
    public Button mBtn_Skip;
    public GameObject skip;

    public WUIStarVideoPanel()
    {
        // ���Raw Image�Ĳ���֡

        
        if (mVideoPlayer!=null) mVideoPlayer.targetTexture.Release();
        // ������Ƶ���Ž���
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached += EndReached;
        if (mBtn_Skip != null) mBtn_Skip.onClick.AddListener(OnSkipBtnClick);
    }
    private void Awake()
    {
        mVideoPlayer = this.gameObject.GetComponent<VideoPlayer>();
        if (mVideoPlayer != null) mVideoPlayer.targetTexture.Release();
        // ������Ƶ���Ž���
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached += EndReached;
        if (mBtn_Skip != null) mBtn_Skip.onClick.AddListener(OnSkipBtnClick);
    }
    private void OnDisable()
    {
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached -= EndReached;
    }
    private void Start()
    {
        skip = GameObject.Find("����");
        skip.SetActive(false);
    }
    private void EndReached(VideoPlayer source)
    {
        // ���ص�ǰ�ű�����
        SceneManager.LoadScene(2);
        gameObject.SetActive(false);
        
    }

    // �ⲿ���ò���
    public void PlayVideo()
    {
        GameObject start = GameObject.Find("start");
        start.GetComponent<Animator>().SetTrigger("���");
        start.SetActive(false);
        skip.SetActive(true);
        GameObject.Find("EXIT").SetActive(false);
        mVideoPlayer.Play();
    }

    // ������Ƶ
    public void OnSkipBtnClick()
    {
        mVideoPlayer.Stop();
        //  EndReached(mVideoPlayer);
        SceneManager.LoadScene(2);
    }
}
