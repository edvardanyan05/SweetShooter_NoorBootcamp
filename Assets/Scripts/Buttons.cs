using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public PanelShower SourcePanel;
    public bool AudioAgain;

    public void Retry()
    {
        Time.timeScale = 1f;

        SourcePanel.Hide(() => 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            if (AudioAgain && MusicManager.instance != null)
            {
                MusicManager.instance.PlayMainMusic();
            }
        });
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SourcePanel.Hide(() => 
        {
            SceneManager.LoadScene(0);
            
            if (AudioAgain && MusicManager.instance != null)
            {
                MusicManager.instance.PlayMainMusic();
            }
        });
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SourcePanel.Hide(() => 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
            if (AudioAgain && MusicManager.instance != null)
            {
                MusicManager.instance.PlayMainMusic();
            }
        });
    }
}
