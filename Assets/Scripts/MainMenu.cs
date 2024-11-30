using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button infoButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private MainMenuFallingBlock fallingBlockPrefab;
    [SerializeField]
    private Transform fallingBlackSpawnParent;
    private List<Transform> fallingBlockSpawns = new();
    [SerializeField]
    private float spawnTime;
    private float timer;

    private void Awake() {
        timer = spawnTime;
        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Game");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
        for(int i = 0;i < fallingBlackSpawnParent.childCount;i += 1) {
            fallingBlockSpawns.Add(fallingBlackSpawnParent.GetChild(i));
        }
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer > spawnTime) {
            var spawn = fallingBlockSpawns.RandomElement();
            Instantiate(fallingBlockPrefab,spawn.position,Quaternion.identity);
            timer = 0.0f;
        }
    }
}