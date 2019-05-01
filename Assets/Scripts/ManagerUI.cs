using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public Text managerCostText;
    public Button buyManagerButton;
    
    private CanvasGroup _canvasGroup;
    private Manager _manager;

    private bool _managerUnlocked;
    private bool _managerPurchased;

    private void Awake()
    {
        GameManager.OnUpdateBalance += UpdateUI;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _managerUnlocked = false;
        _managerPurchased = false;
        _manager = transform.GetComponent<Manager>();
        
        managerCostText.text = _manager.unlockCost.ToString("C2");

        _canvasGroup = transform.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;

        UpdateUI();
    }

    public void UpdateUI()
    {
 
        // Unlock the manager if possible
        if (!_managerUnlocked && !_managerPurchased && _manager.unlockCost <= GameManager.Instance.GetBalance() && _manager.store.StoreCount > 0)
        {
            _managerUnlocked = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
        }
        
        // Make the button red if you can't afford it
        if (_managerUnlocked && !_managerPurchased && _manager.unlockCost > GameManager.Instance.GetBalance())
        {
            buyManagerButton.interactable = false;
        }
        
        // Otherwise, make it white.
        else if (_managerUnlocked && !_managerPurchased && _manager.unlockCost <= GameManager.Instance.GetBalance())
        {
            buyManagerButton.interactable = true;
        }
    }

    public void BuyManagerOnClick()
    {
        // If we don't have sufficient funds, we return.
        if (GameManager.Instance.GetBalance() < _manager.unlockCost)
            return;
        
        _manager.BuyManager();
        _managerPurchased = true;
        buyManagerButton.interactable = false;
    }
}
