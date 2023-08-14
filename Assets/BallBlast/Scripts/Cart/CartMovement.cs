using UnityEngine;
using UnityEngine.Events;

public class CartMovement : MonoBehaviour
{
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private Bonus bonus;
    [Header("Movement")]
    [SerializeField, Range(5f, 50f)] private float movementSpeed;

    private Vector3 movementTarget;
    private float vehicleWidth = 1.2f;
    private float deltaMovement;
    private float lastPositionX;

    [Header("Wheels")]
    [SerializeField] private Transform[] wheels;
    private float wheelRadius = 0.5f;

    [HideInInspector] public UnityEvent CollisionStone;
    public float MovementSpeed => movementSpeed;
    private void Start()
    {
        movementSpeed = PlayerPrefs.GetFloat("CartMovement:movementSpeed", 5);

        movementTarget = transform.position;
    }

    private void Update()
    {
        Move();

        RotateWheel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin = collision.transform.root.GetComponent<Coin>();

        if (coin != null)
        {
            playerProgress.AddOrRemoveCoins(coin.CoinValue);
            playerProgress.PickUpCoin();
            Destroy(coin.gameObject);
        }

        Stone stone = collision.transform.root.GetComponent<Stone>();

        if (stone != null && bonus.InvulnerabilityBonusIsActive == false)
        {
            CollisionStone.Invoke();
        }

        FreezeBonus freezeBonus = collision.transform.root.GetComponent<FreezeBonus>();

        if (freezeBonus != null)
        {
            freezeBonus.ActiveBonus();
        }

        InvulnerabilityBonus invulnerabilityBonus = collision.transform.root.GetComponent<InvulnerabilityBonus>();

        if (invulnerabilityBonus != null)
        {
            invulnerabilityBonus.ActiveBonus();
        }
    }

    private void Move()
    {
        lastPositionX = transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);

        deltaMovement = transform.position.x - lastPositionX;
    }

    private void RotateWheel()
    {
        float angle = 180f * deltaMovement / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate( new Vector3( 0, 0, -angle ) );
        }
    }

    public void SetMovementTarget(Vector3 target)
    {
        movementTarget = ClampMovementTarget(target);
    }

    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBorder = LevelBoundary.Instance.LeftBorder + (vehicleWidth / 2);
        float rightBorder = LevelBoundary.Instance.RightBorder - (vehicleWidth / 2);

        Vector3 movTarget = target;
        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBorder) movTarget.x = leftBorder;
        if (movTarget.x > rightBorder) movTarget.x = rightBorder;

        return movTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position - new Vector3(vehicleWidth * 0.5f, 0.5f, 0), transform.position + new Vector3(vehicleWidth * 0.5f, -0.5f, 0));
    }
#endif
    public void AddMovementSpeed (float movementSpeed)
    {
        this.movementSpeed += movementSpeed;
        PlayerPrefs.SetFloat("CartMovement:movementSpeed", this.movementSpeed);
    }
}
