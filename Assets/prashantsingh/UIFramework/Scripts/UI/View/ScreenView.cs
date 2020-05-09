using System.Collections;
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
            if (E_CheckForBack != null)
            {
                StopCoroutine(E_CheckForBack);
                E_CheckForBack = null;
            }
        }

        public override void OnScreenLoaded()
        {
            _raycaster.enabled = true;
            if (E_CheckForBack != null)
            {
                StopCoroutine(E_CheckForBack);
                E_CheckForBack = null;
            }
            E_CheckForBack = CheckForBack();
            StartCoroutine(E_CheckForBack);

        }

        public override void OnScreenHidden()
        {
            _raycaster.enabled = false;

        }

        public bool isCanvasActive()
        {
            return _canvas.enabled;
        }

        IEnumerator E_CheckForBack;
        IEnumerator CheckForBack()
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
    }
}