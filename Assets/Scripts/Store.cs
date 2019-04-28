using UnityEngine;

public class Store : MonoBehaviour
{
    public bool managerUnlocked;
    public float timerTarget;
    public float baseStoreCost;
    public float baseStoreProfit;
    public float storeCostMultiplier;
    public int storeSpeedupCount;
    public float storeSpeedupMultiplier;
    
    public int StoreCount { get; private set; }
    public float NextStoreCost { get; private set; }

    private void Awake()
    {
        StoreCount = 0;
        NextStoreCost = baseStoreCost;
    }

    public void BuyStore()
    {
        var deductAmount = NextStoreCost;
        NextStoreCost = (baseStoreCost * Mathf.Pow(storeCostMultiplier, StoreCount));
        StoreCount++;
        
        // Check if we should speed up the store
        if (StoreCount % storeSpeedupCount == 0)
        {
            timerTarget /= storeSpeedupMultiplier;
        }
        
        // Trigger the event only after updating everything.
        GameManager.Instance.DeductFunds(deductAmount);
    }
}
