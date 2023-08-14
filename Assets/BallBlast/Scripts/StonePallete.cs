using UnityEngine;

public class StonePallete : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    public Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }
}
