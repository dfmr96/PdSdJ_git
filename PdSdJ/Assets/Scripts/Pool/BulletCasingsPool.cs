using System;
using System.Collections;
using System.Collections.Generic;
using Pool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletCasingsPool : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BulletCasing casingPrefab;
    [field: SerializeField] public IObjectPool<BulletCasing> _objectPool { get; private set;}

    private bool collectionCheck = true;

    [SerializeField] private int defaultCapacity = 5;
    [SerializeField] private int maxSize = 30;

    private void Awake()
    {
        _objectPool = new ObjectPool<BulletCasing>(CreateProjectile, OnGetFromPool, OnReleaseToPool,
            OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(casingPrefab, spawnPoint.position, spawnPoint.rotation, transform);
        }
    }

    private BulletCasing CreateProjectile()
    {
        BulletCasing bulletInstance = Instantiate(casingPrefab, spawnPoint.position, spawnPoint.rotation, transform);
        bulletInstance.GetComponent<BulletCasing>().ObjectPool = _objectPool;
        return bulletInstance;
    }

    private void OnReleaseToPool(BulletCasing pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }
    
    private void OnGetFromPool(BulletCasing pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
        pooledObject.gameObject.transform.position = spawnPoint.transform.position;
        pooledObject.gameObject.transform.rotation = spawnPoint.transform.rotation;
    }
    
    private void OnDestroyPooledObject(BulletCasing pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}