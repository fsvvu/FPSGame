using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public GameObject bulletPrefab, magazine;
    private CameraShake cameraShake;

    public Transform raycastOrigin;
    public Transform fpsCameraTransform;

    public Transform bulletSpawnPoint, casingSpawnPoint;

    public ParticleSystem hitEffectPrefab;
    public ParticleSystem muzzleFlash;

    public AnimationClip weaponAnimation;

    public float range = 300f;

    WeaponStats weaponStats;
    RaycastHit hit;
    int layerMask;

    [Header("Weapon Sway (not used)")]
    //Enables weapon sway
    public bool weaponSway;
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float swaySmoothValue = 4.0f;

    private void Start()
    {
        cameraShake = GetComponent<CameraShake>();
        weaponStats = GetComponent<WeaponPickup>().weaponStats;

        fpsCameraTransform = Camera.main.transform;
        layerMask = ~(1 << LayerMask.NameToLayer("Ignore Raycast") | 1 << LayerMask.NameToLayer("Ignore Player") | 1 << LayerMask.NameToLayer("Only Player"));
        hitEffectPrefab = Instantiate(weaponStats.hitEffectPrefab, transform);
        hitEffectPrefab.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void StartFiring()
    {
        muzzleFlash.Emit(1);
        cameraShake.GenerateRecoil(weaponStats.name);

        #region Spawn bullet object and tracer
        //Spawn casing prefab at spawnpoint
        //Instantiate(weaponStats.casingPrefab,
        //    casingSpawnPoint.position,
        //    casingSpawnPoint.rotation);

        //Spawn bullet from bullet spawnpoint
        //Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.LookRotation(fpsCameraTransform.transform.forward));

        //var tracer = Instantiate(weaponStats.bulletTracer, raycastOrigin.position, Quaternion.identity);
        //tracer.AddPosition(bulletSpawnPoint.position);
        #endregion

        if (Physics.Raycast(raycastOrigin.position, fpsCameraTransform.forward, out hit, range, layerMask))
        {
            hitEffectPrefab.transform.position = hit.point;
            hitEffectPrefab.transform.forward = hit.normal;
            hitEffectPrefab.Emit(5);

            PoolingManager.Instance.UseOneHItEffect(hit);

            #region Minigame Test
            //tracer.transform.position = hit.point;
            //if (hit.transform.gameObject.tag == "Wall")
            //{
            //    GameObject wall = hit.transform.gameObject;
            //    WallSpawner.Instance.DestroyWall(wall.GetComponent<WallBehaviour>().index, hit);
            //}
            #endregion
        }
        //else tracer.transform.position += fpsCameraTransform.forward * range;
    }

    public void StopFiring()
    {
        cameraShake.ResetRecoil(weaponStats.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastOrigin.position, fpsCameraTransform.forward);
    }
}
