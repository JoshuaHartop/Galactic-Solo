using System;
using Unity.VisualScripting;
using UnityEngine;

public class Manager<ManagerType> : MonoBehaviour where ManagerType : MonoBehaviour
{
    /// <summary>
    /// Retrieves an instance of a manager or instantiates one if one does
    /// not already exist in the current scene.
    /// </summary>
    public static ManagerType Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject managerObject = GameObject.FindGameObjectWithTag("Manager");

                if (managerObject == null)
                {
                    managerObject = new GameObject
                    {
                        tag = "Manager",
                        name = "-- Game Manager --"
                    };
                }

                ManagerType managerComponent;

                if (!managerObject.TryGetComponent(out managerComponent))
                {
                    managerComponent = managerObject.AddComponent<ManagerType>();
                }

                _instance = managerComponent;
            }

            return _instance;
        }

        private set
        {
            Debug.Assert(false, "A manager instance should never be explicitly assigned!");
            _instance = value;
        }
    }

    private static ManagerType _instance = null;

}
