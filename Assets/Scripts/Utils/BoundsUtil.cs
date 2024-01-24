using UnityEngine;

public class BoundsUtil
{
    [System.Flags]
    public enum Direction {
        Everything = -1,
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
    }

    public static Direction CalcOutsideBoundsDirection(Camera camera, Vector2 worldPos)
    {
        Vector2 viewCoords = camera.WorldToViewportPoint(worldPos);

        if (viewCoords.x > 1.0f) 
        {
            return Direction.East;
        } 
        else if (viewCoords.x < 0.0f)
        {
            return Direction.West;
        }
        else if (viewCoords.y > 1.0f)
        {
            return Direction.North;
        }
        else if (viewCoords.y < 0.0f)
        {
            return Direction.South;
        }
        else
        {
            return Direction.None;
        }
    }
}