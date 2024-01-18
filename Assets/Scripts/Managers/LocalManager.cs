using UnityEngine;

/// <summary>
/// A base class for creating singleton instances of MonoBehaviours.
/// As opposited to GlobalManager, this type of manager is not preloaded
/// and is only accessible from scenes to which it has been manually added as
/// a component of the Manager object, or as a component of a child object of
/// the Manager object.
/// </summary>
public abstract class LocalManager<ManagerType> : MonoBehaviour where ManagerType : MonoBehaviour
{
    /// <summary>
    /// Determines whether the manager is persistent across scenes.
    /// </summary>
    protected abstract bool Persistent { get; }

    protected static ManagerType _instance;

    public static ManagerType Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObject = GameObject.FindWithTag("Manager");
                Debug.Assert(managerObject != null, "Failed to locate Manager GameObject (no GameObject tagged 'Manager' was found)");

                _instance = managerObject.GetComponentInChildren<ManagerType>();
                Debug.AssertFormat(_instance != null, "Failed to locate LocalManager '{0}' component because it does not exist on the Manager GameObject or any of its children", nameof(ManagerType));
            }

            return _instance;
        }

        private set
        {
            Debug.LogError("Attempted to set LocalManager.Instance which is not allowed.");
        }
    }

    public LocalManager()
    {
        Debug.AssertFormat(_instance == null, "A new instance of a singleton manager was created but one already exists - multiple instances of a singleton is not allowed!");
    }

    protected virtual void Start()
    {
        if (_instance == null)
        {
            _instance = GetComponent<ManagerType>();
        }

        if (Persistent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
