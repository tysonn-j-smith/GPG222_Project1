using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerWeapon))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler handler;
    private PlayerMotor motor;
    private PlayerCamera cam;
    private PlayerWeapon weapon;

    private void Awake()
    {
        handler = GetComponent<PlayerInputHandler>();
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<PlayerCamera>();
        weapon = GetComponent<PlayerWeapon>();
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
        weapon.ShootCall();
    }

    private void RequestReload()
    {
        weapon.ReloadCall();
    }
}
