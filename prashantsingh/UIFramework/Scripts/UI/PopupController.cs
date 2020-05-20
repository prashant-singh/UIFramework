using System.Collections.Generic;
using UnityEngine;

namespace Prashant
{
    public enum PopupType
    {
        Popup1,
        Popup2
    }
    [System.Serializable]
    public struct PopupData
    {
        public PopupView view;
        public PopupType _type;
    }
    public class PopupController: MonoBehaviour
    {
        public static PopupController instance;
        [SerializeField] List<PopupData> _popups;
        List<PopupType> _allOpenedPopups;

        private void Awake()
        {
            instance = this;
            _allOpenedPopups = new List<PopupType>();
        }



        public void OpenPopupScreen(PopupType _popupToOpen)
        {
            PopupView view = _popups.Find(x => (x._type == _popupToOpen)).view;
            view.Show(EnableDirection.Forward);
            _allOpenedPopups.Add(_popupToOpen);

        }

        void ClosePopupScreen(PopupType _popupToClose)
        {
            PopupView view = _popups.Find(x => (x._type == _popupToClose)).view;
            view.Hide(EnableDirection.Forward);
            _allOpenedPopups.Remove(_popupToClose);
        }

        public void CloseLastOpened()
        {
            if (_allOpenedPopups.Count > 0)
                ClosePopupScreen(_allOpenedPopups[_allOpenedPopups.Count - 1]);
        }

    }
}