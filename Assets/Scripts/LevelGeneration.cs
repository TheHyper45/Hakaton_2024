using UnityEngine;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private string nextLevelName;
    [SerializeField]
    private MapBlock leftBlockPrefab;
    [SerializeField]
    private MapBlock rightBlockPrefab;
    [SerializeField]
    private MapBlock upBlockPrefab;
    [SerializeField]
    private MapBlock downBlockPrefab;
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

    public string NextLevelName() => nextLevelName;
    public int Index() => int.Parse(SceneManager.GetActiveScene().name.Substring(5));

    public MapBlock InstantiateDirectionalBlock(Vector2 pos,Player.Direction dir) => dir switch {
        Player.Direction.Left => Instantiate(leftBlockPrefab,new Vector3(pos.x,pos.y,0.0f),leftBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Right => Instantiate(rightBlockPrefab,new Vector3(pos.x,pos.y,0.0f),rightBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Up => Instantiate(upBlockPrefab,new Vector3(pos.x,pos.y,0.0f),upBlockPrefab.transform.rotation,blocksParent),
        Player.Direction.Down => Instantiate(downBlockPrefab,new Vector3(pos.x,pos.y,0.0f),downBlockPrefab.transform.rotation,blocksParent),
        _ => throw new InvalidEnumArgumentException(nameof(Player.Direction))
    };

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
