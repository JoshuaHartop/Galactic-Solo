using UnityEngine;

/// <summary>
/// Responsible for instantiating the "PreloadObject" prefab before any scene is loaded.
/// </summary>
public class Preloader : ScriptableObject
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Preload()
    {
        GameObject preloadObject = Instantiate(Resources.Load("PreloadObject")) as GameObject;
        DontDestroyOnLoad(preloadObject);

        Debug.Log("Preload complete!");
    }

    public static GameObject GetPreloadInstance()
    {
        GameObject managerObject = GameObject.FindGameObjectWithTag("PreloadInstance");
        Debug.AssertFormat(managerObject != null, "Attempted to retrieve the PreloadObject prefab instance but it could not be found in the scene.");

        return managerObject;
    }
}
