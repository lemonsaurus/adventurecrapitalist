using UnityEngine;

public class Manager : MonoBehaviour
{
    public int unlockCost;
    public Store store;
    
    public void BuyManager()
    {
        // Unlock the manager and deduct the funds
        store.UnlockManager();
        GameManager.Instance.DeductFunds(unlockCost);    
    }
}
