using System;
using DG.Tweening;
using UnityEngine;

namespace Prashant
{
    // [DisallowMultipleComponent]
    public abstract class BaseAnimation : MonoBehaviour
    {
        protected bool isAnimating;
        public abstract void SetupAnimation(Transform _targetTransform);
        public virtual void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish) { }
        public virtual void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish) { }

        // public virtual void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, Action _doThisOnFinish) { }
        // public virtual void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, Action _doThisOnFinish) { }
        public abstract void ResetAnimation();
    }
}