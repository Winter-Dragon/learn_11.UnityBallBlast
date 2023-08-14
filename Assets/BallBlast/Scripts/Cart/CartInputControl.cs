using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [SerializeField] private CartMovement cartMovement;
    [SerializeField] private Turret turret;

    private void Start()
    {
        enabled = false;
    }
    private void Update()
    {
        cartMovement.SetMovementTarget( Camera.main.ScreenToWorldPoint( Input.mousePosition ) );

        if (Input.GetMouseButton(0) == true)
        {
            turret.Fire();
        }
    }
}
