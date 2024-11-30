using UnityEngine;
using System.ComponentModel;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private MapBlock leftBlockPrefab;
    [SerializeField]
    private MapBlock rightBlockPrefab;
    [SerializeField]
    private MapBlock upBlockPrefab;
    [SerializeField]
    private MapBlock downBlockPrefab;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private Transform blocksParent;

    public MapBlock InstantiateDirectionalBlock(Vector2 pos,Player.Direction dir) => dir switch {
        Player.Direction.Left => Instantiate(leftBlockPrefab,new Vector3(pos.x,pos.y,0.0f),leftBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Right => Instantiate(rightBlockPrefab,new Vector3(pos.x,pos.y,0.0f),rightBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Up => Instantiate(upBlockPrefab,new Vector3(pos.x,pos.y,0.0f),upBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Down => Instantiate(downBlockPrefab,new Vector3(pos.x,pos.y,0.0f),downBlockPrefab.transform.rotation,blocksParent),
        _ => throw new InvalidEnumArgumentException(nameof(Player.Direction))
    };

    private void Awake() {
        {
            int y = mapHeight / 2;
            for(int x = -mapWidth / 2;x <= mapWidth / 2;x += 1) {
                InstantiateDirectionalBlock(new Vector2(x,y),Player.Direction.Right);
                InstantiateDirectionalBlock(new Vector2(x,-y),Player.Direction.Left);
            }
        }
        {
            int x = mapWidth / 2;
            for(int y = -mapHeight / 2;y <= mapHeight / 2;y += 1) {
                InstantiateDirectionalBlock(new Vector2(x,y),Player.Direction.Down);
                InstantiateDirectionalBlock(new Vector2(-x,y),Player.Direction.Up);
            }
        }
        {
            InstantiateDirectionalBlock(new Vector2(1.0f,1.0f),Player.Direction.Right);
            InstantiateDirectionalBlock(new Vector2(5.0f,0.0f),Player.Direction.Down);
            InstantiateDirectionalBlock(new Vector2(4.0f,-5.0f),Player.Direction.Left);
            InstantiateDirectionalBlock(new Vector2(0.0f,-4.0f),Player.Direction.Up);
        }
    }
}
