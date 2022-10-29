using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private InputActionReference _moveActionReference;
    [SerializeField] private Transform _charModel;

    private float _cameraRotationOffset; // Offset by the camera rotation so up always remains up.
    private CharacterController _charController;
    private Vector2 _inputVector = Vector2.zero;
    private Animator _animator;

    // To prevent players walking on things.
    private float startY;


    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraRotationOffset = _mainCamera.transform.eulerAngles.y; // We use the rotation in world-space.

        startY = transform.position.y;
        _animator = GetComponentInChildren<Animator>();

        _moveActionReference.action.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        _inputVector = _moveActionReference.action.ReadValue<Vector2>();

        // Direction vector for our movement. We take into account camera rotation to have up always be up as
        // expected while playing. Additionally, we normalize to prevent diagonal movement from being faster.
        var targetVector = Quaternion.Euler(0, _cameraRotationOffset, 0)
                           * new Vector3(_inputVector.x, 0, _inputVector.y)
                               .normalized;

        // Only move when needed
        CharacterMovement(targetVector);
    }

    private void CharacterMovement(Vector3 targetVector)
    {
        var moveDir = transform.TransformDirection(targetVector);
        _charController.Move(Time.deltaTime * moveSpeed * moveDir);

        // Reset the Y position to prevent walking on things.
        var pos = transform.position;
        pos.y = startY;
        transform.position = pos;

        var isWalking = _charController.velocity.magnitude >= 0.5f;

        if (isWalking) 
        {
            var rot = Quaternion.LookRotation(moveDir);
            _charModel.rotation = Quaternion.RotateTowards(_charModel.rotation, rot, rotateSpeed * Time.deltaTime);
        }
        
        _animator.SetBool("isWalking", isWalking);
    }
}