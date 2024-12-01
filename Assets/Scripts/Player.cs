using UnityEngine;
using System.ComponentModel;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Canvas winCanvas;
    [SerializeField]
    private GameObject shroud;
    [SerializeField]
    private SpriteRenderer mainSprite;
    [SerializeField]
    private SpriteRenderer bloodSprite;

    private enum State { Preparation,Run,Lost,Won }
    private State state = State.Preparation;
    private Vector2 startPosition;
    private Vector2 prevPosition;
    private Vector2 prevGridPosition;
    private new Rigidbody2D rigidbody;
    private AudioSource audioSource;
    private float immobileTimer = 0.0f;
    private float autoRestartTimer = 0.0f;

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
        mainSprite.enabled = true;
        bloodSprite.enabled = false;
        shroud.SetActive(false);
        canvas.gameObject.SetActive(true);
        winCanvas.gameObject.SetActive(false);
        startPosition = transform.Position2D();
        prevPosition = transform.Position2D();
        prevGridPosition = transform.Position2D();
        direction = Direction.Right;
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x,transform.position.x,cameraLerpSpeed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y,transform.position.y,cameraLerpSpeed * Time.deltaTime),
            camera.transform.position.z
        );

        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
        if(state == State.Preparation) {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.mouseScrollDelta.y,5.0f,15.0f);
            if(Input.GetKeyDown(KeyCode.Space)) {
                state = State.Run;
                canvas.gameObject.SetActive(false);
                shroud.SetActive(true);
                camera.orthographicSize = 5.0f;
            }
        }
        else if(state == State.Run) {
            if(Input.GetKeyDown(KeyCode.R)) {
                direction = Direction.Right;
                transform.position = startPosition;
                prevGridPosition = transform.Position2D();
                return;
            }
            var newBlockPosition = MathEx.Floor(transform.Position2D());
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                if(direction == Direction.Left) newBlockPosition.x += 1.0f;
                if(direction == Direction.Down) newBlockPosition.y += 1.0f;
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Up);
            }
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                if(direction == Direction.Left) newBlockPosition.x += 1.0f;
                if(direction == Direction.Down) newBlockPosition.y += 1.0f;
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Down);
            }
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                if(direction == Direction.Left) newBlockPosition.x += 1.0f;
                if(direction == Direction.Down) newBlockPosition.y += 1.0f;
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Left);
            }
            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                if(direction == Direction.Left) newBlockPosition.x += 1.0f;
                if(direction == Direction.Down) newBlockPosition.y += 1.0f;
                levelGeneration.InstantiateDirectionalBlock(newBlockPosition,Direction.Right);
            }
        }
        else if(state == State.Lost) {
            autoRestartTimer += Time.deltaTime;
            if(autoRestartTimer >= 3.0f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if(state == State.Won) {
            winCanvas.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(levelGeneration.NextLevelName());
            }
        }
    }

    private void FixedUpdate() {
        /*if(Mathf.Abs((prevPosition - transform.Position2D()).magnitude) <= 0.0001f) {
            immobileTimer += Time.fixedDeltaTime;
            if(immobileTimer >= 3.0f) {
                Destroy(gameObject);
                return;
            }
        }
        else immobileTimer = 0.0f;
        prevPosition = transform.Position2D();*/
        if(state != State.Run) return;
        if(Mathf.Abs(transform.position.x - prevGridPosition.x) >= 1.0f || Mathf.Abs(transform.position.y - prevGridPosition.y) >= 1.0f) {
            audioSource.volume = Mathf.Clamp01((1.0f / (levelGeneration.exitBlock.transform.position - transform.position).magnitude) * 2.0f);
            var raycastHit = Physics2D.Raycast(RaycastPosition(),DirectionVector(),1.0f,LayerMask.NameToLayer("Default"));
            if(!raycastHit) {
                audioSource.Play();
            }
            prevGridPosition = transform.Position2D();
        }
        rigidbody.MovePosition(speed * Time.fixedDeltaTime * DirectionVector() + transform.Position2D());
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(state != State.Run) return;
        if(!collision.gameObject.TryGetComponent(out MapBlock mapBlock)) return;
        bool hitBlock = false;
        if(direction == Direction.Right && transform.position.x < mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x - 1.0f,transform.position.y,0.0f);
            hitBlock = true;
        }
        else if(direction == Direction.Left && transform.position.x > mapBlock.transform.position.x && MathEx.FloatsAreEqual(transform.position.y,mapBlock.transform.position.y)) {
            transform.position = new Vector3(mapBlock.transform.position.x + 1.0f,transform.position.y,0.0f);
            hitBlock = true;
        }
        else if(direction == Direction.Up && transform.position.y < mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y - 1.0f,0.0f);
            hitBlock = true;
        }
        else if(direction == Direction.Down && transform.position.y > mapBlock.transform.position.y && MathEx.FloatsAreEqual(transform.position.x,mapBlock.transform.position.x)) {
            transform.position = new Vector3(transform.position.x,mapBlock.transform.position.y + 1.0f,0.0f);
            hitBlock = true;
        }
        if(hitBlock) {
            direction = mapBlock.GetBlockDirection();
            mapBlock.audioSource.Play();
            if(mapBlock.GetBlockType() == MapBlock.Type.Death) {
                shroud.SetActive(false);
                state = State.Lost;
                mainSprite.enabled = false;
                bloodSprite.enabled = true;
            }
            else if(mapBlock.GetBlockType() == MapBlock.Type.Exit) {
                shroud.SetActive(false);
                state = State.Won;
                mainSprite.enabled = false;
                winCanvas.gameObject.SetActive(true);
            }
        }
    }
}
