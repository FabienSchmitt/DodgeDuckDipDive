using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public enum PlayerMotion
{
    Vertical,
    Horizontal
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float flyingSpeed;

    [SerializeField]
    public PlayerMotion playerMotion;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField] AudioClip flappingSound;
    [SerializeField] AudioClip movingSound;

    private PlayerInput playerInput;
    private Animator playerAnimator;
    private Rigidbody2D playerBody;
    private UIManager uiManager;
    private Vector2 motionDirection;
    private bool doMove = false;
    private float boostTimer = 0f;
    private float maxBoostTime = 3f;


    // Set to false when the zone appears. 
    public bool CanChangeDirection { get; set; } = true;
    public bool IsBoostActive { get; set; }


    public event EventHandler ChangeDirectionHandler;

    private void Awake()
    {
        TryGetComponent(out playerInput);
        TryGetComponent(out playerBody);
        TryGetComponent(out playerAnimator);
        uiManager = FindFirstObjectByType<UIManager>();
        if (playerMotion != PlayerMotion.Vertical)
            playerAnimator.SetBool("flappingAbove", true);

        playerInput.onActionTriggered += context =>
        {
            if (context.action.name == InputActionConstants.Player.InputActionMove)
                OnMove(context);
            if (context.action.name == InputActionConstants.Player.InputActionJump)
                ChangeDirection(context);
            if (context.action.name == InputActionConstants.Player.InputActionBoost)
                Boost(context);
        };
    }

    private void OnMove(CallbackContext context)
    {
        if (!enabled && !context.canceled) // always get key up, to stop motion even after disabling it
            return;

        if (context.canceled)
            StopMoving();

        Vector2 playerInput = context.ReadValue<Vector2>();
        if (playerMotion == PlayerMotion.Horizontal && playerInput.x != 0)
        {
            motionDirection.x = playerInput.x;
            // In the end, the bird will move and encounter the floor as it becomes visible.
            if (!doMove)
            {
                motionDirection.y = 0;
            }
        }
        else if (playerMotion == PlayerMotion.Vertical && playerInput.y != 0)
        {
            motionDirection.x = 0;
            motionDirection.y = playerInput.y;
        }
        else
        {
            StopMoving();
        }
        SoundManager.Instance.PlaySound(movingSound);
    }

    private void ChangeDirection(CallbackContext context)
    {
        if (!CanChangeDirection) return; // TODO : inform user
        
        transform.localScale = new Vector2(-this.transform.localScale.x, transform.localScale.y); 
        motionDirection = new Vector2(-motionDirection.x, motionDirection.y);
        if(ChangeDirectionHandler != null)
            ChangeDirectionHandler.Invoke(this, new EventArgs());
    }

    private void Boost(CallbackContext context)
    {
        if (playerMotion == PlayerMotion.Horizontal) return;

        if (context.started)
        {
            Debug.Log("starting");
            IsBoostActive = true;
            uiManager.ShowBoostImage(true);
            boostTimer = 0;
        }
        else if (context.canceled || boostTimer > maxBoostTime)
        {
            IsBoostActive = false;
            uiManager.ShowBoostImage(false);
        }
        else if (context.performed)
        {
            boostTimer += Time.deltaTime;
        }       
    }

    private void StopMoving()
    {
        motionDirection = Vector2.zero;
    }

    public void NeedToMove()
    {
        doMove = true;
        rigidbody2D.gravityScale = 20;
    }

    private void Update()
    {
        // Calling run here allows continuous movement when holding the motion input
        UpdateFlyingBehaviour();
    }

    private void UpdateFlyingBehaviour()
    {
        // Actual movement
        playerBody.linearVelocity = new Vector2(motionDirection.x * flyingSpeed, motionDirection.y * flyingSpeed);            
        
        // Sound effect: avoid playing multiple times the running sound when other sound are playing
        if (!SoundManager.Instance.IsAudioSourcePlaying())
            SoundManager.Instance.PlaySound(flappingSound);
    }

    public PlayerMotion GetPlayerMotion()
    {
        return playerMotion;
    }
}
