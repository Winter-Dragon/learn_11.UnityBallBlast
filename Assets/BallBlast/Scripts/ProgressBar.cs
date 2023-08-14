using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private LevelState levelState;

    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private TMP_Text nextLevelText;

    private void Awake()
    {
        FillBar(0, 1);
        UpdateLevelState();
    }
    public void FillBar(int alredySpawned, int amount)
    {
        progressBar.fillAmount = (float) alredySpawned / amount;
    }

    public void UpdateLevelState()
    {
        currentLevelText.text = levelState.Level.ToString();
        nextLevelText.text = (levelState.Level + 1).ToString();
    }
}
