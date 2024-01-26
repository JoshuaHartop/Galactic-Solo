using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuScreen : ScreenBase
{
    [Header("UXML element identifiers")]
    [SerializeField]
    private string _backButtonID;

    [SerializeField]
    private string _volumeSliderID;

    [SerializeField]
    private string _volumeSliderIndicatorID;

    private Button _backButton;

    private SliderInt _volumeSlider;

    private VisualElement _volumeSliderIndicator;

    protected override void OnEnable()
    {
        base.OnEnable();

        _backButton = GetRootElement().Query<Button>(_backButtonID);
        _backButton.RegisterCallback<ClickEvent>(OnClickBackButton);

        _volumeSlider = GetRootElement().Query<SliderInt>(_volumeSliderID);
        _volumeSlider.RegisterCallback<ChangeEvent<int>>(OnVolumeSliderChange);
        _volumeSlider.value = Mathf.CeilToInt(SoundManager.Instance.Volume * 10f);

        _volumeSliderIndicator = GetRootElement().Query<VisualElement>(_volumeSliderIndicatorID);
    }

    protected override void OnSetFocus(VisualElement element)
    {
        base.OnSetFocus(element);

        // Means menu is not enabled, we can just return
        if (_volumeSliderIndicator == null)
            return;

        if (element.name == _volumeSliderID)
        {
            // Apply focus styling
            _volumeSliderIndicator.style.opacity = 1;
        }
        else
        {
            _volumeSliderIndicator.style.opacity = 0;
        }
    }

    private void OnClickBackButton(ClickEvent evt)
    {
        GetMenuManager().OpenMenu("main-menu-screen");
    }

    private void OnVolumeSliderChange(ChangeEvent<int> evt)
    {
        // Volume gives a value between 0-10 with 1 step but it has to
        // be a factor between 0-1
        float scaledVolume = evt.newValue * .1f;
        SoundManager.Instance.Volume = scaledVolume;
    }
}
