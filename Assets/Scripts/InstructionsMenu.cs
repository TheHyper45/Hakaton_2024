using UnityEngine;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour {
    [SerializeField]
    private Button goBackButton;
    [SerializeField]
    private MainMenu mainMenu;

    private void Awake() {
        goBackButton.onClick.AddListener(() => {
            mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
