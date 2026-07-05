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

    public override void OnNetworkSpawn()
    {
        info.SetName();

        RegisterPlayer();
    }

    public override void OnDestroy()
    {
        GameMaster.Instance.UnregisterPlayer(this.gameObject);
    }

    public void RegisterPlayer()
    {
        if (!IsOwner)
        {
            return;
        }

        GameMaster.Instance.RegisterPlayer(this.gameObject, info.GetName());
    }

    private void RequestMovement(Vector2 input)
    {
        if(!IsOwner)
        {
            return;
        }

        motor.SetInput(input);
    }

    private void RequestLook(Vector2 input)
    {
        if (!IsOwner)
        {
            return;
        }

        cam.SetInput(input);
    }

    private void RequestJump()
    {
        if (!IsOwner)
        {
            return;
        }

        motor.TryJump();
    }

    private void RequestShoot()
    {
        if (!IsOwner)
        {
            return;
        }

        weapon.ShootCall();
    }

    private void RequestReload()
    {
        if (!IsOwner)
        {
            return;
        }

        weapon.ReloadCall();
    }
}
