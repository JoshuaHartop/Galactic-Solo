using UnityEngine;

/// <summary>
/// Invokes the `onOutOfBounds()` event when this GameObject goes out of
/// the bounds of the viewport. Only sends a new event if it is different
/// from the previously sent one.
/// </summary>
public class OutOfBoundsListener : MonoBehaviour
{
    public delegate void OnOutOfBounds(BoundsDirection direction);
    public event OnOutOfBounds onOutOfBounds;

    [System.Flags]
    public enum BoundsDirection {
        Everything = -1,
        None = 0,
        North = 1,
        East = 2,
        South = 4,
        West = 8,
    }

    [SerializeField]
    [Tooltip("The camera used to determine if something is out of bounds. If null, the camera tagged MainCamera will be used.")]
    private Camera _viewportCamera;

    [SerializeField]
    [Tooltip("The amount of frames to wait before performing each out of bounds check.")]
    private int _checkInterval = 20;

    [SerializeField]
    [Tooltip("Adds a safe zone area to the bounds check, effectively making the viewport smaller than it actually is. Value is in percentage of viewport size, adjusted for viewport aspect ratio.")]
    private float _safeZone = 0f;

    private BoundsDirection _lastOutOfBoundsDirection;

    private void Awake()
    {
        if (_viewportCamera == null)
            _viewportCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.frameCount % _checkInterval == 0)
        {
            BoundsDirection dir = CalcOutsideBoundsDirection(_viewportCamera, transform.position, _safeZone);

            if (_lastOutOfBoundsDirection != dir)
            {
                _lastOutOfBoundsDirection = dir;

                if (onOutOfBounds != null)
                    onOutOfBounds.Invoke(dir);
            }
        }
    }

    public static BoundsDirection CalcOutsideBoundsDirection(Camera camera, Vector2 worldPos, float safeZone = 0f)
    {
        Vector2 viewCoords = camera.WorldToViewportPoint(worldPos);

        BoundsDirection dirFlag = BoundsDirection.None;

        if (viewCoords.x > 1.0f - safeZone)
        {
            dirFlag |= BoundsDirection.East;
        } 
        else if (viewCoords.x < 0.0f + safeZone)
        {
            dirFlag |= BoundsDirection.West;
        }

        if (viewCoords.y > 1.0f - safeZone * camera.aspect)
        {
            dirFlag |= BoundsDirection.North;
        }
        else if (viewCoords.y < 0.0f + safeZone * camera.aspect)
        {
            dirFlag |= BoundsDirection.South;
        }

        return dirFlag;
    }
}
