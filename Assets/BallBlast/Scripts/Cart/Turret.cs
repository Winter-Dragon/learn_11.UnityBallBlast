using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField, Range(1, 0.1f)] private float fireRate;
    [SerializeField, Range(1, 801)] private int damage;
    [SerializeField, Range(1, 50)] private int projectileAmount;
    [SerializeField, Range(0.01f, 1.0f)] private float critChance;
    [SerializeField, Range(0.05f, 5.0f)] private float critDamage;
    [SerializeField] private float projectileInterval;
    [SerializeField] private Shop shop;

    private int damagePerSecond;
    public int Damage => damage;
    public float FireRate => fireRate;
    public int ProjectileAmount => projectileAmount;
    public float CritChance => critChance;
    public float CritDamage => critDamage;

    private float timer;

    private void Awake()
    {
        damage = PlayerPrefs.GetInt("Turret:damage", 1);
        fireRate = PlayerPrefs.GetFloat("Turret:fireRate", 1);
        projectileAmount = PlayerPrefs.GetInt("Turret:projectileAmount", 1);
        critChance = PlayerPrefs.GetFloat("Turret:critChance", 0.01f);
        critDamage = PlayerPrefs.GetFloat("Turret:critDamage", 0.05f);

        shop.UpdateTextStates();
        shop.UpdateButtonsState();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void Fire()
    {
        if (timer >= fireRate)
        {
            SpawnProjectile();

            timer = 0;
        }
    }

    private void SpawnProjectile()
    {
        float startPosX = shootPoint.position.x - projectileInterval * (projectileAmount - 1) * 0.5f;

        for (int i = 0; i < projectileAmount; i++)
        {
            Projectile projectile = Instantiate(projectilePrefab, new Vector3(startPosX + i * projectileInterval, shootPoint.position.y, shootPoint.position.z), transform.rotation);

            if (UnityEngine.Random.Range(0, 100) < critChance * 100)    // Крит прокнул
            {
                projectile.SetDamage((int)(damage + (critDamage * damage)));
            }
            else                                            // Не прокнул
            {
                projectile.SetDamage(damage);
            }
        }
    }

    public int GetDamagePerSecond()
    {
        damagePerSecond = (int) (damage * projectileAmount * (1 / fireRate) + (damage * critDamage * critChance));
        return damagePerSecond;
    }

    public void AddDamage(int damage)
    {
        this.damage += damage;
        PlayerPrefs.SetInt("Turret:damage", this.damage);
    }
    public void AddFireRate(float fireRate)
    {
        this.fireRate -= fireRate;
        this.fireRate = (float)Math.Round(this.fireRate, 2);
        PlayerPrefs.SetFloat("Turret:fireRate", this.fireRate);
    }
    public void AddProjectileAmount(int projectileAmount)
    {
        this.projectileAmount += projectileAmount;
        PlayerPrefs.SetInt("Turret:projectileAmount", this.projectileAmount);
    }
    public void AddCritChance(float critChance)
    {
        this.critChance += critChance;
        this.critChance = (float)Math.Round(this.critChance, 2);
        PlayerPrefs.SetFloat("Turret:critChance", this.critChance);
    }
    public void AddCritDamage(float critDamage)
    {
        this.critDamage += critDamage;
        this.critDamage = (float)Math.Round(this.critDamage, 2);
        PlayerPrefs.SetFloat("Turret:critDamage", this.critDamage);
    }
}
