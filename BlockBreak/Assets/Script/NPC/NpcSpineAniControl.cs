using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpineAniControl : MonoBehaviour
{
    //这个是目前专用于瓶子NPC的
    private SkeletonAnimation skeletonAnimation;

    void Start()
    {
        // 获取 SkeletonAnimation 组件
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        // 订阅动画完成事件
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;

        // 设置初始动画（循环）
        SetAnimation("idle",true);
    }

    public void SetAnimation(string animationName, bool loop)
    {
        // 切换动画
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        // 检查当前动画名称
        if (trackEntry.Animation.Name == "ban")
        {
            // 在动画播放完毕后，播放动画（循环）
            SetAnimation("ban_idle", true);
        }
        if (trackEntry.Animation.Name == "pass")
        {
            // 在动画播放完毕后，播放动画（循环）
            SetAnimation("pass_idle", true);
        }
        if (trackEntry.Animation.Name == "fight")
        {
            // 在动画播放完毕后，播放动画（循环）
            SetAnimation("idle", true);
        }
    }
}
