using UnityEngine;

static class TransformExtensions {
    public static Vector2 Position2D(this Transform transform) {
        return new Vector2(transform.position.x,transform.position.y);
    }
}

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float cameraLerpSpeed;

    private new Rigidbody2D rigidbody;

    private readonly Vector2 DirectionUp = new(0.0f,1.0f);
    private readonly Vector2 DirectionDown = new(0.0f,-1.0f);
    private readonly Vector2 DirectionLeft = new(-1.0f,0.0f);
    private readonly Vector2 DirectionRight = new(1.0f,0.0f);

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
        var moveDir = Vector2.zero;
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
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * moveDir + transform.Position2D());
    }
}