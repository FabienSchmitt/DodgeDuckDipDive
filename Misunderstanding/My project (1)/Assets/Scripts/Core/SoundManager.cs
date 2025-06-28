using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private float maxMusicVolume = 0.3f;

    [SerializeField]
    private float maxSoundEffectsVolume = 1f;

    private AudioSource soundEffectSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            // first time creating this object, we can create the singleton and keep it alive forever
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != null && this != Instance)
        {
            // new object created by Unity on scene reload, we do not want nor need it
            Destroy(gameObject);
        }
        soundEffectSource = GetComponent<AudioSource>();
        musicSource = GetComponentsInChildren<AudioSource>().Where(go => go.gameObject != this.gameObject).FirstOrDefault();
        // force retrieval of player prefs with no volume change during init
        ChangeMusicVolume(0);
        ChangeSoundEffectVolume(0);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if(audioClip != null)
            soundEffectSource.PlayOneShot(audioClip);
    }

    public bool IsAudioSourcePlaying()
    {
        return soundEffectSource.isPlaying;
    }

    public void ChangeSoundEffectVolume(float volumeChange)
    {
        ChangeAudioSourceVolume(soundEffectSource, PlayerPrefsConstants.SoundEffectVolume, 1f, volumeChange);
    }

    public void ChangeMusicVolume(float volumeChange)
    {
        ChangeAudioSourceVolume(musicSource, PlayerPrefsConstants.MusicVolume, maxMusicVolume, volumeChange);
    }

    private void ChangeAudioSourceVolume(AudioSource audioSource, string persistedFieldName, float normalizedVolume, float volumeChange)
    {
        float currentVolume = PlayerPrefs.GetFloat(persistedFieldName, maxSoundEffectsVolume);
        currentVolume += volumeChange;

        // check volume is in range of min/max
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        // save the current volume but NOT the normalized one, since it will always be normalized afterwards
        PlayerPrefs.SetFloat(persistedFieldName, currentVolume);

        float finalVolume = currentVolume * normalizedVolume;
        audioSource.volume = finalVolume;

    }
}
