using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{    
    bool isRespawnRequested = false;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    HPHandler hpHandler;
    NetworkInGameMessages networkInGameMessages;
    NetworkPlayer networkPlayer;

    [Header("Animation")]
    public Animator characterAnimator;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip clip_walk;
    public AudioClip clip_jump;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        hpHandler = GetComponent<HPHandler>();
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
        networkPlayer = GetComponent<NetworkPlayer>();

        characterAnimator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            if (isRespawnRequested)
            {
                Respawn();
                return;
            }

            //Don't update the clients position when they are dead
            if (hpHandler.isDead)
                return;
        }

        //Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            //Cancel out rotation on X axis as we don't want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            //Move        
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
                characterAnimator.SetBool("IsJumping", true);

                source.PlayOneShot(clip_jump);
            }

            else
            {
                characterAnimator.SetBool("IsJumping", false);
            }

            //walk
            Vector2 walkVector = new Vector2(networkCharacterControllerPrototypeCustom.Velocity.x, networkCharacterControllerPrototypeCustom.Velocity.z);
            float inputMagnitude = Mathf.Clamp01(walkVector.magnitude);

            characterAnimator.SetFloat("WalkMagnitude", inputMagnitude);

            //walk sound
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                source.PlayOneShot(clip_walk);

                //source.clip = clip_walk;
                //source.loop = true;
                //source.Play();
            }

            //crouch
            if (Input.GetKey(KeyCode.LeftShift))
            {
                characterAnimator.SetBool("IsCrouching", true);
                characterAnimator.SetFloat("CrouchMagnitude", inputMagnitude);
            }

            else
            {
                characterAnimator.SetBool("IsCrouching", false);
            }           

            walkVector.Normalize();

            //Check if we've fallen off the world.
            CheckFallRespawn();
        }
    }

    void CheckFallRespawn()
    {
        if (transform.position.y < -12)
        {
            if (Object.HasStateAuthority)
            {
                Debug.Log($"{Time.time} Respawn due to fall outside of map at position {transform.position}");

                networkInGameMessages.SendInGameRPCMessage(networkPlayer.nickName.ToString(), "fell off the world");

                Respawn();
            }
        }
    }

    public void RequestRespawn()
    {
        isRespawnRequested = true;
    }

    void Respawn()
    {
        networkCharacterControllerPrototypeCustom.TeleportToPosition(Utils.GetRandomSpawnPoint());

        hpHandler.OnRespawned();

        isRespawnRequested = false;
    }

    public void SetCharacterControllerEnabled(bool isEnabled)
    {
        networkCharacterControllerPrototypeCustom.Controller.enabled = isEnabled;
    }

}
