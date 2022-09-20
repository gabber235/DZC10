using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    // [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputActionReference _moveActionReference;
    
    private float _cameraRotationOffset; // Offset by the camera rotation so up always remains up.
    private Transform _playerTransform;
    private Vector2 _inputVector = Vector2.zero;
    private CharacterController _charController;


    void Awake() {
        _charController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start() {
        _cameraRotationOffset = mainCamera.transform.eulerAngles.y; // We use the rotation in world-space.
        _playerTransform = transform;
        
        _moveActionReference.action.Enable();
    }
    
    // Update is called once per frame
    void Update() {

        _inputVector =  _moveActionReference.action.ReadValue<Vector2>();

        // Direction vector for our movement. We take into account camera rotation to have up always be up as
        // expected while playing. Additionally, we normalize to prevent diagonal movement from being faster.
        Vector3 targetVector = Quaternion.Euler(0, _cameraRotationOffset, 0)
                               * new Vector3(_inputVector.x, 0, _inputVector.y)
                                   .normalized;

        // Only move when needed
        if (targetVector.magnitude != 0) {
            CharacterMovement(targetVector);
        }
    }

    private void CharacterMovement(Vector3 targetVector) {
        Vector3 targetPosition = moveSpeed * Time.deltaTime * targetVector + _playerTransform.position;

        Vector3 moveDir = transform.TransformDirection(targetVector);
        _charController.Move(Time.deltaTime * moveSpeed * moveDir);

    }
}