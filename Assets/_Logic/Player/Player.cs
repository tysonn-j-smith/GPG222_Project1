using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMotor))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler handler;
    private PlayerMotor motor;

    private void Awake()
    {
        handler = GetComponent<PlayerInputHandler>();
        motor = GetComponent<PlayerMotor>();
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

    }

    private void RequestJump()
    {

    }

    private void RequestShoot()
    {

    }

    private void RequestReload()
    {

    }
}
