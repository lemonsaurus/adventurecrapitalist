using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float _cashMoney;
    public Text cashMoneyText;

    void Start()
    {
        _cashMoney = 2.0f;
        cashMoneyText.text = _cashMoney.ToString("C2");
    }

    public float GetBalance()
    {
        return _cashMoney;
    }

    public void AddFunds(float amount)
    {
        _cashMoney += amount;
        cashMoneyText.text = _cashMoney.ToString("C2");
    }

    public void DeductFunds(float amount)
    {
        _cashMoney -= amount;
        cashMoneyText.text = _cashMoney.ToString("C2");
    }
}
