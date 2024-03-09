using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameArea : MonoBehaviour
{
    public static GameArea Instance;
    [SerializeField] private Bounds _area;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool TryChangeDirection(out Vector3 newDirection, Transform targetTransform)
    {
        if (_area.min.x > targetTransform.position.x)
        {
            newDirection = Vector3.right;
            return true;
        }
        else if (_area.max.x < targetTransform.position.x)
        {
            newDirection = Vector3.left;
            return true;
        }

        newDirection = Vector3.zero;
        return false;
    }

    public bool TryChangePosition(Transform targetTransform)
    {
        if (_area.min.x > targetTransform.position.x)
        {
            Vector3 position = targetTransform.position;
            position.x = _area.max.x;
            targetTransform.position = position;
            return true;
        }
        else if (_area.max.x < targetTransform.position.x)
        {
            Vector3 position = targetTransform.position;
            position.x = _area.min.x;
            targetTransform.position = position;
            return true;
        }
        else if (_area.min.y > targetTransform.position.y)
        {
            Vector3 position = targetTransform.position;
            position.y = _area.max.y;
            targetTransform.position = position;
            return true;
        }
        else if (_area.max.y < targetTransform.position.y)
        {
            Vector3 position = targetTransform.position;
            position.y = _area.min.y;
            targetTransform.position = position;
            return true;
        }

        return false;
    }

    public bool Contains(Vector3 position) => _area.Contains(position);

    public Vector3 GetRandomPoint()
    {
        return new Vector3(Random.Range(_area.min.x, _area.max.x), Random.Range(_area.min.y, _area.max.y));
    }

    public Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(_area.max.x, Random.Range(_area.min.y + 1f, _area.max.y - 1f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_area.center, _area.size);
    }
}
