using UnityEngine;

/// <summary>
/// A singleton base class for creating preloaded globally accessible MonoBehaviours.
/// Any MonoBehaviours deriving from this class have to be added as a component to
/// either a child of or directly to the PreloadObject prefab located in 'Assets/Resources'
/// or it will generated a runtime debug assertion.
/// </summary>
public abstract class GlobalManager<ManagerType> : MonoBehaviour where ManagerType : MonoBehaviour
{
    private static ManagerType _instance = null;

    /// <summary>
    /// Retrieves an instance of a manager or instantiates one if one does
    /// not already exist in the current scene.
    /// </summary>
    public static ManagerType Instance
    {
        get
        {
            Debug.AssertFormat(
                _instance != null, 
                "Attempted to retrieve a singleton manager instance that has not been loaded. Make sure the '{0}' script has been added to a GameObject in the current scene or the Preload scene if it's meant to be global.",
                nameof(ManagerType)
            );

            return _instance;
        }

        private set
        {
            Debug.Assert(false, "A manager instance should never be explicitly assigned!");
            _instance = value;
        }
    }

    protected virtual void Awake()
    {
        _instance = Preloader.GetPreloadInstance().GetComponentInChildren<ManagerType>();
    }
}
