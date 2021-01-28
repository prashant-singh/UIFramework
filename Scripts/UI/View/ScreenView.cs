using UnityEngine;
using System.Collections;

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
            StopEnumerators();
            E_CheckingForBack = CheckingForBack();
            StartCoroutine(E_CheckingForBack);
            _raycaster.enabled = true;
        }

        void StopEnumerators()
        {
            if (E_CheckingForBack != null)
            {
                StopCoroutine(E_CheckingForBack);
                E_CheckingForBack = null;
            }
        }

        public override void OnScreenHideCalled()
        {
            base.OnScreenHideCalled();
            StopEnumerators();
        }
        public override void OnScreenHidden()
        {
            base.OnScreenHidden();
            _raycaster.enabled = false;
        }

        IEnumerator E_CheckingForBack;
        IEnumerator CheckingForBack()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OnBackPressed();
                }
                yield return null;
            }
        }

        public virtual void OnBackPressed()
        {

        }

        public bool isCanvasActive()
        {
            return _canvas.enabled;
        }
    }
}