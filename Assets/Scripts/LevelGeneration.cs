using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private int mapWidth;
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private Transform blocksParent;

    private void Awake() {
        {
            int y = mapHeight / 2;
            for(int x = -mapWidth / 2;x <= mapWidth / 2;x += 1) {
                Instantiate(wallPrefab,new Vector3(x * 0.64f,y * 0.64f,0.0f),Quaternion.identity,blocksParent);
                Instantiate(wallPrefab,new Vector3(x * 0.64f,-y * 0.64f,0.0f),Quaternion.identity,blocksParent);
            }
        }
        {
            int x = mapWidth / 2;
            for(int y = -mapHeight / 2;y <= mapHeight / 2;y += 1) {
                Instantiate(wallPrefab,new Vector3(x * 0.64f,y * 0.64f,0.0f),Quaternion.identity,blocksParent);
                Instantiate(wallPrefab,new Vector3(-x * 0.64f,y * 0.64f,0.0f),Quaternion.identity,blocksParent);
            }
        }
    }
}
