using UnityEngine;
using UnityEngine.UI;
    

public class Store : MonoBehaviour
{
    public float _timerTarget;
    public float _baseStoreCost;
    public float _baseStoreProfit;
    public bool _managerUnlocked;
    public float _storeCostMultiplier;
    
    public GameManager gameManager;
    public Text storeCountText;
    public Text storeCostText;
    public Slider progressSlider;
    
    private float _currentTimer;
    private bool _timerRunning;
    private int _storeCount;
    private float _nextStoreCost;
    
    void Start()
    {
        _storeCount = 0;
        _nextStoreCost = _baseStoreCost;
        storeCostText.text = _nextStoreCost.ToString("C2");
    }

    // Update is called once per frame
    void Update()
    {
        if (_timerRunning)
        {
            _currentTimer += Time.deltaTime;
            
            if (_currentTimer >= _timerTarget)
            {
                if (!_managerUnlocked)
                {
                    _timerRunning = false;    
                }
                _currentTimer = 0.0f;
                gameManager.AddFunds(_baseStoreProfit * _storeCount);
            }

            progressSlider.value = _currentTimer / _timerTarget;
        }


    }

    public void BuyStoreOnClick()
    {

        if (_nextStoreCost > gameManager.GetBalance())
        {
            return;
        }

        gameManager.DeductFunds(_nextStoreCost);
        _nextStoreCost = (_baseStoreCost * Mathf.Pow(_storeCostMultiplier, _storeCount));
        _storeCount++;
        
        // Display the new text
        storeCountText.text = _storeCount.ToString();
        storeCostText.text = _nextStoreCost.ToString("C2");
    }

    public void StoreOnClick()
    {
        if (!_timerRunning)
        {
            _timerRunning = true;
        }
    }
}
