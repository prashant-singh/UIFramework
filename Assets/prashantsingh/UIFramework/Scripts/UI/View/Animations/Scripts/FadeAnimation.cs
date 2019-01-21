using System;
using DG.Tweening;
using UnityEngine;

namespace Prashant
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : BaseAnimation
    {
        CanvasGroup _currentCanvasGroup;
        public override void SetupAnimation(Transform _targetTransform)
        {
            _currentCanvasGroup = _targetTransform.GetComponent<CanvasGroup>();
        }

        public override void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            // StartCoroutine(StartFading(1,_animationTime,_doThisOnFinish));
        }

        public override void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            // StartCoroutine(StartFading(0,_animationTime,_doThisOnFinish));
        }

        public override void ResetAnimation()
        {
            throw new NotImplementedException();
        }

        // IEnumerator StartFading(float targetFadeAmount, float _fadingTime,Action _doThisOnFinish)
        // {
        //     // float counter = _fadingTime;
        //     // while (counter > 0)
        //     // {
        //     //     counter -= Time.deltaTime;
        //     //     // _currentCanvasGroup.alpha = 
        //     //     yield return null;
        //     // }
        //     // _darkBG.color = _targetColor;
        //     // _doThisOnFinish();
        //     // yield return null;
        // }

    }
}