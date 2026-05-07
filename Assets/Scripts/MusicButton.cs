using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public GameObject musicOffIcon;
    public VolumeSlider volumeSlider;
    public void OnEnable()
    {
        UpdateIcon();
    }
    public void Clicked()
    {
        MusicManager.instance.ToggleMusic();
        if(volumeSlider != null)
        {
            if (MusicManager.instance.IsMusicOn())
                volumeSlider.SetSliderWithoutNotify(MusicManager.instance.musicVolume);
            else
                volumeSlider.SetSliderWithoutNotify(0f);
        }
        UpdateIcon();
    }
    void UpdateIcon()
    {
        if (MusicManager.instance == null) 
            return;

        bool musicOn = MusicManager.instance.IsMusicOn();
        musicOffIcon.SetActive(!musicOn);
    }
}