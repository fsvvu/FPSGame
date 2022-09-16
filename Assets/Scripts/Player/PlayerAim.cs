using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private InputController inputController;
    private CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GetComponent<InputController>();
        cameraShake = GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAimAnimation(string weaponName)
    {
        cameraShake.PlayAimAnimation(weaponName);
    }
}
