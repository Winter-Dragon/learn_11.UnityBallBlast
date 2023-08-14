using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private CartMovement cart;
    [SerializeField] private PlayerProgress playerProgress;
    [Header("Buttons")]
    [SerializeField] private Button damageButton;
    [SerializeField] private Button fireRateButton;
    [SerializeField] private Button multiShotButton;
    [SerializeField] private Button critChanceButton;
    [SerializeField] private Button critDamageButton;
    [SerializeField] private Button moveSpeedButton;
    [Header("Current Text Areas")]
    [SerializeField] private TMP_Text currentDamageText;
    [SerializeField] private TMP_Text currentFireRateText;
    [SerializeField] private TMP_Text currentMultiShotText;
    [SerializeField] private TMP_Text currentCritChanceText;
    [SerializeField] private TMP_Text currentCritDamageText;
    [SerializeField] private TMP_Text currentMoveSpeedText;
    [Header("Next Upgrade Text Areas")]
    [SerializeField] private TMP_Text upgradedDamageText;
    [SerializeField] private TMP_Text upgradedFireRateText;
    [SerializeField] private TMP_Text upgradedMultiShotText;
    [SerializeField] private TMP_Text upgradedCritChanceText;
    [SerializeField] private TMP_Text upgradedCritDamageText;
    [SerializeField] private TMP_Text upgradedMoveSpeedText;
    [Header("Upgrade Count Text Areas")]
    [SerializeField] private TMP_Text upgradeCountDamageText;
    [SerializeField] private TMP_Text upgradeCountFireRateText;
    [SerializeField] private TMP_Text upgradeCountMultiShotText;
    [SerializeField] private TMP_Text upgradeCountCritChanceText;
    [SerializeField] private TMP_Text upgradeCountCritDamageText;
    [SerializeField] private TMP_Text upgradeCountMoveSpeedText;

    private static int maxDamage = 801;
    private static float maxFireRate = 0.1f;
    private static int maxProjectileAmount = 50;
    private static float maxCritChance = 1f;
    private static float maxCritDamage = 5f;
    private static float maxMoveSpeed = 50f;

    private static int damageUpgradeStep = 8;
    private static float fireRateUpgradeStep = 0.01f;
    private static int projectileAmountUpgradeStep = 1;
    private static float critChanceUpgradeStep = 0.01f;
    private static float critDamageUpgradeStep = 0.05f;
    private static float moveSpeedUpgradeStep = 5f;

    private static int damageUpgradePrice = 10;
    private static int fireRateUpgradePrice = 10;
    private static int projectileAmountUpgradePrice = 30;
    private static int critChanceUpgradePrice = 10;
    private static int critDamageUpgradePrice = 10;
    private static int moveSpeedUpgradePrice = 20;

    private int currentDamageUpgradePrice;
    private int currentFireRateUpgradePrice;
    private int currentProjectileAmountUpgradePrice;
    private int currentCritChanceUpgradePrice;
    private int currentCritDamageUpgradePrice;
    private int currentMoveSpeedUpgradePrice;

    private void Awake()
    {
        UpdateTextStates();
        UpdateButtonsState();
        damageButton.onClick.AddListener(ClickDamageButton);
        fireRateButton.onClick.AddListener(ClickFireRateButton);
        multiShotButton.onClick.AddListener(ClickMultiShotButton);
        critChanceButton.onClick.AddListener(ClickCritChanceButton);
        critDamageButton.onClick.AddListener(ClickCritDamageButton);
        moveSpeedButton.onClick.AddListener(ClickMoveSpeedButton);
    }

    private void OnDestroy()
    {
        damageButton.onClick.RemoveListener(ClickDamageButton);
        fireRateButton.onClick.RemoveListener(ClickFireRateButton);
        multiShotButton.onClick.RemoveListener(ClickMultiShotButton);
        critChanceButton.onClick.RemoveListener(ClickCritChanceButton);
        critDamageButton.onClick.RemoveListener(ClickCritDamageButton);
        moveSpeedButton.onClick.RemoveListener(ClickMoveSpeedButton);
    }

    public void UpdateTextStates()
    {
        UpdateCurrentTextStates();
        UpdateUpgradedTextStates();
        UpdateUpgradePriceTextStates();
    }
    private void UpdateCurrentTextStates()
    {
        currentDamageText.text = turret.Damage.ToString();
        currentFireRateText.text = turret.FireRate.ToString();
        currentMultiShotText.text = turret.ProjectileAmount.ToString();
        currentCritChanceText.text = (turret.CritChance * 100) + "%";
        currentCritDamageText.text = (turret.CritDamage * 100 + 100) + "%"; ;
        currentMoveSpeedText.text = cart.MovementSpeed.ToString();
    }
    private void UpdateUpgradedTextStates()
    {
        if (turret.Damage == maxDamage) upgradedDamageText.text = "MAX";        // Damage
        else
        {
            if (turret.Damage + damageUpgradeStep >= maxDamage) upgradedDamageText.text = maxDamage.ToString();
            else upgradedDamageText.text = (turret.Damage + damageUpgradeStep).ToString();
        }

        if (turret.FireRate == maxFireRate) upgradedFireRateText.text = "MAX";      // Fire Rate
        else
        {
            if (turret.FireRate - fireRateUpgradeStep <= maxFireRate) upgradedFireRateText.text = maxFireRate.ToString();
            else upgradedFireRateText.text = Math.Round(turret.FireRate - fireRateUpgradeStep, 2).ToString();
        }

        if (turret.ProjectileAmount == maxProjectileAmount) upgradedMultiShotText.text = "MAX";     // Projectile Amount
        else
        {
            if (turret.ProjectileAmount + projectileAmountUpgradeStep >= maxProjectileAmount) upgradedMultiShotText.text = maxProjectileAmount.ToString();
            else upgradedMultiShotText.text = (turret.ProjectileAmount + projectileAmountUpgradeStep).ToString();
        }

        if (turret.CritChance == maxCritChance) upgradedCritChanceText.text = "MAX";        // Crit Chance
        else
        {
            if (turret.CritChance + critChanceUpgradeStep >= maxCritChance) upgradedCritChanceText.text = (maxCritChance * 100) + "%";
            else upgradedCritChanceText.text = (turret.CritChance + critChanceUpgradeStep) * 100 + "%";
        }

        if (turret.CritDamage == maxCritDamage) upgradedCritDamageText.text = "MAX";        // Crit Damage
        else
        {
            if (turret.CritDamage + critDamageUpgradeStep >= maxCritDamage) upgradedCritDamageText.text = (maxCritDamage * 100 + 100) + "%";
            else upgradedCritDamageText.text = ((turret.CritDamage + critDamageUpgradeStep) * 100 + 100) + "%";
        }

        if (cart.MovementSpeed == maxMoveSpeed) upgradedMoveSpeedText.text = "MAX";        // Movement Speed
        else
        {
            if (cart.MovementSpeed + moveSpeedUpgradeStep >= maxMoveSpeed) upgradedMoveSpeedText.text = maxMoveSpeed.ToString();
            else upgradedMoveSpeedText.text = (cart.MovementSpeed + moveSpeedUpgradeStep).ToString();
        }
    }
    private void UpdateUpgradePriceTextStates()
    {
        UpdateUpgradePrice();

        upgradeCountDamageText.text = currentDamageUpgradePrice.ToString();
        upgradeCountFireRateText.text = currentFireRateUpgradePrice.ToString();
        upgradeCountMultiShotText.text = currentProjectileAmountUpgradePrice.ToString();
        upgradeCountCritChanceText.text = currentCritChanceUpgradePrice.ToString();
        upgradeCountCritDamageText.text = currentCritDamageUpgradePrice.ToString();
        upgradeCountMoveSpeedText.text = currentMoveSpeedUpgradePrice.ToString();

        if (turret.Damage == maxDamage) upgradeCountDamageText.text = "-";
        if (turret.FireRate == maxFireRate) upgradeCountFireRateText.text = "-";
        if (turret.ProjectileAmount == maxProjectileAmount) upgradeCountMultiShotText.text = "-";
        if (turret.CritChance == maxCritChance) upgradeCountCritChanceText.text = "-";
        if (turret.CritDamage == maxCritDamage) upgradeCountCritDamageText.text = "-";
        if (cart.MovementSpeed == maxMoveSpeed) upgradeCountMoveSpeedText.text = "-";
    }
    private void UpdateUpgradePrice()
    {
        currentDamageUpgradePrice = ((turret.Damage - 1) / damageUpgradeStep + 1) * damageUpgradePrice;
        currentFireRateUpgradePrice = (int)((1 - turret.FireRate) / fireRateUpgradeStep + 1) * fireRateUpgradePrice;
        currentProjectileAmountUpgradePrice = (turret.ProjectileAmount / projectileAmountUpgradeStep) * projectileAmountUpgradePrice;
        currentCritChanceUpgradePrice = (int)((turret.CritChance / critChanceUpgradeStep) + 1) * critChanceUpgradePrice;
        currentCritDamageUpgradePrice = (int)(turret.CritDamage / critDamageUpgradeStep) * critDamageUpgradePrice;
        currentMoveSpeedUpgradePrice = (int)(cart.MovementSpeed / moveSpeedUpgradeStep) * moveSpeedUpgradePrice;
    }
    public void UpdateButtonsState()
    {
        damageButton.interactable = true;
        fireRateButton.interactable = true;
        multiShotButton.interactable = true;
        critChanceButton.interactable = true;
        critDamageButton.interactable = true;
        moveSpeedButton.interactable = true;

        if (playerProgress.Coins < currentDamageUpgradePrice || turret.Damage == maxDamage) damageButton.interactable = false;
        if (playerProgress.Coins < currentFireRateUpgradePrice || turret.FireRate == maxFireRate) fireRateButton.interactable = false;
        if (playerProgress.Coins < currentProjectileAmountUpgradePrice || turret.ProjectileAmount == maxProjectileAmount) multiShotButton.interactable = false;
        if (playerProgress.Coins < currentCritChanceUpgradePrice || turret.CritChance == maxCritChance) critChanceButton.interactable = false;
        if (playerProgress.Coins < currentCritDamageUpgradePrice || turret.CritDamage == maxCritDamage) critDamageButton.interactable = false;
        if (playerProgress.Coins < currentMoveSpeedUpgradePrice || cart.MovementSpeed == maxMoveSpeed) moveSpeedButton.interactable = false;
    }
    public void ClickDamageButton()
    {
        playerProgress.AddOrRemoveCoins(-currentDamageUpgradePrice);
        turret.AddDamage(damageUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
    private void ClickFireRateButton()
    {
        playerProgress.AddOrRemoveCoins(-currentFireRateUpgradePrice);
        turret.AddFireRate(fireRateUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
    private void ClickMultiShotButton()
    {
        playerProgress.AddOrRemoveCoins(-currentProjectileAmountUpgradePrice);
        turret.AddProjectileAmount(projectileAmountUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
    private void ClickCritChanceButton()
    {
        playerProgress.AddOrRemoveCoins(-currentCritChanceUpgradePrice);
        turret.AddCritChance(critChanceUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
    private void ClickCritDamageButton()
    {
        playerProgress.AddOrRemoveCoins(-currentCritDamageUpgradePrice);
        turret.AddCritDamage(critDamageUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
    private void ClickMoveSpeedButton()
    {
        playerProgress.AddOrRemoveCoins(-currentMoveSpeedUpgradePrice);
        cart.AddMovementSpeed(moveSpeedUpgradeStep);
        UpdateTextStates();
        UpdateButtonsState();
    }
}
