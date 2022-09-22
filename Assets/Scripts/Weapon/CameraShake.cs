using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : GameObserver
{
    public Cinemachine.CinemachineVirtualCamera playerCamera;
    public Cinemachine.CinemachinePOV playerAiming;  
    public Cinemachine.CinemachineImpulseSource cameraShake;
    public Animator rigController;
    public Vector2[] recoilPattern;

    [SerializeField]
    InputController inputController;
    [SerializeField]
    WeaponPickup weaponPickup;

    public float duration;
    public float yAxisValue;

    public int index = 0;
    float horizontalRecoil, verticalRecoil;
    float time;

    public override void Awake()
    {
        cameraShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
        playerAiming = playerCamera.GetCinemachineComponent<Cinemachine.CinemachinePOV>();
        weaponPickup = GetComponent<WeaponPickup>();
    }

    private void Start()
    {
        gameEvent?.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRecoilValue();
    }

    void UpdateRecoilValue()
    {
        yAxisValue = playerAiming.m_VerticalAxis.Value;
        if (rigController)
        {
            if(weaponPickup.weaponSlot == ActiveWeapon.WeaponSlot.Sidearm) rigController.SetBool("inAttack", inputController.isFire && inputController.isSingleFire);
            else rigController.SetBool("inAttack", inputController.isFire);
        }

        if (time > 0)
        {
            playerAiming.m_HorizontalAxis.Value -= horizontalRecoil * Time.deltaTime / duration;
            playerAiming.m_VerticalAxis.Value -= verticalRecoil * Time.deltaTime / duration;
            time -= Time.deltaTime;
        }
    }

    int NextIndex()
    {
        return ((index + 1) % recoilPattern.Length);
    }
        
    public void ResetRecoil(string weaponName)
    {
        index = 0;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPattern[index].x;
        verticalRecoil = recoilPattern[index].y;

        index = NextIndex();

        rigController.Play("Recoil " + weaponName, 1, 0.0f);
    }

    public void PlayAimAnimation(string weaponName, bool inAim)
    {
        if (inAim == false) rigController.Play("Aim " + weaponName + " Effect");
        else rigController.Play("Exit Aim " + weaponName + " Effect");

        rigController.SetBool("inAim", !inAim);
    }

    //public override void Execute(IGameEvent gameEvent)
    //{
    //    Debug.Log($"Run from children {this}");
    //    base.Execute(gameEvent);
    //}

    public void SetUpWeaponRecoilForNewWeapon(Cinemachine.CinemachineVirtualCamera newCamera, Animator newRigController)
    {
        playerCamera = newCamera;
        rigController = newRigController;
    }
}
