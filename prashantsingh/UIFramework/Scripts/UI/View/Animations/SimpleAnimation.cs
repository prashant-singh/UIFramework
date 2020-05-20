using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Prashant
{
    public class SimpleAnimation : MonoBehaviour, IBaseCanvasStateListner
    {
        public bool shouldLookForBaseUI = true;
        [SerializeField] bool shouldLookForContent = false;
        bool hasCanvas;
        public float showAnimationTime = 0.8f;
        public Ease showAnimationEffect = Ease.OutExpo;
        public float hideAnimationTime = 0.5f;
        public Ease hideAnimationEffect = Ease.InExpo;
        public UnityEvent OnShowAnimationFinish;
        public UnityEvent OnHideAnimationFinish;
        bool hasAnimationComponent;
        BaseAnimation[] _animationComponents;
        UIBase _currentUIBase;
        Canvas _canvas;
        Transform _targetTransform;
        private void Awake()
        {
            if (!hasAnimationComponent)
                Initialize();
        }

        void Initialize()
        {
            if (GetComponents<BaseAnimation>().Length > 0)
            {
                if (GetComponent<Canvas>())
                {
                    _canvas = GetComponent<Canvas>();
                    hasCanvas = true;
                }
                if (shouldLookForContent && !shouldLookForBaseUI)
                {
                    _targetTransform = transform.Find("Content");
                    if (_targetTransform == null)
                        _targetTransform = transform;
                }
                else
                    _targetTransform = transform;
                _animationComponents = GetComponents<BaseAnimation>();
                for (int count = 0; count < _animationComponents.Length; count++)
                {
                    _animationComponents[count].SetupAnimation(_targetTransform);
                }
                hasAnimationComponent = true;
                if (shouldLookForBaseUI)
                    SubscribeToCanvasEvents();
            }
        }

        public void SubscribeToCanvasEvents()
        {
            if (GetComponentInParent<UIBase>())
            {
                _currentUIBase = GetComponentInParent<UIBase>();
                _currentUIBase.OnPanelStateChanged += OnNotify;
            }
        }

        private void OnDestroy()
        {
            if (GetComponent<BaseAnimation>())
            {
                UnSubscribeToCanvasEvents();
            }
        }

        public void StartShowAnimation(EnableDirection _direction)
        {
            if (!hasAnimationComponent)
                Initialize();
            if (hasAnimationComponent)
            {
                if (hasCanvas)
                    _canvas.enabled = true;
                for (int count = 0; count < _animationComponents.Length; count++)
                {
                    _animationComponents[count].ShowAnimation(_direction, showAnimationTime, showAnimationEffect, ShowAnimationComplete);
                }
                if (_currentUIBase)
                {
                    _currentUIBase.OnElementShowAnimationStarted(this);
                }
                else
                {
                    if (!hasCanvas)
                        gameObject.SetActive(true);
                }
            }
        }

        public void StartHideAnimation(EnableDirection _direction)
        {
            if (!hasAnimationComponent)
                Initialize();
            if (hasAnimationComponent)
            {
                for (int count = 0; count < _animationComponents.Length; count++)
                {
                    _animationComponents[count].HideAnimation(_direction, hideAnimationTime, hideAnimationEffect, HideAnimationComplete);
                }
                if (_currentUIBase)
                {
                    _currentUIBase.OnElementHideAnimationStarted(this);
                }
            }
        }





        void ShowAnimationComplete()
        {
            if (OnShowAnimationFinish != null)
            {
                OnShowAnimationFinish.Invoke();
            }
            if (_currentUIBase)
            {
                _currentUIBase.OnElementShowAnimationFinished(this);
            }

        }

        void HideAnimationComplete()
        {
            if (OnHideAnimationFinish != null)
            {
                OnHideAnimationFinish.Invoke();
            }
            if (hasCanvas)
                _canvas.enabled = false;
            if (_currentUIBase)
            {
                _currentUIBase.OnElementHideAnimationFinished(this);
            }
            else
            {
                if (!hasCanvas)
                    gameObject.SetActive(false);
            }
        }



        public void UnSubscribeToCanvasEvents()
        {
            if (_currentUIBase)
            {
                _currentUIBase.OnPanelStateChanged -= OnNotify;
            }
        }

        public virtual void OnNotify(CanvasState _currentState, EnableDirection _direction)
        {
            if (_currentState == CanvasState.Enabled)
            {
                StartShowAnimation(_direction);
            }
            else
            {
                StartHideAnimation(_direction);
            }
        }

        public void ResetAnimation()
        {
            for (int count = 0; count < _animationComponents.Length; count++)
            {
                _animationComponents[count].ResetAnimation();
            }
        }
    }

}