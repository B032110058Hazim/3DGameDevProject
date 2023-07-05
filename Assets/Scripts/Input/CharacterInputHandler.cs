using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;
    bool isFireButtonPressed = false;
    bool isGrenadeFireButtonPressed = false;
    bool isRocketLauncherFireButtonPressed = false;
    bool isBritishSoldier = true;

    //Other components
    Animator characterAnimator;
    LocalCameraHandler localCameraHandler;
    CharacterMovementHandler characterMovementHandler;
    public Camera localCamera;
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject BritishSoldier;
    [SerializeField]
    private GameObject JapaneseSoldier;

    [Header("Sounds")]
    public AudioSource source;
    public AudioClip clip_aim;
    public AudioClip clip_shoot;

    private void Awake()
    {
        localCamera = GetComponentInChildren<Camera>();
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();

        characterMovementHandler = GetComponent<CharacterMovementHandler>();

        characterAnimator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        image.enabled = false;
        JapaneseSoldier.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovementHandler.Object.HasInputAuthority)
            return;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            //View input
            viewInputVector.x = Input.GetAxis("Mouse X");
            viewInputVector.y = Input.GetAxis("Mouse Y") * -1; //Invert the mouse look

            //Move input
            moveInputVector.x = Input.GetAxis("Horizontal");
            moveInputVector.y = Input.GetAxis("Vertical");

            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                isJumpButtonPressed = true;
            }
               

            if (isBritishSoldier)
            {
                //Fire
                if (Input.GetButtonDown("Fire1"))
                {
                    isFireButtonPressed = true;
                    source.PlayOneShot(clip_shoot);
                }
                    

                //Scope
                if (Input.GetButtonDown("Fire2"))
                {
                    if (localCamera.fieldOfView == 60)
                    {
                        localCamera.fieldOfView = 20;
                        image.enabled = true;
                        characterAnimator.SetBool("IsAiming", true);
                        source.PlayOneShot(clip_aim);
                    }
                    else
                    {
                        localCamera.fieldOfView = 60;
                        image.enabled = false;
                        characterAnimator.SetBool("IsAiming", false);
                        source.PlayOneShot(clip_aim);
                    }
                }
            }

            else if (!isBritishSoldier)
            {
                //Fire
                if (Input.GetButtonDown("Fire1"))
                    isRocketLauncherFireButtonPressed = true;

                //Throw grenade
                if (Input.GetButtonDown("Fire2"))
                    isGrenadeFireButtonPressed = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            //View input
            viewInputVector.x = 0;
            viewInputVector.y = 0;

            //Move input
            moveInputVector.x = 0;
            moveInputVector.y = 0;
        }

        //Set view
        localCameraHandler.SetViewInputVector(viewInputVector);
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //Aim data
        networkInputData.aimForwardVector = localCameraHandler.transform.forward;

        //Move data
        networkInputData.movementInput = moveInputVector;

        //Jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;

        //Fire data
        networkInputData.isFireButtonPressed = isFireButtonPressed;

        //Rocket data
        networkInputData.isRocketLauncherFireButtonPressed = isRocketLauncherFireButtonPressed;

        //Grenade fire data
        networkInputData.isGrenadeFireButtonPressed = isGrenadeFireButtonPressed;

        //Reset variables now that we have read their states
        isJumpButtonPressed = false;
        isFireButtonPressed = false;
        isGrenadeFireButtonPressed = false;
        isRocketLauncherFireButtonPressed = false;

        return networkInputData;
    }

    public void SwapBritishSoldier()
    {
        isBritishSoldier = true;
        
        BritishSoldier.SetActive(true);
        JapaneseSoldier.SetActive(false);
    }

    public void SwapJapaneseSoldier()
    {
        localCamera.fieldOfView = 60;
        image.enabled = false;

        isBritishSoldier = false;

        BritishSoldier.SetActive(false);
        JapaneseSoldier.SetActive(true);
    }
}
