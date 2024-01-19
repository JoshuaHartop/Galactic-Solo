using UnityEngine;

public class DestroyOnOutOfBounds : MonoBehaviour 
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

    // [System.AttributeUsage(System.AttributeTargets.Enum)]
    public Direction destroyFlags;

    private static Camera _mainCamera;

    private void Start()
    {
        if (!_mainCamera)
            _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            Direction outOfBoundsDir = CalcOutsideBoundsDirection();

            if (outOfBoundsDir != Direction.None && 
                destroyFlags.HasFlag(outOfBoundsDir))
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector2 GetPositionInViewport()
    {
        return _mainCamera.WorldToViewportPoint(transform.position);
    }

    public Direction CalcOutsideBoundsDirection()
    {
        Vector2 viewCoords = GetPositionInViewport();

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