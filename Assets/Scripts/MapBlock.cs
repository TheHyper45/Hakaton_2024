using UnityEngine;

public class MapBlock : MonoBehaviour {
    public enum Type { Directional,Death }
    [SerializeField]
    private Type type;
    [SerializeField]
    private Player.Direction direction;
    public Type GetBlockType() => type;
    public Player.Direction GetBlockDirection() => direction;
}
