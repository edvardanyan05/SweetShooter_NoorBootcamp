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
            int levelNum = SceneManager.GetActiveScene().buildIndex;
            char num = (char)('0' + levelNum);
            string str = "Level" + levelNum;
            SceneTransition.Instance.LoadScene(str);
            
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
            SceneTransition.Instance.LoadScene("Menu");;
            
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
            int levelNum = SceneManager.GetActiveScene().buildIndex + 1;
            char num = (char)('0' + levelNum);
            string str = "Level" + levelNum;
            SceneTransition.Instance.LoadScene(str);
            
            if (AudioAgain && MusicManager.instance != null)
            {
                MusicManager.instance.PlayMainMusic();
            }
        });
    }
}
