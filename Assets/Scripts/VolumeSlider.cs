using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public MusicButton musicButton;

    void Start()
    {
        if (MusicManager.instance != null)
        {
            slider.SetValueWithoutNotify(MusicManager.instance.musicVolume);
        }

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.SetVolume(value);
            if(musicButton != null)
                musicButton.OnEnable();
        }
    }

    public void SetSliderWithoutNotify(float value)
    {
        slider.SetValueWithoutNotify(value);
    }
}