using UnityEngine;
using Unity.Netcode;

public class PlayerCamera : NetworkBehaviour
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

    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            return;
        }

        GameObject camObj = Instantiate(cameraPrefab, cameraPivot.transform);
        cam = camObj.GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if(cam == null || !IsOwner)
        {
            return;
        }

        HandleCamera();
    }

    public void SetInput(Vector2 input)
    {
        if(!IsOwner)
        {
            return;
        }

        lookInput = input;
    }

    private void HandleCamera()
    {
        float camX = lookInput.x * camXSens * Time.deltaTime;
        float camY = lookInput.y * camYSens * Time.deltaTime;

        rotationX -= camY;
        rotationX = Mathf.Clamp(rotationX, -89f, 89f);

        cameraPivot.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        UpdatePlayerRotationServerRpc(camX, rotationX);
    }

    #region RPC
    [Rpc(SendTo.Server)]
    private void UpdatePlayerRotationServerRpc(float camX, float pitch)
    {
        transform.Rotate(Vector3.up * camX);

        UpdateRotationClientRpc(transform.rotation, pitch);
    }

    [Rpc(SendTo.Everyone)]
    private void UpdateRotationClientRpc(Quaternion rot, float pitch)
    {
        if(IsOwner)
        {
            return;
        }

        transform.rotation = rot;
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
    #endregion
}
