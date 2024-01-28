using UnityEngine;

class VolumeSaveData : SaveData<VolumeSaveData>
{
    public float volume;

    public VolumeSaveData()
    {
        volume = 1f;
    }
}

/// <summary>
/// Handles setting the global sound volume.
/// </summary>
public class SoundManager : GlobalManager<SoundManager>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float _volume;

    private AudioSource _audioSource;

    private VolumeSaveData _volumeSaveData;

    protected void Start()
    {
        _volumeSaveData = VolumeSaveData.Load();

        Volume = _volumeSaveData.volume;

        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        _volumeSaveData.Save();
    }

    /// <summary>
    /// Set or get the global sound volume.
    /// The volume must be between 0.0 and 1.0.
    /// </summary>
    public float Volume 
    {
        set {
            Debug.AssertFormat(value >= 0f && value <= 1f, "Volume must be a value between 0.0 and 1.0!");

            _volumeSaveData.volume = value;
            AudioListener.volume = value;

            _volumeSaveData.Save();
        }

        get {
            return AudioListener.volume;
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // _audioSource.Stop();
        _audioSource.PlayOneShot(clip, volume);
    }
}
