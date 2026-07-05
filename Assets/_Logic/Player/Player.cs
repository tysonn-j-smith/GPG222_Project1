using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerInfoHandler))]
public class Player : NetworkBehaviour
{
    private PlayerInputHandler handler;
    private PlayerMotor motor;
    private PlayerCamera cam;
    private PlayerWeapon weapon;
    private PlayerInfoHandler info;

    private void Awake()
    {
        handler = GetComponent<PlayerInputHandler>();
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<PlayerCamera>();
        weapon = GetComponent<PlayerWeapon>();
        info = GetComponent<PlayerInfoHandler>();
    }

    private void OnDestroy()
    {
        GameMaster.Instance.UnregisterPlayer(this.gameObject);
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

    public void RegisterPlayer()
    {
        GameMaster.Instance.RegisterPlayer(this.gameObject, info.GetName());
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
