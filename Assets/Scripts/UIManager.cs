using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private enum State
    {
        Game, Managers, Config
    }

    private State _currentState;
    
    public Text cashMoneyText;
    public GameObject managerWindow;
    public GameObject configWindow;
    public GameObject storeGrid;

    private void Start()
    {
        _currentState = State.Game;
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

    public void OnClickManagers()
    {
        if (_currentState == State.Config || _currentState == State.Game)
        {
            _currentState = State.Managers;
            configWindow.SetActive(false);
            storeGrid.SetActive(false);
            managerWindow.SetActive(true);            
        }

        else if (_currentState == State.Managers)
        {
            _currentState = State.Game;
            configWindow.SetActive(false);
            storeGrid.SetActive(true);
            managerWindow.SetActive(false);            
        }
    }
    
    public void OnClickConfig()
    {
        if (_currentState == State.Managers || _currentState == State.Game)
        {
            _currentState = State.Config;
            configWindow.SetActive(true);
            storeGrid.SetActive(false);
            managerWindow.SetActive(false);            
        }

        else if (_currentState == State.Config)
        {
            _currentState = State.Game;
            configWindow.SetActive(false);
            storeGrid.SetActive(true);
            managerWindow.SetActive(false);            
        }
    }
}
