using UnityEngine;

public class MainMenuFallingBlock : MonoBehaviour {
    [SerializeField]
    private float fallSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float lifetime;
    private float timer = 0.0f;

    private void Update() {
        transform.position -= new Vector3(0.0f,fallSpeed * Time.deltaTime,0.0f);
        transform.rotation = Quaternion.Euler(0.0f,0.0f,timer * rotationSpeed);
        timer += Time.deltaTime;
        if(timer >= lifetime) {
            Destroy(gameObject);
        }
    }
}
