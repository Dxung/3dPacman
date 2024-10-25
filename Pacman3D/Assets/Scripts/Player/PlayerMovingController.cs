using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovingController : MonoBehaviour
{
    [Header("Can Player Move?")]
    public bool _canMove { get; private set; } = true;

    [Header("Movement Parameters")]
    [SerializeField] private float _constantSpeed = 2f;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _gravity = 9.8f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float _lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80.0f;
    private float _rotationX = 0;

    [Header("Player State")]
    private PlayerStateController _playerStateController;

    [Header("Other References")]
    private Camera _playerCamera;
    private CharacterController _myCharacterController;

    [Header("Input")]
    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    [Header("Starting Position")]
    [SerializeField] private Vector3 _startingposition;

    private void Awake()
    {
        _playerStateController = this.gameObject.GetComponentInChildren<PlayerStateController>();
        _playerCamera = GetComponentInChildren<Camera>();
        _myCharacterController = GetComponent<CharacterController>();

        /*--- Lock and hide mouse outside screen when playing ---*/
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        ReSpawnPacman();
    }

    private void Update()
    {
          SpeedControl();
            if (_canMove)
            {
                HandleMovementInput();
                HandleMouseLook();
                ApplyFinalMovement();
            }

    }

    private void HandleMovementInput()
    { 
        Vector2 tempToNormalize = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        _currentInput = tempToNormalize.normalized * _walkSpeed;
        float moveDirectionY = _moveDirection.y;       
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x) + (transform.TransformDirection(Vector3.right) * _currentInput.y);
        _moveDirection.y = moveDirectionY; //reset player height after moving

    }

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _lookSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_lowerLookLimit, _upperLookLimit);        
   
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0); //rotation of the camera will be relative to rotation of player

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeedX, 0);//rotate player when mouse rotate
    }

    private void ApplyFinalMovement()
    {
        /*--- if player is not on the ground, use gravity to slowly pull player down ---*/
        if (!_myCharacterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }
        _myCharacterController.Move(_moveDirection * Time.deltaTime);

    }

    /*--- Speed ---*/
    private void SpeedControl()
    {
        if (_playerStateController.IsitThatState("normal"))
        {
            SpeedChange(_constantSpeed);
        }
        else if (_playerStateController.IsitThatState("consume"))
        {
            SpeedChange(0.9f * _constantSpeed);
        }
        else if (_playerStateController.IsitThatState("powerUp"))
        {
            SpeedChange(1.5f * _constantSpeed);
        }
        else if (_playerStateController.IsitThatState("dead"))
        {
            SpeedChange(0);
        }
    }

    private void SpeedChange(float speed)
    {
        _walkSpeed = speed;
    }

    public void ReSpawnPacman()
    {
        _canMove = false;
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = _startingposition;
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        _canMove = true;
        SpeedChange(_constantSpeed);
        _playerStateController.ReSpawn();
    }
}
