using UnityEngine;
using UnityEngine.Events;

public class LevelState : MonoBehaviour
{
    [SerializeField] private CartMovement cart;
    [SerializeField] private StoneSpawner spawner;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private Bonus bonus;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private int level = 1;
    private float timer;

    public int Level => level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("LevelState:level", 1);
        ChangeBalanceSettings();
        progressBar.UpdateLevelState();

        cart.CollisionStone.AddListener(OnCartCollisionStone);
        spawner.Completed.AddListener(OnSpawnCompleted);
        enabled = false;
    }

    private void OnDestroy()
    {
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
        spawner.Completed.RemoveListener(OnSpawnCompleted);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1)
        {
            if (FindObjectsOfType<Stone>().Length == 0)
            {
                CollectAllCoins();
                playerProgress.PickUpCoin();
                Passed.Invoke();
                spawner.UpdateSpawner();
                level++;
                PlayerPrefs.SetInt("LevelState:level", Level);
                ChangeBalanceSettings();
                progressBar.UpdateLevelState();
                bonus.DeleteAllBonuses();
                enabled = false;
            }

            timer = 0;
        }
    }

    private void OnCartCollisionStone()
    {
        Defeat.Invoke();
        spawner.UpdateSpawner();
        enabled = false;
        spawner.enabled = false;

        Stone[] stones = FindObjectsOfType<Stone>();
        for (int i = 0; i < stones.Length; i++)
        {
            Destroy(stones[i].gameObject);
        }

        Coin[] coins = FindObjectsOfType<Coin>();
        for (int i = 0; i < coins.Length; i++)
        {
            Destroy(coins[i].gameObject);
        }

        bonus.DeleteAllBonuses();
    }

    private void OnSpawnCompleted()
    {
        enabled = true;
    }

    private void CollectAllCoins()
    {
        Coin[] coins = FindObjectsOfType<Coin>();

        for (int i = 0; i < coins.Length; i++)
        {
            playerProgress.AddOrRemoveCoins(coins[i].CoinValue);
            Destroy(coins[i].gameObject);
        }
    }

    private void ChangeBalanceSettings()
    {
        if (level < 5)
        {
            spawner.ChangeSpawnRate(10);
            spawner.ChangeAmount(8);
            spawner.ChangeMinHitpointsPercentage(0.5f);
            spawner.ChangeMaxHitpointsRate(1f);
        }

        if (level > 5 && level < 11)
        {
            spawner.ChangeSpawnRate(8);
            spawner.ChangeAmount(10);
            spawner.ChangeMinHitpointsPercentage(0.7f);
            spawner.ChangeMaxHitpointsRate(1.5f);
        }

        if (level > 10 && level < 21)
        {
            spawner.ChangeAmount(12);
            spawner.ChangeMinHitpointsPercentage(1f);
            spawner.ChangeMaxHitpointsRate(2f);
        }

        if (level > 20 && level < 31)
        {
            spawner.ChangeAmount(15);
            spawner.ChangeMaxHitpointsRate(3f);
        }

        if (level > 30 && level < 31)
        {
            spawner.ChangeAmount(20);
            spawner.ChangeMaxHitpointsRate(4f);
        }

        if (level > 40 && level < 51)
        {
            spawner.ChangeAmount(25);
            spawner.ChangeMaxHitpointsRate(5f);
        }

        if (level > 50)
        {
            spawner.ChangeAmount(30);
        }

        if (level % 10 == 0)
        {
            spawner.ChangeSpawnRate(5);
            spawner.ChangeAmount(15);
            spawner.ChangeMinHitpointsPercentage(0.1f);
            spawner.ChangeMaxHitpointsRate(1f);
        }

        if (level % 10 == 5)
        {
            spawner.ChangeSpawnRate(20);
            spawner.ChangeAmount(3);
            spawner.ChangeMinHitpointsPercentage(1f);
            spawner.ChangeMaxHitpointsRate(5f);
        }
    }
}
