using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // A simple implementation of the Observer Pattern
    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;
    
    public static GameManager Instance;
    public Store[] allStores;
    public Image[] imagesToPurplify;
    private float _cashMoney = 2.0f;

    void Awake()
    {
        // Singleton design pattern. Ensures only a single
        // instance of this class will ever exist at the 
        // same time.
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public float GetBalance()
    {
        return _cashMoney;
    }

    public void AddFunds(float amount)
    {
        _cashMoney += amount;

        // This ? operator is called null propagation, and means it will only trigger
        // if OnUpdateBalance is not null. This is true only if there are no
        // subscribers to that event.
        OnUpdateBalance?.Invoke();  

    }

    public void DeductFunds(float amount)
    {
        _cashMoney -= amount;
        OnUpdateBalance?.Invoke();
    }

    public void UnlockAllManagersOnClick(Button buttonClicked)
    {
        foreach (var store in allStores)
            store.UnlockManager();

        buttonClicked.interactable = false;

    }

    public void GetRichOnClick(Button buttonClicked)
    {
        AddFunds(10000);
        buttonClicked.interactable = false;
    }

    public void MakeStuffPurpleOnClick()
    {
        foreach (var image in imagesToPurplify)
            image.color = new Color(128, 0, 128);
    }
}
