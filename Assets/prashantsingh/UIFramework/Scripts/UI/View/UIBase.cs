using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prashant
{
    // The listener to subscribe to the canvas enable/disable events
    public interface IBaseCanvasStateListner
    {
        void SubscribeToCanvasEvents();

        void UnSubscribeToCanvasEvents();

        void OnNotify(CanvasState _currentState, EnableDirection _direction);
    }

    public enum CanvasState
    {
        Enabled,
        Disabled
    }

    public enum EnableDirection
    {
        Forward,
        Reverse
    }

    public abstract class UIBase: MonoBehaviour
    {
        protected Canvas _canvas;
        protected GraphicRaycaster _raycaster;

        public delegate void PanelStateChanges(CanvasState status, EnableDirection _direction);
        public event PanelStateChanges OnPanelStateChanged;
        List<IBaseCanvasStateListner> m_showAnimationListeners = new List<IBaseCanvasStateListner>();
        List<IBaseCanvasStateListner> m_hideAnimationListeners = new List<IBaseCanvasStateListner>();
        List<IBaseCanvasStateListner> m_activeAnimationListeners = new List<IBaseCanvasStateListner>();
        CanvasState m_currentState;
        CanvasState _currentState
        {
            get { return m_currentState; }
            set { m_currentState = value; NotifyAll(); }
        }
        EnableDirection _direction;

        // When show animation is called the animation component subscribes to the canvas event.
        public void OnElementShowAnimationStarted(IBaseCanvasStateListner _tempListener)
        {
            m_showAnimationListeners.Add(_tempListener);
           

            if (!m_activeAnimationListeners.Contains(_tempListener))
                m_activeAnimationListeners.Add(_tempListener);
        }

        // When show animation is finished the animation component un-subscribes from the canvas event.
        public void OnElementShowAnimationFinished(IBaseCanvasStateListner _tempListener)
        {
            m_showAnimationListeners.Remove(_tempListener);
            
            if (m_showAnimationListeners.Count <= 0)
            {
                OnScreenLoaded();
            }
        }

        // When Hide animation is called the animation component subscribes to the canvas event.
        public void OnElementHideAnimationStarted(IBaseCanvasStateListner _tempListener)
        {
            m_hideAnimationListeners.Add(_tempListener);
        }

        // When Hide animation is finished the animation component un-subscribes from the canvas event.
        public void OnElementHideAnimationFinished(IBaseCanvasStateListner _tempListener)
        {
            m_hideAnimationListeners.Remove(_tempListener);
            m_activeAnimationListeners.Remove(_tempListener);
            if (m_hideAnimationListeners.Count <= 0)
            {
                OnScreenHidden();
            }
            if (m_activeAnimationListeners.Count <= 0)
            {
                EnableDisableCanvas(false);
            }
        }

        // Notifies the subscribed animation components
        void NotifyAll()
        {
            if (OnPanelStateChanged != null)
                OnPanelStateChanged(_currentState, _direction);
        }

        // When the UIBase is awake
        public virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _raycaster = GetComponent<GraphicRaycaster>();
            Application.targetFrameRate = 60;
        }

        // Abstract show method
        public abstract void Show(EnableDirection m_direction);

        // Abstract Hide method
        public abstract void Hide(EnableDirection m_direction);

        // Show Canvas method to enable canvas component
        protected void ShowCanvas(EnableDirection m_direction)
        {
            _direction = m_direction;
            _currentState = CanvasState.Enabled;
            OnScreenShowCalled();
            EnableDisableCanvas(true);
        }

        // Hide Canvas method to disable canvas component
        protected void HideCanvas(EnableDirection m_direction)
        {
            _direction = m_direction;
            _currentState = CanvasState.Disabled;
            OnScreenHideCalled();
            // Debug.Log("---m_hideAnimationListener "+m_hideAnimationListeners.Count,gameObject);
            if (m_hideAnimationListeners.Count <= 0)
            {
                EnableDisableCanvas(false);
            }
        }

        // Screen show animation start callback
        public virtual void OnScreenShowCalled()
        {

        }

        // Screen hide animation start callback
        public virtual void OnScreenHideCalled()
        {

        }

        // Enable/Disable Canvas Component
        void EnableDisableCanvas(bool status)
        {
            // Debug.Log("canvas state " + status + "", gameObject);
            _canvas.enabled = status;
        }

        // Screen show animation finished callback
        public virtual void OnScreenLoaded()
        {

        }

        // Screen hide animation finished callback
        public virtual void OnScreenHidden()
        {

        }
    }
}
