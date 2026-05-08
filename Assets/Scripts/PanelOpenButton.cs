using UnityEngine;

public class PanelOpenButton : MonoBehaviour
{
    [SerializeField] private PanelShower panel;
    public void ShowPanel()
    {
        panel.Show();
    }
}
