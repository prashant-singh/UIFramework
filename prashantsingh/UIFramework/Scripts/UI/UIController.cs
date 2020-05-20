using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prashant
{
    public enum ScreenType
    {
        Screen1,
        Screen2,
        Screen3,
        Screen4
    }
    [System.Serializable]
    public struct ScreenCollection
    {
        public ScreenView _screen;
        public ScreenType _type;
    }

    public class UIController: MonoBehaviour
    {
        public static UIController instance;
        [SerializeField] List<ScreenCollection> _allScreens;

        [SerializeField] ScreenType _currentScreen;
        [SerializeField] ScreenType _previousScreen;


        private void Awake()
        {
            instance = this;
        }

        public void ShowThisScreen(ScreenType _screenToShow, EnableDirection _direction, Action _tempAction = null)
        {
            _previousScreen = _currentScreen;
            ScreenView m_screen = FindScreen(_screenToShow);
            _currentScreen = _screenToShow;
            m_screen.Show(_direction);
        }

        public void HideThisScreen(ScreenType _screenToHide, EnableDirection _direction, Action _tempAction = null)
        {
            ScreenView m_screen = FindScreen(_screenToHide);
            m_screen.Hide(_direction);
        }

        public void OpenPreviousScreen(Action _tempAction = null)
        {
            ShowThisScreen(_previousScreen, EnableDirection.Reverse, _tempAction);
        }

        public void HideCurrentScreen(Action _tempAction = null)
        {
            HideThisScreen(_currentScreen, EnableDirection.Reverse, _tempAction);
        }

        ScreenView FindScreen(ScreenType _type)
        {
            return _allScreens.Find(x => (x._type == _type))._screen;
        }

        public bool isScreenActive(ScreenType m_type)
        {
            return _allScreens.Find(x => (x._type == m_type))._screen.isCanvasActive();
        }
    }
}