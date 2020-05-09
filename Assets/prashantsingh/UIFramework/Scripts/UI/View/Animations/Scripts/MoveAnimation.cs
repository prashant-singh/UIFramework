using DG.Tweening;
using UnityEngine;

namespace Prashant
{
    [DisallowMultipleComponent]
    public class MoveAnimation : BaseAnimation
    {
        public enum PositionType
        {
            Top,
            Bottom,
            Left,
            Right
        }
        public PositionType _startPositionType = PositionType.Right;
        Vector2 _moveFromPosition, _initPosition;
        float screenWidth, screenHeight;
        Transform m_targetTransform;
        float xOffset = 100, yOffset = 100;

        public override void SetupAnimation(Transform _targetTransform)
        {

            screenWidth = Screen.width > 1080 ? Screen.width : 1080;
            screenHeight = Screen.height > 1920 ? Screen.height : 1920;
            m_targetTransform = _targetTransform;
            _initPosition = _targetTransform.GetComponent<RectTransform>().anchoredPosition;
            switch (_startPositionType)
            {
                case PositionType.Top:
                    _moveFromPosition = new Vector2(_initPosition.x, screenHeight + yOffset);
                    break;
                case PositionType.Bottom:
                    _moveFromPosition = new Vector2(_initPosition.x, -screenHeight - yOffset);
                    break;
                case PositionType.Left:
                    _moveFromPosition = new Vector2(-screenWidth - xOffset, _initPosition.y);
                    break;
                case PositionType.Right:
                    _moveFromPosition = new Vector2(screenWidth + xOffset, _initPosition.y);
                    break;
            }
        }

        Vector2 GetStartPosition()
        {
            switch (_startPositionType)
            {
                case PositionType.Top:
                    return new Vector2(_initPosition.x, -((screenHeight) + yOffset));
                case PositionType.Bottom:
                    return new Vector2(_initPosition.x, -((-screenHeight) - yOffset));
                case PositionType.Left:
                    return new Vector2(-((-screenWidth) - xOffset), _initPosition.y);
                case PositionType.Right:
                    return new Vector2(-((screenWidth) + xOffset), _initPosition.y);
                default:
                    return Vector2.zero;
            }
        }



        public override void ShowAnimation(EnableDirection _direction, float contentMoveTime, Ease _animationEffect, TweenCallback doThisOnFinish)
        {
            if (isAnimating)
            {
                m_targetTransform.DOKill();
                doThisOnFinish?.Invoke();
            }
            if (m_targetTransform == null)
            {
                Debug.Log("Please Setup Animation before starting the animation");
                return;
            }
            Vector2 m_tempStartPos = _moveFromPosition;
            if (_direction == EnableDirection.Reverse)
            {
                m_tempStartPos = GetStartPosition();
            }
            isAnimating = true;
            m_targetTransform.GetComponent<RectTransform>().anchoredPosition = m_tempStartPos;
            m_targetTransform.DOLocalMove(_initPosition, contentMoveTime).SetEase(_animationEffect).OnComplete(()=> { isAnimating = false;doThisOnFinish?.Invoke(); });
        }

        public override void HideAnimation(EnableDirection _direction, float contentMoveTime, Ease _animationEffect, TweenCallback doThisOnFinish)
        {
            if (isAnimating)
            {
                m_targetTransform.DOKill();
                doThisOnFinish?.Invoke();
            }
            if (m_targetTransform == null)
            {
                Debug.Log("Please Setup Animation before starting the animation");
                return;
            }
            isAnimating = true;
            Vector2 m_tempStartPos = _moveFromPosition;
            //Debug.Log(_direction);
            if (_direction == EnableDirection.Reverse)
            {
                m_tempStartPos = GetStartPosition();
            }
            m_targetTransform.DOLocalMove(m_tempStartPos, contentMoveTime).SetEase(_animationEffect).OnComplete(() => { isAnimating = false; doThisOnFinish?.Invoke(); });
        }

        public override void ResetAnimation()
        {
            m_targetTransform.GetComponent<RectTransform>().anchoredPosition = _initPosition;
        }


    }
}