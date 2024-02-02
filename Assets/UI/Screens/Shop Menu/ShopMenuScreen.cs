using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenuScreen : ScreenBase
{
    [Header("UXML element identifiers")]
    [SerializeField]
    private string _pointsDisplayLabelID;

    [SerializeField]
    private string _bulletCountUpgradeButtonID;

    [SerializeField]
    private string _bulletCountUpgradeInfoLabelID;

    [SerializeField]
    private string _bulletVelocityUpgradeButtonID;

    [SerializeField]
    private string _bulletVelocityUpgradeInfoLabelID;

    [SerializeField]
    private string _shipDurabilityUpgradeButtonID;

    [SerializeField]
    private string _shipDurabilityUpgradeInfoLabelID;

    [SerializeField]
    private string _backButtonID;

    [Header("Screen Reference IDs")]

    [SerializeField]
    private string _mainMenuScreenID;

    private Button _bulletCountUpgradeButton;
    private Label _bulletCountUpgradeInfoLabel;
    private Button _bulletVelocityUpgradeButton;
    private Label _bulletVelocityUpgradeInfoLabel;
    private Button _shipDurabilityUpgradeButton;
    private Label _shipDurabilityUpgradeInfoLabel;
    private Button _backButton;
    private Label _pointsDisplayLabel;

    private PlayerUpgradeData _playerData;
    private PointsData _pointsData;

    [SerializeField]
    private int _bulletCountUpgradeBaseCost;

    private int _bulletCountCost;

    [SerializeField]
    private int _bulletVelocityUpgradeBaseCost;

    private int _bulletVelocityCost;

    [SerializeField]
    private int _shipDurabilityUpgradeBaseCost;

    private int _shipDurabilityCost;

    protected override void OnEnable()
    {
        base.OnEnable();

        _bulletCountUpgradeButton = GetRootElement().Query<Button>(_bulletCountUpgradeButtonID);
        _bulletCountUpgradeButton.RegisterCallback<ClickEvent>(OnClickBulletCountUpgradeButton);

        _bulletCountUpgradeInfoLabel = GetRootElement().Query<Label>(_bulletCountUpgradeInfoLabelID);

        _bulletVelocityUpgradeButton = GetRootElement().Query<Button>(_bulletVelocityUpgradeButtonID);
        _bulletVelocityUpgradeButton.RegisterCallback<ClickEvent>(OnClickBulletVelocityUpgradeButton);

        _bulletVelocityUpgradeInfoLabel = GetRootElement().Query<Label>(_bulletVelocityUpgradeInfoLabelID);

        _shipDurabilityUpgradeButton = GetRootElement().Query<Button>(_shipDurabilityUpgradeButtonID);
        _shipDurabilityUpgradeButton.RegisterCallback<ClickEvent>(OnClickShipDurabilityUpgradeButton);

        _shipDurabilityUpgradeInfoLabel = GetRootElement().Query<Label>(_shipDurabilityUpgradeInfoLabelID);

        _backButton = GetRootElement().Query<Button>(_backButtonID);
        _backButton.RegisterCallback<ClickEvent>(OnClickBackButton);

        _playerData = PlayerUpgradeData.Load();
        _pointsData = PointsData.Load();

        _pointsDisplayLabel = GetRootElement().Query<Label>(_pointsDisplayLabelID);
        _pointsDisplayLabel.text = string.Format("You have {0:N0} pts", _pointsData._savePoints);

        // _bulletCountUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _bulletCountCost, _playerData.bulletCountUpgrades);
        // _bulletVelocityUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _bulletVelocityCost, _playerData.bulletVelocityUpgrades);
        // _shipDurabilityUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _shipDurabilityCost, _playerData.healthUpgrades);

        _bulletCountCost = _bulletCountUpgradeBaseCost * (_playerData.bulletCountUpgrades + 1);
        _bulletVelocityCost = _bulletVelocityUpgradeBaseCost * (_playerData.bulletVelocityUpgrades + 1);
        _shipDurabilityCost = _shipDurabilityUpgradeBaseCost * (_playerData.healthUpgrades + 1);
        
        RefreshUpgradeMenuLabels();
    }

    private void OnDisable()
    {
        _playerData.Save();
        _pointsData.Save();
    }

    private void RefreshUpgradeMenuLabels()
    {
        if (_playerData.bulletCountUpgrades >= 3)
        {
            _bulletCountUpgradeInfoLabel.text = "Maxed out! (3/3)";
        }
        else
        {
            _pointsDisplayLabel.text = string.Format("You have {0:N0} pts", _pointsData._savePoints);
            _bulletCountUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _bulletCountCost, _playerData.bulletCountUpgrades);
        }

        if (_playerData.bulletVelocityUpgrades >= 3)
        {
            _bulletVelocityUpgradeInfoLabel.text = "Maxed out! (3/3)";
        }
        else
        {
            _pointsDisplayLabel.text = string.Format("You have {0:N0} pts", _pointsData._savePoints);
            _bulletVelocityUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _bulletVelocityCost, _playerData.bulletVelocityUpgrades);
        }

        if (_playerData.healthUpgrades >= 3)
        {
            _shipDurabilityUpgradeInfoLabel.text = "Maxed out! (3/3)";
        }
        else
        {
            _pointsDisplayLabel.text = string.Format("You have {0:N0} pts", _pointsData._savePoints);
            _shipDurabilityUpgradeInfoLabel.text = string.Format("Costs {0:N0} pts ({1}/3)", _shipDurabilityCost, _playerData.healthUpgrades);
        }
    }

    private void OnClickBulletCountUpgradeButton(ClickEvent evt)
    {
        if (_playerData.bulletCountUpgrades < 3 && _pointsData._savePoints >= _bulletCountCost)
        {
            _pointsData._savePoints -= _bulletCountCost;
            _playerData.bulletCountUpgrades += 1;

            _bulletCountCost = _bulletCountUpgradeBaseCost * (_playerData.bulletCountUpgrades + 1);

            RefreshUpgradeMenuLabels();
        }
    }

    private void OnClickBulletVelocityUpgradeButton(ClickEvent evt)
    {
        if (_playerData.bulletVelocityUpgrades < 3 && _pointsData._savePoints >= _bulletVelocityCost)
        {
            _pointsData._savePoints -= _bulletVelocityCost;
            _playerData.bulletVelocityUpgrades += 1;

            _bulletVelocityCost = _bulletVelocityUpgradeBaseCost * (_playerData.bulletVelocityUpgrades + 1);

            RefreshUpgradeMenuLabels();
        }
    }

    private void OnClickShipDurabilityUpgradeButton(ClickEvent evt)
    {
        if (_playerData.healthUpgrades < 3 && _pointsData._savePoints >= _shipDurabilityCost)
        {
            _pointsData._savePoints -= _shipDurabilityCost;
            _playerData.healthUpgrades += 1;

            _shipDurabilityCost = _shipDurabilityUpgradeBaseCost * (_playerData.healthUpgrades + 1);

            RefreshUpgradeMenuLabels();
        }
    }

    private void OnClickBackButton(ClickEvent evt)
    {
        GetMenuManager().OpenMenu(_mainMenuScreenID);
    }
}
