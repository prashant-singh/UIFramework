using System;
using System.Collections;
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
            _currentCanvasGroup.alpha = 0;
            _currentCanvasGroup.DOFade(1, _animationTime).OnComplete(_doThisOnFinish);
        }

        public override void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            _currentCanvasGroup.alpha = 1;
            _currentCanvasGroup.DOFade(0, _animationTime).OnComplete(_doThisOnFinish);
        }

        public override void ResetAnimation()
        {
            _currentCanvasGroup.DOKill();
        }
    }
}