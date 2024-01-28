using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScreen : ScreenBase
{
    // Yuck.

    [Header("UXML element identifiers")]
    [SerializeField]
    private string _startButtonID = "start-button";

    [SerializeField]
    private string _settingsButtonID = "settings-button";

    [SerializeField]
    private string _quitButtonID = "quit-button";

    [Header("Menu IDs")]

    [SerializeField]
    private string _settingsMenuID = "settings-menu";

    private Button _startButton;
    private Button _settingsButton;

    private Button _quitButton;

    [Header("Misc")]

    [SerializeField]
    private string _playSceneName;

    protected override void OnEnable()
    {
        base.OnEnable();

        _startButton = GetRootElement().Query<Button>(_startButtonID);
        _startButton.RegisterCallback<ClickEvent>(OnClickStartButton);

        _settingsButton = GetRootElement().Query<Button>(_settingsButtonID);
        _settingsButton.RegisterCallback<ClickEvent>(OnClickSettingsButton);

        _quitButton = GetRootElement().Query<Button>(_quitButtonID);
        _quitButton.RegisterCallback<ClickEvent>(OnClickQuitButton);
    }

    private void OnClickStartButton(ClickEvent evt)
    {
        // Maybe open loading menu?
        SceneManager.LoadScene(_playSceneName);
    }

    private void OnClickSettingsButton(ClickEvent evt)
    {
        GetMenuManager().OpenMenu(_settingsMenuID);
    }

    private void OnClickQuitButton(ClickEvent evt)
    {
        Debug.Log("Quitting...");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
