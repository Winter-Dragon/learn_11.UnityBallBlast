using UnityEngine;

public class LevelEdge : MonoBehaviour
{
    public enum EdgeType
    {
        Left,
        Right,
        Bottom
    }

    [SerializeField] private EdgeType type;
    public EdgeType Type => type;
}
