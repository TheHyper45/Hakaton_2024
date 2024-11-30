using UnityEngine;

public class MapBlock : MonoBehaviour {
    public enum Type { Directional,Death }
    public Type type;
    public Player.Direction direction;
}
