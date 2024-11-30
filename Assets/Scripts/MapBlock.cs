using UnityEngine;

public class MapBlock : MonoBehaviour {
    public enum Type {
        DirectionUp,
        DirectionDown,
        DirectionLeft,
        DirectionRight,
        Death
    }
    public Type type;
}