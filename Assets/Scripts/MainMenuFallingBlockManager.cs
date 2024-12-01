using UnityEngine;
using System.Collections.Generic;

public class MainMenuFallingBlockManager : MonoBehaviour {
    [SerializeField]
    private List<MainMenuFallingBlock> fallingBlockPrefabs;
    [SerializeField]
    private Transform fallingBlackSpawnParent;
    private List<Transform> fallingBlockSpawns = new();
    [SerializeField]
    private float spawnTime;
    private float timer;

    private void Awake() {
        timer = spawnTime;
        for(int i = 0;i < fallingBlackSpawnParent.childCount;i += 1) {
            fallingBlockSpawns.Add(fallingBlackSpawnParent.GetChild(i));
        }
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer > spawnTime) {
            var spawn = fallingBlockSpawns.RandomElement();
            Instantiate(fallingBlockPrefabs.RandomElement(),spawn.position,Quaternion.identity);
            timer = 0.0f;
        }
    }
}
