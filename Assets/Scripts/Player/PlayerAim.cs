using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private InputController inputController;
    [SerializeField]
    private bool inAim = false;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAimAnimation(CameraShake cameraShake, string weaponName)
    {
        cameraShake.PlayAimAnimation(weaponName, inAim);
        inAim = !inAim;
    }
}
