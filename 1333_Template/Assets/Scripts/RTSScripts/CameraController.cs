using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Cinemachine Target")]
    [SerializeField] private CinemachineCamera _cinemachineCam;
    [SerializeField] private GameObject _cameraTarget;

    [Header("Movement Settings")]
    [SerializeField] private float _panSpeed = 15f;
    private Vector3 _movementInput;
    private bool _isKeyboardPanning = false;

    [Header("Zoom Settings (DOTween)")]
    [SerializeField] private float _minFOV = 20f;               // Max zoom in
    [SerializeField] private float _maxFOV = 60f;               // Max zoom out
    [SerializeField] private float _zoomStepSize = 8f;  
    [SerializeField] private float _zoomDuration = 0.3f; 
    [SerializeField] private Ease _zoomEase = Ease.OutCubic;    // Added ease for smooth scroll movement

    private float _targetFOV;

    public bool IsMovementLocked { get; set; } = false;

    private void Start()
    {
        // Initialize target destination to match current camera view at start
        if (_cinemachineCam != null)
        {
            _targetFOV = _cinemachineCam.Lens.FieldOfView;
        }
    }

    private void Update()
    {
        if (IsMovementLocked) return;

        MoveCamera();
    }

    private void MoveCamera()
    {
        // Smoothly translate target object that the Cinemachine camera follows
        _cameraTarget.transform.Translate(_movementInput * Time.deltaTime * _panSpeed, Space.World);
    }

    // --- INPUT SYSTEM CALLBACKS ---

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed) _isKeyboardPanning = true;
        else if (context.canceled) _isKeyboardPanning = false;

        Vector2 inputVector = context.ReadValue<Vector2>();
        _movementInput.x = inputVector.x;
        _movementInput.z = inputVector.y; 
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (_isKeyboardPanning) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Screen checks using percentage widths
        if (mousePosition.x > (Screen.width * 0.95f)) _movementInput.x = 1f;
        else if (mousePosition.x < (Screen.width * 0.05f)) _movementInput.x = -1f;
        else _movementInput.x = 0f;

        if (mousePosition.y > (Screen.height * 0.95f)) _movementInput.z = 1f;
        else if (mousePosition.y < (Screen.height * 0.05f)) _movementInput.z = -1f;
        else _movementInput.z = 0f;
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (IsMovementLocked) return;

        if (context.performed)
        {
            Vector2 scrollValue = context.ReadValue<Vector2>();

            // Calculate next FOV step
            if (scrollValue.y > 0) _targetFOV -= _zoomStepSize;     
            else if (scrollValue.y < 0) _targetFOV += _zoomStepSize; 

            _targetFOV = Mathf.Clamp(_targetFOV, _minFOV, _maxFOV);
            DOTween.Kill(_cinemachineCam);

            // Smoothly glide lens FOV to the target step
            DOTween.To(() => _cinemachineCam.Lens.FieldOfView,
                       x => _cinemachineCam.Lens.FieldOfView = x,
                       _targetFOV,
                       _zoomDuration)
                   .SetEase(_zoomEase)
                   .SetUpdate(true); 
        }
    }
}