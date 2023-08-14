using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [HideInInspector] public float maxHitPoints;

    [HideInInspector] public UnityEvent hpAreOver;
    [HideInInspector] public UnityEvent changeHitPoints;
    private float hitPoints;
    private bool isDie = false;

    private void Start()
    {
        hitPoints = maxHitPoints;
        changeHitPoints.Invoke();
    }
    public void ApplyDamage(int damage)
    {
        hitPoints -= damage;
        changeHitPoints.Invoke();

        if (hitPoints < 1)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (isDie == true) return;
        hitPoints = 0;
        isDie = true;

        changeHitPoints.Invoke();
        hpAreOver.Invoke();
    }

    public void HealDamage(int heal)
    {
        if (heal + hitPoints >= maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
        else hitPoints += heal;

        changeHitPoints.Invoke();
    }

    public float GetHitPoints()
    {
        return hitPoints;
    }

    public float GetMaxHitPoints()
    {
        return maxHitPoints;
    }

    public float GetNormalizeHitPoints()
    {
        return GetHitPoints() / GetMaxHitPoints();
    }
}
