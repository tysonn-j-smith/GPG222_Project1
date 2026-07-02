using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private GameObject cameraPrefab;

    [Range(0.1f, 100f)]
    [SerializeField] private float camXSens = 15f;
    [Range(0.1f, 100f)]
    [SerializeField] private float camYSens = 15f;

    private Vector2 lookInput;

    private Camera cam;

    private float rotationX = 0f;

    private void Awake()
    {
        GameObject camObj = Instantiate(cameraPrefab, cameraPivot.transform);
        cam = camObj.GetComponent<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if(cam == null)
        {
            return;
        }

        HandleCamera();
    }

    public void SetInput(Vector2 input)
    {
        lookInput = input;
    }

    private void HandleCamera()
    {
        float camX = lookInput.x * camXSens * Time.deltaTime;
        float camY = lookInput.y * camYSens * Time.deltaTime;

        rotationX -= camY;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f);

        cameraPivot.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * camX);
    }
}
