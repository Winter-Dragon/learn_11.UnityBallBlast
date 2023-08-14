using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    [Header("Spawn")]
    [SerializeField] private Stone stonePrefab;
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] private Turret turret;

    [Header("Balance")]
    [SerializeField] private float spawnRate;
    [SerializeField] private int amount;
    [SerializeField, Range(0.0f, 1.0f)] private float minHitpointsPercentage;
    [SerializeField] private float maxHitpointsRate;
    [SerializeField] private StonePallete stonePallete;

    [HideInInspector] public UnityEvent Completed;

    private float timer;
    private int spawnAmount;
    private int DPS;
    private int stoneMaxHitpoints;
    private int stoneMinHitpoints;
    private float offsetZAxis;
    public float OffsetZAxis => offsetZAxis;

    private void Start()
    {
        UpdateSpawner();

        offsetZAxis = 0;

        stonePallete = GetComponent<StonePallete>();

        enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Spawn();
            progressBar.FillBar(spawnAmount, amount);
            timer = 0;
        }

        if (spawnAmount >= amount)
        {
            enabled = false;

            Completed.Invoke();
        }
    }

    private void Spawn()
    {
        Stone stone = Instantiate(stonePrefab, spawnpoints[Random.Range(0, spawnpoints.Length)].position, Quaternion.identity);
        stone.SetSize((Stone.Size) Random.Range(1, 4));
        stone.maxHitPoints = Random.Range(stoneMinHitpoints, stoneMaxHitpoints + 1);
        stone.transform.position += new Vector3(0, 0, offsetZAxis);

        SpriteRenderer stoneSprite = stone.transform.GetChild(0).GetComponent<SpriteRenderer>();
        stoneSprite.color = stonePallete.GetRandomColor();

        AddZAxisOffset();
        spawnAmount++;
    }

    public void AddZAxisOffset()
    {
        offsetZAxis += 0.001f;
        if (offsetZAxis >= 1) offsetZAxis = 0;
    }

    public void UpdateSpawner()
    {
        DPS = turret.GetDamagePerSecond();
        stoneMaxHitpoints = (int)(maxHitpointsRate * DPS);
        stoneMinHitpoints = (int)(stoneMaxHitpoints * minHitpointsPercentage);

        spawnAmount = 0;
        timer = spawnRate;
    }

    public void ChangeSpawnRate(float spawnRate)
    {
        this.spawnRate = spawnRate;
    }
    public void ChangeAmount(int amount)
    {
        this.amount = amount;
    }
    public void ChangeMinHitpointsPercentage(float minHitpointsPercentage)
    {
        this.minHitpointsPercentage = minHitpointsPercentage;
    }
    public void ChangeMaxHitpointsRate(float maxHitpointsRate)
    {
        this.maxHitpointsRate = maxHitpointsRate;
    }
}
