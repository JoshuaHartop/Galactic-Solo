using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct MenuScreenPair
{
    public string id;
    public ScreenBase screen;
}

public class MenuScreenManager : LocalManager<MenuScreenManager>
{
    protected override bool Persistent => false;

    [SerializeField]
    private MenuScreenPair[] _screens;

    [SerializeField]
    private MenuScreenPair _startScreen;

    private MenuScreenPair _currentScreen;

    protected override void Start()
    {
        base.Start();

        if (_startScreen.screen != null)
        {
            _startScreen.screen.Show();
            _currentScreen = _startScreen;
        }

        foreach (MenuScreenPair pair in _screens)
        {
            if (pair.screen == null || pair.id != _startScreen.id)
            {
                pair.screen.Hide();
            }
        }
    }

    public void OpenMenu(string menuID)
    {
        foreach (MenuScreenPair pair in _screens)
        {
            if (pair.id == menuID)
            {
                _currentScreen.screen.Hide();
                _currentScreen = pair;
                _currentScreen.screen.Show();
            }
        }
    }

    public string GetCurrentScreenID()
    {
        return _currentScreen.id;
    }
}
