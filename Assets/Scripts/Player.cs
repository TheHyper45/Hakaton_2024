using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed;

    private void Update() {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.position += speed * Time.deltaTime * new Vector3(0.0f,1.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.position += speed * Time.deltaTime * new Vector3(0.0f,-1.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += speed * Time.deltaTime * new Vector3(-1.0f,0.0f,0.0f);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.position += speed * Time.deltaTime * new Vector3(1.0f,0.0f,0.0f);
        }
    }
}