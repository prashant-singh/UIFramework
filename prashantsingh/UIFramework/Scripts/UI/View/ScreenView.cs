using UnityEngine;

namespace Prashant
{
    public class ScreenView : UIBase
    {
        public override void Show(EnableDirection m_direction = EnableDirection.Forward)
        {
            base.ShowCanvas(m_direction);
        }

        public override void Hide(EnableDirection m_direction = EnableDirection.Forward)
        {
            base.HideCanvas(m_direction);
        }

        public override void OnScreenLoaded()
        {
            base.OnScreenLoaded();
            _raycaster.enabled = true;
            // Debug.Log("Screen Loaded");
        }

        public override void OnScreenHidden()
        {
            base.OnScreenHidden();
            _raycaster.enabled = false;
            // Debug.Log("Screen Hidden");
        }

        public bool isCanvasActive()
        {
            return _canvas.enabled;
        }
    }
}