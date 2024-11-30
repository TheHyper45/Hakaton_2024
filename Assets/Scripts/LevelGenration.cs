using UnityEngine;

public class LevelGenration : MonoBehaviour
{
    public GameObject wallPrefab; // Prefab œciany
    public GameObject backgorund; // Prefab œciany
    public int[,] mazeMap = new int[,] // Tablica reprezentuj¹ca labirynt
    {
        { 1, 1, 1, 1, 1 },
        { 1, 2, 2, 2, 1 },
        { 1, 1, 2, 1, 1 },
        { 1, 2, 2, 2, 1 },
        { 1, 1, 1, 1, 1 }
    };
    

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        int rows = mazeMap.GetLength(0);
        int cols = mazeMap.GetLength(1);
        
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (mazeMap[y, x] == 1)
                {
                    Vector3 position = new Vector3(x*0.64f, -y*0.64f, 0); // Pozycja w 2D
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                if(mazeMap[y, x] == 2)
                {
                    print("x");
                    Vector3 position = new Vector3(x * 0.64f, -y * 0.64f, 0); // Pozycja w 2D
                    Instantiate(backgorund, position, Quaternion.identity);
                }
            }
        }
    }
    
}
