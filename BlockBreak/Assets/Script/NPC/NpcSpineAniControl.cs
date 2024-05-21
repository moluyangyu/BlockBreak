using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpineAniControl : MonoBehaviour
{
    //�����Ŀǰר����ƿ��NPC��
    private SkeletonAnimation skeletonAnimation;

    void Start()
    {
        // ��ȡ SkeletonAnimation ���
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        // ���Ķ�������¼�
        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;

        // ���ó�ʼ������ѭ����
        SetAnimation("idle",true);
    }

    public void SetAnimation(string animationName, bool loop)
    {
        // �л�����
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        // ��鵱ǰ��������
        if (trackEntry.Animation.Name == "ban")
        {
            // �ڶ���������Ϻ󣬲��Ŷ�����ѭ����
            SetAnimation("ban_idle", true);
        }
        if (trackEntry.Animation.Name == "pass")
        {
            // �ڶ���������Ϻ󣬲��Ŷ�����ѭ����
            SetAnimation("pass_idle", true);
        }
        if (trackEntry.Animation.Name == "fight")
        {
            // �ڶ���������Ϻ󣬲��Ŷ�����ѭ����
            SetAnimation("idle", true);
        }
    }
}
