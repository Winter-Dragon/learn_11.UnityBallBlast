using UnityEngine;

public class FreezeBonus : MonoBehaviour
{
    [SerializeField] private float fallSpeed;

    private Bonus bonus;
    private bool bonusIsDrop = false;

    private void Awake()
    {
        bonus = FindObjectOfType<Bonus>();
    }

    private void Update()
    {
        if (bonusIsDrop == true) return;

        if (transform.position.y > -4)
        {
            transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
            bonusIsDrop = true;
        }
    }

    public void ActiveBonus()
    {
        bonus.FreezeBonus();
        Destroy(gameObject);
    }
}
