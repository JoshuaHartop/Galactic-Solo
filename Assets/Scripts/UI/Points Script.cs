using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PointsScript : LocalManager<PointsScript>
{
    protected override bool Persistent => false;
    public TMP_Text text;
    private int _points;
    private PointsData _pointsData;

    public void Awake()
    {
        _pointsData = PointsData.Load();
        _points = _pointsData._savePoints;
    }
    public void addPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        text.text = "Points: " + _points.ToString();
        _pointsData._savePoints = _points;
    }

    public void removePoints(int pointsToRemove)
    {
        _points -= pointsToRemove;
        text.text = "Points: " + _points.ToString();
        _pointsData._savePoints = _points;
    }

    public void OnDestroy()
    {
        _pointsData.Save();
    }
}

public class PointsData : SaveData<PointsData>
{
    public int _savePoints;
}