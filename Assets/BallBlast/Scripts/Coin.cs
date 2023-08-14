using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float fallSpeed;

    private Animator animator;
    private bool coinIsDrop = false;
    private int coinValue;
    public int CoinValue => coinValue;
    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        animator.speed = 0;
    }

    private void Update()
    {
        if (coinIsDrop == true) return;

        if (transform.position.y > -4)
        {
            transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
            animator.speed = 1;
            coinIsDrop = true;
        }
    }

    public void SetValue(int value)
    {
        coinValue = value;
    }
}
