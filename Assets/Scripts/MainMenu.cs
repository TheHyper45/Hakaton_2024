using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button infoButton;
    [SerializeField]
    private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Game");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}