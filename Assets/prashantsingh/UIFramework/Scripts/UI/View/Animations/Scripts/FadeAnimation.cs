using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Prashant
{
    [DisallowMultipleComponent]

    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimation : BaseAnimation
    {
        TweenCallback showCallback, hideCallback;
        CanvasGroup _currentCanvasGroup;

        public override void SetupAnimation(Transform _targetTransform)
        {
            _currentCanvasGroup = _targetTransform.GetComponent<CanvasGroup>();
        }

        public override void ShowAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            if (isAnimating)
            {
                StopCoroutine(E_StartFading);
                E_StartFading = null;
                //Debug.Log("Show Overridden");
                showCallback?.Invoke();
            }
            isAnimating = true;
            showCallback = _doThisOnFinish;
            E_StartFading = StartFading(1, _animationTime);
            StartCoroutine(E_StartFading);
        }

        public override void HideAnimation(EnableDirection _direction, float _animationTime, Ease _animationEffect, TweenCallback _doThisOnFinish)
        {
            if (isAnimating)
            {
                StopCoroutine(E_StartFading);
                E_StartFading = null;
                //Debug.Log("Hide Overridden");
                hideCallback?.Invoke();
            }
            isAnimating = true;
            hideCallback = _doThisOnFinish;
            E_StartFading = StartFading(0, _animationTime);
            StartCoroutine(E_StartFading);
        }

        public override void ResetAnimation()
        {
            //throw new NotImplementedException();
        }

        IEnumerator E_StartFading;
        IEnumerator StartFading(float targetFadeAmount, float _fadingTime)
        {
            //Debug.Log("Started Fading " + targetFadeAmount);
            bool shouldFadeIn = targetFadeAmount != 0;
            _currentCanvasGroup.alpha = shouldFadeIn ? 0 : 1;
            if (shouldFadeIn)
            {
                while (_currentCanvasGroup.alpha < 1)
                {
                    _currentCanvasGroup.alpha += Time.deltaTime * (10 / _fadingTime);
                    yield return null;
                }
            }
            else
            {
                while (_currentCanvasGroup.alpha > 0)
                {
                    _currentCanvasGroup.alpha -= Time.deltaTime * (1 / _fadingTime);
                    yield return null;
                }
            }
            if (targetFadeAmount == 0) hideCallback?.Invoke();
            else showCallback?.Invoke();
            isAnimating = false;
            //Debug.Log("Calling the callback " + targetFadeAmount, gameObject);
            yield return null;
        }

    }
}