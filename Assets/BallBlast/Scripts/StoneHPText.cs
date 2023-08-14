using UnityEngine;
using TMPro;

public class StoneHPText : MonoBehaviour
{
    [SerializeField] private TMP_Text hitPointsText;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();

        destructible.changeHitPoints.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        destructible.changeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hitPoints =(int) destructible.GetHitPoints();

        if (hitPoints < 1000)
        {
            hitPointsText.text = hitPoints.ToString();
        }

        if (hitPoints >= 1000)
        {
            hitPointsText.text = hitPoints / 1000 + "K";
        }

        if (hitPoints >= 1000000)
        {
            hitPointsText.text = hitPoints / 1000000 + "M";
        }
    }
}
