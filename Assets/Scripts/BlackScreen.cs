using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blackScreen.SetActive(false);
    }
    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            blackScreen.SetActive (true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartGame();
    }
}
