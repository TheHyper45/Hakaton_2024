using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private MapBlock wallPrefab;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private Transform blocksParent;

    private void InstantiateDirectionalBlock(Vector2 pos,Player.Direction dir) {
        var block = Instantiate(wallPrefab,new Vector3(pos.x,pos.y,0.0f),Quaternion.identity,blocksParent);
        block.type = MapBlock.Type.Directional;
        block.direction = dir;
    }

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
            InstantiateDirectionalBlock(new Vector2(0.0f,1.0f),Player.Direction.Right);
            InstantiateDirectionalBlock(new Vector2(5.0f,0.0f),Player.Direction.Down);
            InstantiateDirectionalBlock(new Vector2(4.0f,-5.0f),Player.Direction.Left);
            InstantiateDirectionalBlock(new Vector2(0.0f,-5.0f),Player.Direction.Up);
        }
    }
}
