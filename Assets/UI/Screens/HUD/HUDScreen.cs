using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDScreen : ScreenBase
{
    [Header("UXML Element Strings")]

    [SerializeField]
    private string _pointsLabelID = "points-label";

    [SerializeField]
    private string _healthContainerID = "health-container";

    private Label _pointsLabel;

    private int _playerMaxHealth;

    private Player _playerController;

    private PointsScript _pointsMgr;

    protected override void OnEnable()
    {
        base.OnEnable();

        _pointsLabel = GetRootElement().Query<Label>(_pointsLabelID);

        _pointsMgr = PointsScript.Instance;
        _pointsMgr.onAddPoints += OnAddPoints;
    }

    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<Player>();
        _playerMaxHealth = _playerController.HP;
        _playerController.onPlayerTakeDamage += OnTakeDamage;

        for (int i = 0; i < _playerMaxHealth; i++)
        {
            // Set health UI
            VisualElement elem = GetRootElement().Query<VisualElement>(_healthContainerID).Children<VisualElement>().AtIndex(i);
            elem.style.display = DisplayStyle.Flex;
        }

        _pointsLabel.text = string.Format("Points: {0:N0}", _pointsMgr.GetPointCount());
    }

    private void OnAddPoints(int newPoints)
    {
        _pointsLabel.text = string.Format("Points: {0:N0}", newPoints);
    }

    private void OnTakeDamage(int damageTaken)
    {
        int newHealth = _playerController.HP;

        Color healthBarColor = Color.green;

        if ((float)newHealth / _playerMaxHealth <= 0.35f || newHealth <= 1)
        {
            healthBarColor = Color.red;
        }
        else if ((float)newHealth / _playerMaxHealth < 0.7f)
        {
            healthBarColor = Color.yellow;
        }

        for (int i = 0; i < _playerMaxHealth; i++)
        {
            VisualElement elem = GetRootElement().Query<VisualElement>(_healthContainerID).Children<VisualElement>().AtIndex(i);
            elem.ElementAt(0).style.unityBackgroundImageTintColor = new StyleColor(healthBarColor);
        }

        for (int i = newHealth; i < _playerMaxHealth; i++)
        {
            // Set health UI
            VisualElement elem = GetRootElement().Query<VisualElement>(_healthContainerID).Children<VisualElement>().AtIndex(i);
            elem.ElementAt(0).style.opacity = 0;
        }
    }
}
