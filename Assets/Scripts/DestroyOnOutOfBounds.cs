using UnityEngine;

[RequireComponent(typeof(OutOfBoundsListener))]
public class DestroyOnOutOfBounds : MonoBehaviour 
{
    public OutOfBoundsListener.BoundsDirection destroyFlags;

    private void Start()
    {
        GetComponent<OutOfBoundsListener>().onOutOfBounds += OnOutOfBounds;
    }

    private void OnDestroy()
    {
        GetComponent<OutOfBoundsListener>().onOutOfBounds -= OnOutOfBounds;
    }

    private void OnOutOfBounds(OutOfBoundsListener.BoundsDirection direction)
    {
        Destroy(gameObject);
    }
}