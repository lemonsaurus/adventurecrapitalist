using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public Text cashMoneyText;

    private void Start()
    {
        UpdateUI();
    }

    private void OnEnable()
    {
        // This adds UpdateUI as a subscriber to 
        // this event. That means it will be called
        // whenever the event is invoked.
        GameManager.OnUpdateBalance += UpdateUI;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event.
        GameManager.OnUpdateBalance -= UpdateUI;
    }

    private void UpdateUI()
    {
        cashMoneyText.text = GameManager.Instance.GetBalance().ToString("C2");
    }
}
