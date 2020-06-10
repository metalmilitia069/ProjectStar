using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeLists_SO<T> : ScriptableObject
{
    protected List<T> runtimeItems = new List<T>();

    public void AddToRuntimeList(T item)
    {
        if (!runtimeItems.Contains(item))
        {
            runtimeItems.Add(item);
        }
    }

    public void RemoveFromRuntimeList(T item)
    {
        if (runtimeItems.Contains(item))
        {
            runtimeItems.Remove(item);
        }
    }
}
