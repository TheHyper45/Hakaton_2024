using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button instructionsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private InstructionsMenu instructionsMenu;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("LevelsList");
        });
        instructionsButton.onClick.AddListener(() => {
            instructionsMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
