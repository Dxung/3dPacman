using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovingController : MonoBehaviour
{
    [Header("Can Player Move?")]
    private bool _canMove  = true;

    [Header("Movement Parameters")]
    [SerializeField] private float _constantSpeed = 2.0f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _walkSpeed;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float _lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80.0f;
    private float _rotationX = 0; //Chac chan rang camera nhin thang phia truoc va khong bi xoay dot ngot khi bat dau phien van choi

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
        if (_playerStateController.GetCurrentState() == PlayerState.normal)
        {
            ChangeSpeed(_constantSpeed);
        }
        else if (_playerStateController.GetCurrentState() == PlayerState.consume)
        {
            ChangeSpeed(0.9f * _constantSpeed);
        }
        else if (_playerStateController.GetCurrentState() == PlayerState.powerUp)
        {
            ChangeSpeed(1.5f * _constantSpeed);
        }
        else if (_playerStateController.GetCurrentState() == PlayerState.dead)
        {
            ChangeSpeed(0);
        }
    }

    private void ChangeSpeed(float speed)
    {
        _walkSpeed = speed;
    }

    public void ReSpawnPacman()
    {
        //lock all input until respawn complete
        _canMove = false;
        this.gameObject.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = _startingposition;
        this.gameObject.GetComponent<CharacterController>().enabled = true;
        _canMove = true;

        //change to original speed
        ChangeSpeed(_constantSpeed);

        //change to original state
        _playerStateController.ReSpawn();
    }
}
