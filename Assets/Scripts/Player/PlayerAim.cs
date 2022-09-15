using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private InputController inputController;
    private ShootController shootController;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GetComponent<InputController>();
        shootController = GetComponent<ShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayAimAnimation();
    }

    void PlayAimAnimation()
    {
        if (inputController.isAim)
        {
            shootController.PlayAimanimation();
        }
    }
}
