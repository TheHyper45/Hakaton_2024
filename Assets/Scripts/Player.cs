using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float cameraLerpSpeed;

    private new Rigidbody2D rigidbody;

    private readonly Vector3 DirectionUp = new(0.0f,1.0f,0.0f);
    private readonly Vector3 DirectionDown = new(0.0f,-1.0f,0.0f);
    private readonly Vector3 DirectionLeft = new(-1.0f,0.0f,0.0f);
    private readonly Vector3 DirectionRight = new(1.0f,0.0f,0.0f);

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x,transform.position.x,cameraLerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y,transform.position.y,cameraLerpSpeed * Time.deltaTime),
            camera.transform.position.z
        );
    }

    private void FixedUpdate() {
        Vector3 moveDir = Vector3.zero;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            moveDir += DirectionUp;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            moveDir += DirectionDown;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveDir += DirectionLeft;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveDir += DirectionRight;
        }
        if(moveDir.magnitude > 1.0f) {
            moveDir.Normalize();
        }
        Rigidbody2D.SlideMovement slide = new() { gravity = Vector2.zero };
        rigidbody.Slide(speed * moveDir,Time.fixedDeltaTime,slide);
    }
}