using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField]
    private AudioSource BackgroundAudioSource;

    public override void Init()
    {
        base.Init();
        UpdateGlobalVolume();
    }

    #region Volume Control
    [SerializeField]
    [Range(0,1)]
    [OnValueChanged("UpdateGlobalVolume")]
    private float _globalVolume;

    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateBackgroundVolume")]
    private float _backgroundVolume;

    [SerializeField]
    [Range(0, 1)]
    [OnValueChanged("UpdateEffetVolume")]
    private float _effetVolume;

    [SerializeField]
    [OnValueChanged("UpdateIsMuted")]
    private bool _isMuted;

    [SerializeField]
    [OnValueChanged("UpdateIsLoop")]
    private bool _isLoop;

    [SerializeField]
    [OnValueChanged("UpdateIsPause")]
    private bool _isPause;

    public float GlobalVolume { get => _globalVolume; set {  _globalVolume = value; } }
    public float BackgroundVolume { get => _backgroundVolume; set => _globalVolume = value; }
    public float EffetVolume { get => _effetVolume; set => _globalVolume = value; }
    public bool IsMuted { get => _isMuted; set { if (_isMuted == value) return; _isMuted = value; UpdateIsMuted(); } }
    public bool IsLoop { get => _isLoop; set { if (_isLoop == value) return; _isLoop = value; UpdateIsLoop(); } }
    public bool IsPause { get => _isPause; set { if (_isPause == value) return; _isPause = value; UpdateIsPause(); } }


    /// <summary>
    /// Update global volume
    /// </summary>
    private void UpdateGlobalVolume()
    {
        UpdateBackgroundVolume();
        UpdateEffetVolume();
    }

    /// <summary>
    /// Update background volume
    /// </summary>
    private void UpdateBackgroundVolume()
    {
        BackgroundAudioSource.volume = BackgroundVolume * GlobalVolume;
    }

    /// <summary>
    /// Update effect volume
    /// </summary>
    private void UpdateEffetVolume()
    {
        BackgroundAudioSource.volume = BackgroundVolume * GlobalVolume;
    }

    private void UpdateIsMuted()
    {
        BackgroundAudioSource.mute = IsMuted;
    }

    private void UpdateIsLoop()
    {
        BackgroundAudioSource.loop = IsLoop;
    }
    private void UpdateIsPause()
    {
        if(IsPause)
        {
            BackgroundAudioSource.Pause();
        }
        else
        {
            BackgroundAudioSource.UnPause();
        }

        UpdateEffetVolume();
    }

    #endregion

    #region Background Music
    /// <summary>
    /// Play an Clip which we have a clip object
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    /// <param name="volume"></param>

    public void PlayBackgroundAudio(AudioClip clip, bool loop = true, float volume = -1)
    {
        BackgroundAudioSource.clip = clip;
        IsLoop = loop;
        if(volume != -1)
        {
            BackgroundVolume = volume;
        }
        BackgroundAudioSource.Play();
    }

    /// <summary>
    /// Play an Clip which we have a clip path
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="loop"></param>
    /// <param name="volume"></param>
    public void PlayBackgroundAudio(string clipPath, bool loop = true, float volume = -1)
    {
        AudioClip clip = ResourceManager.Instance.LoadAsset<AudioClip>(clipPath);
        PlayBackgroundAudio(clip, loop, volume);
    }

    #endregion
}
