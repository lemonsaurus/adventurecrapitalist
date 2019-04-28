using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Text storeCountText;
    public Text storeCostText;
    public Slider progressSlider;
    public Button buyStoreButton;
    
    private CanvasGroup _canvasGroup;
    private Store _store;
    private bool _timerRunning;
    private bool _storeUnlocked;
    private float _currentTimer;

    private void OnEnable()
    {
        GameManager.OnUpdateBalance += UpdateUI;
    }
    
    private void OnDisable()
    {
        GameManager.OnUpdateBalance -= UpdateUI;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _storeUnlocked = false;
        _store = transform.GetComponent<Store>();
        
        storeCountText.text = _store.StoreCount.ToString();
        storeCostText.text = _store.baseStoreCost.ToString("C2");

        _canvasGroup = transform.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;

        UpdateUI();
    }

    // Update is called once per frame
    private void Update()
    {
        // Update the progress bar
        if (_timerRunning)
        {
            _currentTimer += Time.deltaTime;
            
            if (_currentTimer >= _store.timerTarget)
            {
                if (!_store.managerUnlocked)
                {
                    _timerRunning = false;    
                }
                _currentTimer = 0.0f;
                GameManager.Instance.AddFunds(_store.baseStoreProfit * _store.StoreCount);
            }

            progressSlider.value = _currentTimer / _store.timerTarget;
        }
    }

    public void UpdateUI()
    {
        // Unlock the store if possible
        if (!_storeUnlocked && _store.NextStoreCost <= GameManager.Instance.GetBalance())
        {
            _storeUnlocked = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
        }
        
        // Make the button red if you can't afford to buy any
        if (_storeUnlocked && _store.NextStoreCost > GameManager.Instance.GetBalance())
        {
            buyStoreButton.interactable = false;
        }
        
        // Otherwise, make it white.
        else if (_storeUnlocked && _store.NextStoreCost <= GameManager.Instance.GetBalance())
        {
            buyStoreButton.interactable = true;
        }
    }

    public void BuyStoreOnClick()
    {
        if (_store.NextStoreCost > GameManager.Instance.GetBalance())
        {
            return;
        }
        
        _store.BuyStore();
        
        // Display the new text
        storeCountText.text = _store.StoreCount.ToString();
        storeCostText.text = _store.NextStoreCost.ToString("C2");        
    }
    
    public void UseStoreOnClick()
    {
        if (!_timerRunning && _store.StoreCount > 0)
        {
            _timerRunning = true;
        }
    }

}
