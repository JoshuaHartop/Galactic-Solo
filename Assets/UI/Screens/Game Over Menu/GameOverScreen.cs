using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverScreen : ScreenBase
{
    [SerializeField]
    private string _playSceneName;

    [SerializeField]
    private string _mainMenuSceneName;


    [Header("UXML element identifiers")]
    [SerializeField]
    private string _restartButtonID;

    [SerializeField]
    private string _shopButtonID;

    [SerializeField]
    private string _returnButtonID; 

    private Button _restartButton;
    private Button _shopButton;
    private Button _returnButton;

    protected override void OnEnable()
    {
        base.OnEnable();

        _restartButton = GetRootElement().Query<Button>(_restartButtonID);
        _restartButton.RegisterCallback<ClickEvent>(OnClickRestartButton);

        _shopButton = GetRootElement().Query<Button>(_shopButtonID);
        _shopButton.RegisterCallback<ClickEvent>(OnClickShopButton);

        _returnButton = GetRootElement().Query<Button>(_returnButtonID);
        _returnButton.RegisterCallback<ClickEvent>(OnClickReturnButton);
    }

    private void OnClickRestartButton(ClickEvent evt)
    {
        SceneManager.LoadScene(_playSceneName);
    }

    private void OnClickShopButton(ClickEvent evt)
    {
        GetMenuManager().OpenMenu("shop-menu-screen");
    }

    private void OnClickReturnButton(ClickEvent evt)
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}
