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
            var blockDir = (direction == Direction.Up || direction == Direction.Down) ? Direction.Left : Direction.Up;
            levelGeneration.InstantiateDirectionalBlock(newBlockPosition,blockDir);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            var blockDir = (direction == Direction.Up || direction == Direction.Down) ? Direction.Right : Direction.Down;
            levelGeneration.InstantiateDirectionalBlock(newBlockPosition,blockDir);
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out MapBlock mapBlock)) {
            return;
        }
        if(mapBlock.GetBlockType() == MapBlock.Type.Death) {
            Destroy(gameObject);
            return;
        }
        if(direction == Direction.Right && transform.position.x < mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x - 1.0f,transform.position.y,0.0f);
            direction = mapBlock.GetBlockDirection();
        }
        else if(direction == Direction.Left && transform.position.x > mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x + 1.0f,transform.position.y,0.0f);
            direction = mapBlock.GetBlockDirection();
        }
        else if(direction == Direction.Up && transform.position.y < mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y - 1.0f,0.0f);
            direction = mapBlock.GetBlockDirection();
        }
        else if(direction == Direction.Down && transform.position.y > mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y + 1.0f,0.0f);
            direction = mapBlock.GetBlockDirection();
        }
    }
}
