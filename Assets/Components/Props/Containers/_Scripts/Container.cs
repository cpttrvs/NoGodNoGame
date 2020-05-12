using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AillieoUtils.EasyBehaviorTree;

public class Container : MonoBehaviour, IBlackBoardData
{
    [SerializeField]
    private uint capacity = 0;
    [SerializeField]
    protected Transform containerSlot = null;

    private List<IContainable> items = new List<IContainable>();

    private void Start()
    {
        if(containerSlot.childCount > 0)
        {
            foreach (Transform tr in containerSlot)
            {
                IContainable containable = tr.gameObject.GetComponent<IContainable>();

                if (containable != null)
                {
                    bool success = AddItem(containable, false);

                    if (!success)
                    {
                        Debug.LogWarning("Start: " + name + " can't auto-add containables");
                    }
                }
            }
        }
    }

    public bool AddItem(IContainable item, bool hasPosition = true)
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

            if(hasPosition)
                mb.transform.localPosition = Vector3.zero;
        }

        //Debug.Log("Container: " + name + " added an item");

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

    public bool RemoveAny()
    {
        IContainable item = null;
        foreach(IContainable i in items)
        {
            item = i;
            break;
        }

        return RemoveItem(item);
    }

    public IContainable[] GetItems()
    {
        return items.ToArray();
    }

    public uint GetCapacity() { return capacity; }
    public int GetContentSize() { return items.Count; }
    public bool IsFull() { return GetContentSize() == GetCapacity(); }
}
