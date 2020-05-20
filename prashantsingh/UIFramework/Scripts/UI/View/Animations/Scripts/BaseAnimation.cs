using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Prashant
{
    public abstract class BaseAnimation : MonoBehaviour
    {

        public abstract void SetupAnimation(Transform _targetTransform);
        public abstract void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish);
        public abstract void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish);

        public abstract void ResetAnimation();

    }
}