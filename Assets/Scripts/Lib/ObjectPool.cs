using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand;
    public Transform container;

    private List<T> pool;

    public ObjectPool(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;

        this.CreatePool(count);
    }
    
    public ObjectPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        
        this.CreatePool(count);
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var objective in pool)
        {
            if (!objective.gameObject.activeInHierarchy)
            {
                element = objective;
                objective.gameObject.SetActive(true);
                return true;
            }
        }
        
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (this.autoExpand)
            return this.CreateObject(true);
        
        throw new System.Exception($"No free elements in pool! {typeof(T)} ");
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++) 
            this.CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }
}