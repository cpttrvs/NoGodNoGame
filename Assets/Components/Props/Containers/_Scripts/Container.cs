using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Container : MonoBehaviour, IBlackBoardData
{
    [SerializeField]
    private uint capacity = 0;
    [SerializeField]
    private Transform containerSlot = null;

    private List<IContainable> items = new List<IContainable>();

    public bool AddItem(IContainable item)
    {
        if (item.isContained) return false;

        if (items.Count >= capacity) return false;

        items.Add(item);
        item.container = this;
        item.isContained = true;

        MonoBehaviour mb = item as MonoBehaviour;
        if (mb != null)
        {
            mb.transform.SetParent(containerSlot);
            mb.transform.position = Vector3.zero;
        }

        return true;
    }

    public bool RemoveItem(IContainable item)
    {
        if (!item.isContained) return false;

        if (!items.Contains(item)) return false;

        items.Remove(item);
        item.container = null;
        item.isContained = false;

        // change transform parent ?

        return true;
    }

    public IContainable[] GetItems()
    {
        return items.ToArray();
    }

    public uint GetCapacity() { return capacity; }
    public int GetContentSize() { return items.Count; }
}
