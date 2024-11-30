using UnityEngine;
using System.ComponentModel;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [SerializeField]
    private LevelGeneration levelGeneration;
    [SerializeField]
    private float speed;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float cameraLerpSpeed;

    private new Rigidbody2D rigidbody;

    public enum Direction { Up,Down,Left,Right }
    private Direction direction;

    private Vector2 DirectionVector() => direction switch {
        Direction.Up => new Vector2(0.0f,1.0f),
        Direction.Down => new Vector2(0.0f,-1.0f),
        Direction.Left => new Vector2(-1.0f,0.0f),
        Direction.Right => new Vector2(1.0f,0.0f),
        _ => throw new InvalidEnumArgumentException(nameof(Direction))
    };

    private void Awake() {
        direction = Direction.Right;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        var newBlockPosition = MathEx.Floor(transform.Position2D() - DirectionVector());
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if(direction == Direction.Up || direction == Direction.Down) {
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Left);
            }
            else {
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Up);
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            if(direction == Direction.Up || direction == Direction.Down) {
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Right);
            }
            else {
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Down);
            }
        }
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x,transform.position.x,cameraLerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y,transform.position.y,cameraLerpSpeed * Time.deltaTime),
            camera.transform.position.z
        );
    }

    private void FixedUpdate() {
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * DirectionVector() + transform.Position2D());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.TryGetComponent(out MapBlock mapBlock)) {
            if(mapBlock.GetBlockType() == MapBlock.Type.Death) {
                Destroy(gameObject);
            }
            else if(mapBlock.GetBlockType() == MapBlock.Type.Directional) {
                direction = mapBlock.GetBlockDirection();
            }
        }
    }
}
