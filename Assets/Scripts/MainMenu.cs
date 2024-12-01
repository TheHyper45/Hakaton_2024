using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button instructionsButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private InstructionsMenu instructionsMenu;
    [SerializeField]
    private LevelSelectionMenu levelSelectionMenu;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            levelSelectionMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
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
