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
    public void addPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        text.text = "Points: " + _points.ToString();
    }
}
