using UnityEngine;

public class PanelOpenButton : MonoBehaviour
{
    public GameObject panel;
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
}
