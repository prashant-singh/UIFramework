using UnityEngine;

namespace Prashant
{
    public class ScreenView: UIBase
    {
        public override void Show(EnableDirection m_direction)
        {
            base.ShowCanvas(m_direction);
           
        }

        public override void Hide(EnableDirection m_direction)
        {
            base.HideCanvas(m_direction);
           
        }

        public override void OnScreenLoaded()
        {
            _raycaster.enabled = true;
            
        }

        public override void OnScreenHidden()
        {
            _raycaster.enabled = false;
           
        }

        public bool isCanvasActive()
        {
            return _canvas.enabled;
        }
    }
}