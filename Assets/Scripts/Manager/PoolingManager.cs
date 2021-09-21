using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;

    public BulletHoleBehaviour hitEffectPrefab;
    public List<BulletHoleBehaviour> hitEffectList;
    public int spawnCount;
    public int indexReadyToUse = 0;
    public int indexAlwaysReady = 0;

    void MakeInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else instance = this;
    }

    public static PoolingManager Instance
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
        for (int i = 0; i < spawnCount; i++)
        {
            BulletHoleBehaviour hitEffect = Instantiate(hitEffectPrefab);
            hitEffect.transform.parent = transform;
            hitEffect.gameObject.SetActive(false);
            hitEffect.index = i;
            hitEffectList.Add(hitEffect);
        }
    }

    public void UseOneHItEffect(RaycastHit hit)
    {
        //Debug.Log("current index can use: " + indexReadyToUse);

        if (hitEffectList[indexReadyToUse].gameObject.activeInHierarchy) indexReadyToUse = indexAlwaysReady;

        if (indexReadyToUse >= spawnCount)
        {
            //Debug.Log("Add new hole, index: " + hitEffectList.Count);
            BulletHoleBehaviour hitEffect = Instantiate(hitEffectPrefab);
            hitEffect.transform.parent = transform;
            hitEffect.gameObject.SetActive(false);
            hitEffect.index = hitEffectList.Count;
            hitEffectList.Add(hitEffect);
        }

        hitEffectList[indexReadyToUse].gameObject.SetActive(true);
        hitEffectList[indexReadyToUse].transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
        indexReadyToUse++;
        indexAlwaysReady = indexReadyToUse;
        //Debug.Log("current index after use: " + indexReadyToUse + " " + indexAlwaysReady);
    }
}