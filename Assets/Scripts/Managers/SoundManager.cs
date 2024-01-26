using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Handles setting the global sound volume.
/// </summary>
public class SoundManager : GlobalManager<SoundManager>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _volume;

    protected void Start()
    {
        Volume = _volume;
    }

    /// <summary>
    /// Set or get the global sound volume.
    /// The volume must be between 0.0 and 1.0.
    /// </summary>
    public float Volume 
    {
        set {
            Debug.AssertFormat(value >= 0f && value <= 1f, "Volume must be a value between 0.0 and 1.0!");

            AudioListener.volume = value;
        }

        get {
            return AudioListener.volume;
        }
    }
}
