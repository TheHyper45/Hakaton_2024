using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour {
    [SerializeField]
    private Button level1Button;
    [SerializeField]
    private Button level2Button;
    [SerializeField]
    private Button level3Button;
    [SerializeField]
    private Button level4Button;
    [SerializeField]
    private Button goBackButton;
    [SerializeField]
    private MainMenu mainMenu;

    void Start() {
        level1Button.onClick.AddListener(() => {
            SceneManager.LoadScene("Level1");
        });
        level2Button.onClick.AddListener(() => {
            SceneManager.LoadScene("Level2");
        });
        level3Button.onClick.AddListener(() => {
            SceneManager.LoadScene("Level3");
        });
        level4Button.onClick.AddListener(() => {
            SceneManager.LoadScene("Level4");
        });
        goBackButton.onClick.AddListener(() => {
            mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
