using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioClip mainMusic;
    //public AudioClip winMusic;
    //public AudioClip loseMusic;
    public float musicVolume = 1f;

    public bool musicOn = true;
    private float lastVolume = 1f;

    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        lastVolume = musicVolume;
        musicSource.volume = musicOn ? musicVolume : 0f;
        effectSource.volume = musicOn ? musicVolume : 0f;
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        if(!musicOn)
        {
            lastVolume = musicVolume;
            musicSource.volume = 0f;
            effectSource.volume = 0f;
        }
        else
        {
            musicVolume = lastVolume;
            musicSource.volume = musicVolume;
            effectSource.volume = musicVolume;
        }
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsMusicOn()
    {
        return musicOn;
    }

    // public void PlayWin()
    // {
    //     musicSource.Stop();
    //     if (musicOn)
    //         effectSource.PlayOneShot(winMusic);
    // }

    // public void PlayLose()
    // {
    //     musicSource.Stop();
    //     if (musicOn)
    //         effectSource.PlayOneShot(loseMusic);
    // }

    public void PlayMainMusic()
    {
        effectSource.Stop();
        musicSource.clip = mainMusic;
        musicSource.loop = true;
        if (musicOn)
            musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        musicVolume = volume;
        if(volume > 0f)
        {
            musicOn = true;
            lastVolume = volume;
        }
        else
        {
            musicOn = false;
        }
        musicSource.volume = volume;
        effectSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}