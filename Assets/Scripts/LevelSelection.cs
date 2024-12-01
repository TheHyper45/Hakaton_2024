using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button backButton;

    void Start()
    {
        level1Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level1");
        });
        level2Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level2");
        });
        level3Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level3");
        });
        backButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
