using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private StyleSheet _stylesheet;

    [SerializeField]
    private SceneAsset _playScene;

    private Button _startButton;
    private Button _quitButton;

    private Slider _volumeSlider;

    private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        _startButton = uiDocument.rootVisualElement.Query<Button>("play-button");
        _startButton.RegisterCallback<ClickEvent>(OnClickPlayButton);

        _quitButton = uiDocument.rootVisualElement.Query<Button>("quit-button");
        _quitButton.RegisterCallback<ClickEvent>(OnClickQuitButton);

        _volumeSlider = uiDocument.rootVisualElement.Query<Slider>("volume-slider");
        _volumeSlider.RegisterCallback<ChangeEvent<float>>(OnVolumeChangedCallback);
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnClickPlayButton);
        _startButton.UnregisterCallback<ClickEvent>(OnClickQuitButton);
    }

    private void OnClickPlayButton(ClickEvent e) 
    {
        SceneManager.LoadScene(_playScene.name);
    }

    private void OnClickQuitButton(ClickEvent e) 
    {
        Debug.Log("Quitting application");

        #if UNITY_EDITOR

        EditorApplication.isPlaying = false;

        #else

        Application.Quit();

        #endif
    }

    private void OnVolumeChangedCallback(ChangeEvent<float> e)
    {
        SoundManager.Instance.Volume = e.newValue;
    }
}