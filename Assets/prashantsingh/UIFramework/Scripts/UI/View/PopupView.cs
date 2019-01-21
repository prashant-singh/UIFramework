using UnityEngine;
namespace Prashant
{
    public class PopupView : ScreenView
    {
        public void OnPopupClose()
        {
            PopupController.instance.CloseLastOpened();
        }
    }
}