using System;
using DG.Tweening;
using UnityEngine;

namespace Prashant
{
    [DisallowMultipleComponent]
    public class ScaleAnimation : BaseAnimation
    {
        Transform m_targetTransform;
        Vector2 _scaleFrom, _initScale;
        public override void SetupAnimation(Transform _targetTransform)
        {
            m_targetTransform = _targetTransform;
            _initScale = _targetTransform.localScale;
            _scaleFrom = Vector2.zero;
        }

        public override void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            if (m_targetTransform == null) Debug.Log("Please SetupAnimation before starting the animation");
            m_targetTransform.localScale = _scaleFrom;
            m_targetTransform.DOScale(_initScale, _animationTime).SetEase(_animationEffect).OnComplete(_doThisOnFinish);
        }

        public override void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            if (m_targetTransform == null) Debug.Log("Please SetupAnimation before starting the animation");
            m_targetTransform.DOScale(_scaleFrom, _animationTime).SetEase(_animationEffect).OnComplete(_doThisOnFinish);
        }

        public override void ResetAnimation()
        {
            throw new NotImplementedException();
        }
    }
}