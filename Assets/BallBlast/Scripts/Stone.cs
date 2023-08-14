using UnityEngine;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Destructible
{
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private InvulnerabilityBonus invulnerabilityBonusPrefab;
    [SerializeField] private FreezeBonus freezeBonusPrefab;
    public enum Size
    {
        Small,
        Normal,
        Big,
        Huge
    }

    [SerializeField] private Size size;
    [SerializeField] private float spawnUpForce;


    private StoneSpawner stoneSpawner;
    private StoneMovement stoneMovement;

    private void Awake()
    {
        stoneMovement = GetComponent<StoneMovement>();
        stoneSpawner = FindObjectOfType<StoneSpawner>();

        hpAreOver.AddListener(OnStoneDestroyed);

        SetSize(size);
    }

    private void OnDestroy()
    {
        hpAreOver.RemoveListener(OnStoneDestroyed);
    }

    private void OnStoneDestroyed()
    {
        if (size != Size.Small)
        {
            SpawnStones();
        }

        int value = (int) (maxHitPoints / 100);
        if (value < 1) value = 1;
        SpawnCoin(value);

        if (Random.Range(0, 40) == 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                FreezeBonus freezeBonus = Instantiate(freezeBonusPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                InvulnerabilityBonus invulnerabilityBonus = Instantiate(invulnerabilityBonusPrefab, transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }

    private void SpawnStones()
    {
        for (int i = 0; i < 2; i++)
        {
            Stone stone = Instantiate(this, transform.position, Quaternion.identity);
            stone.SetSize(size - 1);
            stone.maxHitPoints = Mathf.Clamp(maxHitPoints / 2, 1, maxHitPoints);
            stone.stoneMovement.AddVerticalVelocity(spawnUpForce);
            stone.stoneMovement.SetHorizontalDirection((i % 2 * 2) - 1);

            stone.transform.position += new Vector3(0, 0, stoneSpawner.OffsetZAxis);
            stoneSpawner.AddZAxisOffset();
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0) return;

        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    private Vector3 GetVectorFromSize(Size size)
    {
        if (size == Size.Huge) return new Vector3(1, 1, 1);
        if (size == Size.Big) return new Vector3(0.75f, 0.75f, 0.75f);
        if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

        return Vector3.one;
    }

    private void SpawnCoin(int coinValue)
    {
        Coin coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        coin.SetValue(coinValue);
    }
}
