using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private int _poolCount = 5;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private Bullet _bulletPrefab;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab, _poolCount, transform);
        _pool.autoExpand = _autoExpand;
    }

    public Bullet CreateBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = _pool.GetFreeElement();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        return bullet;
    }
}