using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalCameraHandler : MonoBehaviour
{
    public Transform cameraAnchorPoint;

    //Input
    Vector2 viewInput;

    //Rotation
    float cameraRotationX = 0;
    float cameraRotationY = 0;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    public Camera localCamera;

    [SerializeField]
    private Image image;

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraRotationX = GameManager.instance.cameraViewRotation.x;
        cameraRotationY = GameManager.instance.cameraViewRotation.y;
        image.enabled = false;
    }

    void LateUpdate()
    {
        if (cameraAnchorPoint == null)
            return;

        if (!localCamera.enabled)
            return;

        //Move the camera to the position of the player
        localCamera.transform.position = cameraAnchorPoint.position;

        if (Input.GetKey(KeyCode.LeftControl))
            localCamera.transform.position = localCamera.transform.position - new Vector3(0, 0.5f, 0);

        //Calculate rotation
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        cameraRotationY += viewInput.x * Time.deltaTime * networkCharacterControllerPrototypeCustom.rotationSpeed;

        //Apply rotation
        localCamera.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);

        //Scope
        if (Input.GetButtonDown("Fire3"))
        {
            if (localCamera.fieldOfView == 60)
            {
                localCamera.fieldOfView = 30;
                image.enabled = true;
            }
            else
            {
                localCamera.fieldOfView = 60;
                image.enabled = false;
            }
        }

    }
    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }

    private void OnDestroy()
    {
        if (cameraRotationX != 0 && cameraRotationY != 0)
        {
            GameManager.instance.cameraViewRotation.x = cameraRotationX;
            GameManager.instance.cameraViewRotation.y = cameraRotationY;
        }
    }
}
