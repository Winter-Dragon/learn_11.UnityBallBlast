using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private UnityEvent CoinPickUp;
    [SerializeField] private TMP_Text coinsText;

    private int coins;
    public int Coins => coins;

    private void Awake()
    {
        coins = PlayerPrefs.GetInt("PlayerProgress:coins", 0);
        UpdateCoinsText();
    }

    public void AddOrRemoveCoins(int coins)
    {
        this.coins += coins;
        UpdateCoinsText();
        PlayerPrefs.SetInt("PlayerProgress:coins", this.coins);
    }

    public void PickUpCoin()
    {
        UpdateCoinsText();
        CoinPickUp.Invoke();
        PlayerPrefs.SetInt("PlayerProgress:coins", coins);
    }

    public void UpdateCoinsText()
    {
        if (coins < 1000) coinsText.text = coins.ToString();
        if (coins > 1000 && coins < 1000000) coinsText.text = coins / 1000 + "K";
        if (coins > 1000000) coinsText.text = coins / 1000000 + "M";
    }
}
