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

    public delegate void OnAddPoints(int newPoints);

    public OnAddPoints onAddPoints;

    public void Awake()
    {
        _pointsData = PointsData.Load();
        _points = _pointsData._savePoints;
        // text.text = "Points: " + _points.ToString();
    }
    public void addPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        // text.text = "Points: " + _points.ToString();
        _pointsData._savePoints = _points;

        if (onAddPoints != null)
            onAddPoints(_points);
    }

    public void removePoints(int pointsToRemove)
    {
        _points -= pointsToRemove;
        // text.text = "Points: " + _points.ToString();
        _pointsData._savePoints = _points;
    }

    public void OnDestroy()
    {
        _pointsData.Save();
    }

    public int GetPointCount()
    {
        return _points;
    }
}

public class PointsData : SaveData<PointsData>
{
    public int _savePoints;
}