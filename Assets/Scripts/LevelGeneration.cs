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
    //[SerializeField]
    //private MapBlock exitBlockPrefab;
    [SerializeField]
    private MapBlock deathBlockPrefab;
    public MapBlock exitBlock;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private Transform blocksParent;
    //public MapBlock ExitBlock { get; private set; }

    public MapBlock InstantiateDirectionalBlock(Vector2 pos,Player.Direction dir) => dir switch {
        Player.Direction.Left => Instantiate(leftBlockPrefab,new Vector3(pos.x,pos.y,0.0f),leftBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Right => Instantiate(rightBlockPrefab,new Vector3(pos.x,pos.y,0.0f),rightBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Up => Instantiate(upBlockPrefab,new Vector3(pos.x,pos.y,0.0f),upBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Down => Instantiate(downBlockPrefab,new Vector3(pos.x,pos.y,0.0f),downBlockPrefab.transform.rotation,blocksParent),
        _ => throw new InvalidEnumArgumentException(nameof(Player.Direction))
    };

    //public MapBlock InstantiateExitBlock(Vector2 pos) => Instantiate(exitBlockPrefab,new Vector3(pos.x,pos.y,0.0f),exitBlockPrefab.transform.rotation,blocksParent);
    public MapBlock InstantiateDeathBlock(Vector2 pos) => Instantiate(deathBlockPrefab,new Vector3(pos.x,pos.y,0.0f),deathBlockPrefab.transform.rotation,blocksParent);

    private void Awake() {
        {
            int y = mapHeight / 2;
            for(int x = -mapWidth / 2;x <= mapWidth / 2;x += 1) {
                InstantiateDeathBlock(new Vector2(x,y));
                InstantiateDeathBlock(new Vector2(x,-y));
            }
        }
        {
            int x = mapWidth / 2;
            for(int y = -mapHeight / 2;y <= mapHeight / 2;y += 1) {
                InstantiateDeathBlock(new Vector2(x,y));
                InstantiateDeathBlock(new Vector2(-x,y));
            }
        }
        /*{
            InstantiateDirectionalBlock(new Vector2(1.0f,1.0f),Player.Direction.Right);
            InstantiateDirectionalBlock(new Vector2(10.0f,0.0f),Player.Direction.Down);
            InstantiateDirectionalBlock(new Vector2(9.0f,-10.0f),Player.Direction.Left);
            InstantiateDirectionalBlock(new Vector2(0.0f,-9.0f),Player.Direction.Up);
        }
        ExitBlock = InstantiateExitBlock(new Vector2(2.0f,8.0f));*/
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        {
            int y = mapHeight / 2;
            for(int x = -mapWidth / 2;x <= mapWidth / 2;x += 1) {
                Gizmos.DrawCube(new(x,y),new(1.0f,1.0f));
                Gizmos.DrawCube(new(x,-y),new(1.0f,1.0f));
            }
        }
        {
            int x = mapWidth / 2;
            for(int y = -mapHeight / 2;y <= mapHeight / 2;y += 1) {
                Gizmos.DrawCube(new(x,y),new(1.0f,1.0f));
                Gizmos.DrawCube(new(-x,y),new(1.0f,1.0f));
            }
        }
    }
}
