using UnityEngine;
using System.ComponentModel;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour {
    [SerializeField]
    private LevelGeneration levelGeneration;
    [SerializeField]
    private float speed;
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float cameraLerpSpeed;

    private Vector2 prevPosition;
    private Vector2 prevGridPosition;
    private new Rigidbody2D rigidbody;
    private AudioSource audioSource;
    private float immobileTimer = 0.0f;

    public enum Direction { Up,Down,Left,Right }
    private Direction direction;

    private Vector2 DirectionVector() => direction switch {
        Direction.Up => new Vector2(0.0f,1.0f),
        Direction.Down => new Vector2(0.0f,-1.0f),
        Direction.Left => new Vector2(-1.0f,0.0f),
        Direction.Right => new Vector2(1.0f,0.0f),
        _ => throw new InvalidEnumArgumentException(nameof(Direction))
    };
    private Vector2 RaycastPosition() => direction switch {
        Direction.Up => new Vector2(transform.position.x,transform.position.y),
        Direction.Down => new Vector2(transform.position.x,transform.position.y - 1.0f),
        Direction.Left => new Vector2(transform.position.x,transform.position.y),
        Direction.Right => new Vector2(transform.position.x + 1.0f,transform.position.y),
        _ => throw new InvalidEnumArgumentException(nameof(Direction))
    };

    private void Awake() {
        prevPosition = transform.Position2D();
        prevGridPosition = transform.Position2D();
        direction = Direction.Right;
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            speed = 10;
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            direction = Direction.Right;
            transform.position = Vector3.zero;
            prevGridPosition = transform.Position2D();
            return;
        }
        var newBlockPosition = MathEx.Floor(transform.Position2D());
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            var blockDir = (direction == Direction.Up || direction == Direction.Down) ? Direction.Left : Direction.Up;
            if(direction == Direction.Left) newBlockPosition.x += 1.0f;
            if(direction == Direction.Down) newBlockPosition.y += 1.0f;
            levelGeneration.InstantiateDirectionalBlock(newBlockPosition,blockDir);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1)) {
            var blockDir = (direction == Direction.Up || direction == Direction.Down) ? Direction.Right : Direction.Down;
            if(direction == Direction.Left) newBlockPosition.x += 1.0f;
            if(direction == Direction.Down) newBlockPosition.y += 1.0f;
            levelGeneration.InstantiateDirectionalBlock(newBlockPosition,blockDir);
        }
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x,transform.position.x,cameraLerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y,transform.position.y,cameraLerpSpeed * Time.deltaTime),
            camera.transform.position.z
        );
    }

    private void FixedUpdate() {
        if(Mathf.Abs((prevPosition - transform.Position2D()).magnitude) <= 0.0001f) {
            immobileTimer += Time.fixedDeltaTime;
            if(immobileTimer >= 3.0f) {
                Destroy(gameObject);
                return;
            }
        }
        else immobileTimer = 0.0f;
        prevPosition = transform.Position2D();

        if(Mathf.Abs(transform.position.x - prevGridPosition.x) >= 1.0f || Mathf.Abs(transform.position.y - prevGridPosition.y) >= 1.0f) {
            audioSource.volume = Mathf.Clamp01((1.0f / (levelGeneration.ExitBlock.transform.position - transform.position).magnitude) * 2.0f);
            var raycastHit = Physics2D.Raycast(RaycastPosition(),DirectionVector(),1.0f,LayerMask.NameToLayer("Default"));
            if(!raycastHit) {
                audioSource.Play();
            }
            prevGridPosition = transform.Position2D();
        }
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * DirectionVector() + transform.Position2D());
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(!collision.gameObject.TryGetComponent(out MapBlock mapBlock)) {
            return;
        }
        if(mapBlock.GetBlockType() == MapBlock.Type.Death) {
            mapBlock.audioSource.Play();
            Destroy(gameObject);
            return;
        }
        if(mapBlock.GetBlockType() == MapBlock.Type.Exit) {
            mapBlock.audioSource.Play();
            Destroy(gameObject);
            return;
        }
        if(direction == Direction.Right && transform.position.x < mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x - 1.0f,transform.position.y,0.0f);
            direction = mapBlock.GetBlockDirection();
            mapBlock.audioSource.Play();
        }
        else if(direction == Direction.Left && transform.position.x > mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x + 1.0f,transform.position.y,0.0f);
            direction = mapBlock.GetBlockDirection();
            mapBlock.audioSource.Play();
        }
        else if(direction == Direction.Up && transform.position.y < mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y - 1.0f,0.0f);
            direction = mapBlock.GetBlockDirection();
            mapBlock.audioSource.Play();
        }
        else if(direction == Direction.Down && transform.position.y > mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y + 1.0f,0.0f);
            direction = mapBlock.GetBlockDirection();
            mapBlock.audioSource.Play();
        }
    }
}
