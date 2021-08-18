using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController instance;

    public Vector2 move;
    public Vector2 look;

    public bool isIdle;
    public bool isWalk, isWalkRight, isWalkLeft, isWalkBackward, isWalkForward;
    public bool isAim;
    public bool isFire = false;
    public bool isAutoFire, isSingleFire;
    public bool isManipulationFire;
    public bool isJump;
    public bool isBrake;

    public float horizontal, vertical;
    public float mouseX, mouseY;
    public float fireValue = 0;
    public float jumpValue;

    void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else instance = this;
    }

    public static InputController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        move.x = horizontal = Input.GetAxis("Horizontal");
        move.y = vertical = Input.GetAxis("Vertical");
        look.x = mouseX = Input.GetAxis("Mouse X");
        look.y = mouseY = Input.GetAxis("Mouse Y");

        isJump = isBrake = Input.GetButtonDown("Jump");
        isSingleFire = Input.GetMouseButtonDown(0);
        isAutoFire = (!isSingleFire && Input.GetMouseButton(0));
        isAim = Input.GetMouseButton(1);

        if (Input.GetMouseButton(0))
        {
            fireValue += 0.25f;
            isFire = true;
        }
        //else if (Input.GetMouseButtonDown(0))
        //{
        //    fireValue = 2;
        //    isFire = true;
        //}
        else if (Input.GetMouseButtonUp(0) || !(Input.GetMouseButton(0)))
        {
            fireValue -= 0.25f;
        }

        fireValue = Mathf.Clamp(fireValue, 0, 2);

        if (fireValue == 0 && isFire)
        {
            isFire = false;
        }

        UpdateStatusAnimation();
    }

    void UpdateStatusAnimation()
    {
        if (horizontal != 0 || vertical != 0)
        {
            isIdle = false;
            isWalk = true;
        }
        else
        {
            isIdle = true;
            isWalk = false;
        }

        if (isAutoFire || isAim || isSingleFire)
        {
            isManipulationFire = true;
        }
        else isManipulationFire = false;
    }
}
