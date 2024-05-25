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
        // 清除Raw Image的残留帧

        
        if (mVideoPlayer!=null) mVideoPlayer.targetTexture.Release();
        // 监听视频播放结束
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached += EndReached;
        if (mBtn_Skip != null) mBtn_Skip.onClick.AddListener(OnSkipBtnClick);
    }
    private void Awake()
    {
        mVideoPlayer = this.gameObject.GetComponent<VideoPlayer>();
        if (mVideoPlayer != null) mVideoPlayer.targetTexture.Release();
        // 监听视频播放结束
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached += EndReached;
        if (mBtn_Skip != null) mBtn_Skip.onClick.AddListener(OnSkipBtnClick);
    }
    private void OnDisable()
    {
        if (mVideoPlayer != null) mVideoPlayer.loopPointReached -= EndReached;
    }
    private void Start()
    {
        skip = GameObject.Find("跳过");
        skip.SetActive(false);
    }
    private void EndReached(VideoPlayer source)
    {
        // 隐藏当前脚本对象
        SceneManager.LoadScene(2);
        gameObject.SetActive(false);
        
    }

    // 外部调用播放
    public void PlayVideo()
    {
        GameObject start = GameObject.Find("start");
        start.GetComponent<Animator>().SetTrigger("点击");
        start.SetActive(false);
        skip.SetActive(true);
        GameObject.Find("EXIT").SetActive(false);
        mVideoPlayer.Play();
    }

    // 跳过视频
    public void OnSkipBtnClick()
    {
        mVideoPlayer.Stop();
        //  EndReached(mVideoPlayer);
        SceneManager.LoadScene(2);
    }
}
