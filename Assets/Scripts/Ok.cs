using UnityEngine;

public class Ok : MonoBehaviour
{
    public GameObject HintPanel;

    void Start()
    {
        UIState.IsUIOpen = true;
    }

    public void Ok_Clicked()
    {
        HintPanel.SetActive(false);
        Invoke(nameof(UnblockInput), 0.3f);
    }

    void UnblockInput()
    {
        UIState.IsUIOpen = false;
    }
}