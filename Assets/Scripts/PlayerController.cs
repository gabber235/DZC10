using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private Camera mainCamera;

    private float _cameraRotationOffset; // Offset by the camera rotation so up always remains up.
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start() {
        _cameraRotationOffset = mainCamera.transform.eulerAngles.y; // We use the rotation in world-space.
        _playerTransform = transform;
    }

    // Update is called once per frame
    void Update() {
        // Direction vector for our movement. We take into account camera rotation to have up always be up as
        // expected while playing. Additionally, we normalize to prevent diagonal movement from being faster.
        Vector3 targetVector = Quaternion.Euler(0, _cameraRotationOffset, 0)
                               * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))
                                   .normalized;

        // Only move when needed, also prevents rotating back to default as soon as we let go of the movement keys.
        if (targetVector.magnitude != 0) {
            CharacterMovement(targetVector);
        }
    }

    private void CharacterMovement(Vector3 targetVector) {
        Vector3 targetPosition = moveSpeed * Time.deltaTime * targetVector + _playerTransform.position;
        _playerTransform.position = targetPosition;
        
        // Have player rotate to the direction they are walking
        _playerTransform.rotation = Quaternion.RotateTowards(
            _playerTransform.rotation, 
            Quaternion.LookRotation(targetVector), 
            rotateSpeed);
    }
}