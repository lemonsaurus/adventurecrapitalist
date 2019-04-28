using UnityEngine;

public class GameManager : MonoBehaviour
{
    // A simple implementation of the Observer Pattern
    public delegate void UpdateBalance();
    public static event UpdateBalance OnUpdateBalance;
    
    public static GameManager Instance;
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
}
