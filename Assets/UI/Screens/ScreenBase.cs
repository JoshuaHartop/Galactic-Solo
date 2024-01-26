using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class ScreenBase : MonoBehaviour
{
    private static MenuScreenManager _screenManager;

    [SerializeField]
    [Tooltip("Should be sorted in order, first element is first to focus. String should be equal to the element's UXML ID.")]
    private string[] _focusOrder;

    [SerializeField]
    private string _initialFocus;

    private int _currentFocus;

    private UIDocument _uiDocument;
    private IEventHandler _focusedElement;

    protected virtual void OnEnable()
    {
        if (_screenManager == null)
            _screenManager = MenuScreenManager.Instance;

        _uiDocument = gameObject.GetComponent<UIDocument>();
        Debug.Assert(_uiDocument != null);

        SetupCallbacks();

        if (_initialFocus == null || _initialFocus == string.Empty)
        {
            SetFocus(0);
        }
        else
        {
            for (int i = 0; i < _focusOrder.Length; i++)
            {
                if (_focusOrder[i] == _initialFocus)
                {
                    SetFocus(i);
                }
            }
        }
    }

    private void LateUpdate()
    {
        // A little scuffed, but oh well.
        // SetFocus(_currentFocus);
    }

    private void SetupCallbacks()
    {
        // Emulate click event on focused element when submit is pressed
        GetRootElement().RegisterCallback((KeyDownEvent evt) => {
            if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                ClickEvent clickEvt = ClickEvent.GetPooled();
                clickEvt.target = _focusedElement;
                GetRootElement().SendEvent(clickEvt);
            }
        });

        GetRootElement().RegisterCallback((FocusInEvent evt) => {
            _focusedElement = evt.target;
        });

        GetRootElement().RegisterCallback((FocusEvent evt) => {
            Debug.Log("FocusEvent -> " + (GetRootElement().focusController.focusedElement as VisualElement).name);
            if ((GetRootElement().focusController.focusedElement as VisualElement).name != _focusOrder[_currentFocus])
            {
                SetFocus(_currentFocus); // Refocus
            }
        });

        GetRootElement().RegisterCallback((NavigationMoveEvent evt) => {
            switch (evt.direction)
            {
                case NavigationMoveEvent.Direction.Up:
                    SetFocusPrevious();
                    break;
                case NavigationMoveEvent.Direction.Down:
                    SetFocusNext();
                    break;
            }

            evt.PreventDefault();
        });
    }

    private void SetFocusNext()
    {
        SetFocus(_currentFocus + 1);
    }

    private void SetFocusPrevious()
    {
        SetFocus(_currentFocus - 1);
    }

    private void SetFocus(int focusIndex)
    {
        if (focusIndex < 0)
            return;
        else if (focusIndex >= _focusOrder.Length)
            return;

        _currentFocus = focusIndex;

        VisualElement element = GetRootElement().Query<VisualElement>(_focusOrder[focusIndex]);
        element.Focus();

        OnSetFocus(element);
    }

    protected virtual void OnSetFocus(VisualElement element)
    {

    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsHidden()
    {
        return gameObject.activeSelf;
    }

    protected MenuScreenManager GetMenuManager()
    {
        return _screenManager;
    }

    protected UIDocument GetDocument()
    {
        return _uiDocument;
    }

    protected VisualElement GetRootElement()
    {
        return _uiDocument.rootVisualElement;
    }

    protected IEventHandler GetFocusedElement()
    {
        return _focusedElement;
    }
}
