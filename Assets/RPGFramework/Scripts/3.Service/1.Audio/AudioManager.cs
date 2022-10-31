using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : BaseManager<AudioManager>
{
    [SerializeField]
    private AudioSource BackgroundAudioSource;
    [SerializeField]
    private GameObject AudioPlayer;
    /// <summary>
    /// All valid effect audio in scene  
    /// </summary>
    private List<AudioSource> audioPLayList = new List<AudioSource>();

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
    /// Play effect audio
    /// </summary>
    private void UpdateEffetVolume()
    {
        for (int i = audioPLayList.Count - 1; i >= 0; i--)
        {
            if (audioPLayList[i] != null)
            {
                setEffectAudioPlayer(audioPLayList[i]);   
            }
            else
            {
                audioPLayList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Set effect audio player
    /// </summary>
    private void setEffectAudioPlayer(AudioSource audioPlayer, float spatial = -1)
    {
        audioPlayer.mute = IsMuted;
        audioPlayer.volume = EffetVolume * GlobalVolume;
        if(spatial != -1)
        {
            audioPlayer.spatialBlend = spatial;
        }
        if(IsPause)
        {
            audioPlayer.Pause();
        }
        else
        {
            audioPlayer.UnPause();
        }
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

    #region Effect Music

    private Transform audioPlayRoot = null;

    /// <summary>
    /// Get the effect audioSource player
    /// </summary>
    /// <returns></returns>
    private AudioSource GetEffectAudioPlayer(bool is3D = true)
    {
        if(audioPlayRoot == null)
        {
            audioPlayRoot = new GameObject("AudioPlayRoot").transform;
        }
        AudioSource audioSource = PoolManager.Instance.GetGameobject<AudioSource>(AudioPlayer, audioPlayRoot);
        setEffectAudioPlayer(audioSource, is3D?1f:0f);
        audioPLayList.Add(audioSource);
        return audioSource; 
    }

    /// <summary>
    /// Play effect audio on a parent
    /// </summary>
    /// <param name="clip">audioClip</param>
    /// <param name="playPosition">audio play position</param>
    /// <param name="component">Component where audio player attachs</param>
    /// <param name="volume"></param>
    /// <param name="is3D">is audio 3d?</param>
    /// <param name="callBack">Function exectued after audio finish playing</param>
    /// <param name="callBackTime">Delay before trigger Callback function </param>
    public void PlayEffectAudio(AudioClip clip, Component component, float volume = 1, bool is3D = true, UnityAction callBack = null, float callBackTime = 0)
    {
        //Initialize
        AudioSource audioSource = GetEffectAudioPlayer(is3D);
        audioSource.transform.SetParent(component.transform);
        audioSource.transform.localPosition = Vector3.zero;

        //Play audio once 
        audioSource.PlayOneShot(clip, volume);

        //Recycle Audio player and callback
        RecyclAudioPlayer(audioSource, clip, callBack, callBackTime);
    }

    /// <summary>
    /// Play effect audio on a position without parent , EX : in air
    /// </summary>
    /// <param name="clip">audio </param>
    /// <param name="playPosition">audio play position</param>
    /// <param name="volume"></param>
    /// <param name="is3D">is audio 3d?</param>
    /// <param name="callBack">Function exectued after audio finish playing</param>
    /// <param name="callBackTime">Delay before trigger Callback function </param>
    public void PlayEffectAudio(AudioClip clip, Vector3 playPosition,float volume = 1, bool is3D = true, UnityAction callBack = null, float callBackTime = 0)
    {
        //Initialize
        AudioSource audioSource = GetEffectAudioPlayer(is3D);
        audioSource.transform.localPosition = playPosition;

        //Play audio once 
        audioSource.PlayOneShot(clip, volume);

        //Recycle Audio player and callback
        RecyclAudioPlayer(audioSource, clip, callBack, callBackTime);
    }

    /// <summary>
    /// Play effect audio by clip path on a position without parent, EX : in air
    /// </summary>
    /// <param name="clipPath">audio resource path</param>
    /// <param name="playPosition">audio play position</param>
    /// <param name="volume"></param>
    /// <param name="is3D">is audio 3d?</param>
    /// <param name="callBack">Function exectued after audio finish playing</param>
    /// <param name="callBackTime">Delay before trigger Callback function </param>
    public void PlayEffectAudio(string clipPath, Vector3 playPosition, float volume = 1, bool is3D = true, UnityAction callBack = null, float callBackTime = 0)
    {
        AudioClip audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(clipPath);
        if (audioClip != null)
        {
            PlayEffectAudio(audioClip, playPosition, volume, is3D, callBack, callBackTime);
        }
    }

    /// <summary>
    /// Play effect audio by clip path on a parent
    /// </summary>
    /// <param name="clipPath">audio resource path</param>
    /// <param name="parent">audio attachement</param>
    /// <param name="volume"></param>
    /// <param name="is3D">is audio 3d?</param>
    /// <param name="callBack">Function exectued after audio finish playing</param>
    /// <param name="callBackTime">Delay before trigger Callback function </param>
    public void PlayEffectAudio(string clipPath, Component parent, float volume = 1, bool is3D = true, UnityAction callBack = null, float callBackTime = 0)
    {
        AudioClip audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(clipPath);
        if (audioClip != null)
        {
            PlayEffectAudio(audioClip, parent, volume, is3D, callBack, callBackTime);
        }
    }

    /// <summary>
    /// Recycle Audio player
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="clip"></param>
    /// <param name="callBack"></param>
    /// <param name="time"></param>
    private void RecyclAudioPlayer(AudioSource audioSource, AudioClip clip, UnityAction callBack, float Delaytime) 
    {
        StartCoroutine(DoRecycleAudioPlayer(audioSource, clip, callBack, Delaytime));
    }

    /// <summary>
    /// Execute Recyclage
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="clip"></param>
    /// <param name="callBack"></param>
    /// <param name="Delaytime"></param>
    /// <returns></returns>
    private IEnumerator DoRecycleAudioPlayer(AudioSource audioSource, AudioClip clip, UnityAction callBack, float Delaytime)
    {
        //wait for clip finish
        yield return new WaitForSeconds(clip.length);
        //push back to pool
        if(audioSource!=null)
        {
            audioSource.GameObjectPushPool();
            //Invoke CallBack
            yield return new WaitForSeconds(Delaytime);
            callBack?.Invoke();
        }
        
    }
    #endregion
}
