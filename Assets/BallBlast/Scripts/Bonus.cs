using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float freezeBonusActiveTimer;
    [SerializeField] private float invulnerabilityBonusActiveTimer;
    [SerializeField] private CartMovement player;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Color playerSpriteColor;

    private float timer;
    private Stone[] stones;
    private bool freezeBonusIsActive = false;
    private bool invulnerabilityBonusIsActive = false;
    public bool InvulnerabilityBonusIsActive => invulnerabilityBonusIsActive;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= freezeBonusActiveTimer && freezeBonusIsActive == true)
        {
            stones = FindObjectsOfType<Stone>();

            for (int i = 0; i < stones.Length; i++)
            {
                StoneMovement stoneMovement = stones[i].GetComponent<StoneMovement>();
                stoneMovement.enabled = true;
            }

            timer = 0;
            freezeBonusIsActive = false;
            if (invulnerabilityBonusIsActive == true)
            {
                playerSprite.color = new Color(255, 255, 255, 255);
                invulnerabilityBonusIsActive = false;
            }
            enabled = false;
        }

        if (timer >= invulnerabilityBonusActiveTimer && invulnerabilityBonusIsActive == true)
        {
            playerSprite.color = new Color(255, 255, 255, 255);

            timer = 0;
            invulnerabilityBonusIsActive = false;
            enabled = false;
        }
    }

    public void FreezeBonus()
    {
        stones = FindObjectsOfType<Stone>();

        for (int i = 0; i < stones.Length; i++)
        {
            StoneMovement stoneMovement = stones[i].GetComponent<StoneMovement>();
            stoneMovement.enabled = false;
        }

        timer = 0;
        enabled = true;
        freezeBonusIsActive = true;
    }

    public void InvulnerabilityBonus()
    {
        playerSprite.color = playerSpriteColor;

        timer = 0;
        enabled = true;
        invulnerabilityBonusIsActive = true;
    }

    public void DeleteAllBonuses()
    {
        InvulnerabilityBonus[] invulnerabilityBonuses = FindObjectsOfType<InvulnerabilityBonus>();

        if (invulnerabilityBonuses != null)
        {
            for (int i = 0; i < invulnerabilityBonuses.Length; i++)
            {
                Destroy(invulnerabilityBonuses[i].gameObject);
            }
        }

        FreezeBonus[] freezeBonuses = FindObjectsOfType<FreezeBonus>();

        if (freezeBonuses != null)
        {
            for (int i = 0; i < freezeBonuses.Length; i++)
            {
                Destroy(freezeBonuses[i].gameObject);
            }
        }
    }
}
