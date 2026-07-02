using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCamera))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler handler;
    private PlayerMotor motor;
    private PlayerCamera cam;

    private void Awake()
    {
        handler = GetComponent<PlayerInputHandler>();
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<PlayerCamera>();
    }

    private void OnEnable()
    {
        handler.OnMoveInput += RequestMovement;
        handler.OnLookInput += RequestLook;
        handler.OnJumpInput += RequestJump;
        handler.OnShootInput += RequestShoot;
        handler.OnReloadInput += RequestReload;
    }

    private void OnDisable()
    {
        handler.OnMoveInput -= RequestMovement;
        handler.OnLookInput -= RequestLook;
        handler.OnJumpInput -= RequestJump;
        handler.OnShootInput -= RequestShoot;
        handler.OnReloadInput -= RequestReload;
    }

    private void RequestMovement(Vector2 input)
    {
        motor.SetInput(input);
    }

    private void RequestLook(Vector2 input)
    {
        cam.SetInput(input);
    }

    private void RequestJump()
    {
        motor.TryJump();
    }

    private void RequestShoot()
    {

    }

    private void RequestReload()
    {

    }
}
