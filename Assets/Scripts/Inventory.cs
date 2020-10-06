using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private List<Item> ItemList = new List<Item>();
    [SerializeField] private List<Item> Items = new List<Item>();
    [SerializeField] private List<Image> Cells = new List<Image>();

    public void Awake()
    {
        CellUpdate();
        Instance = this;
    }

    public void AddItem(ItemType type)
    {
        foreach (Item setItem in ItemList)
        {
            if (setItem.Type == type)
            {
                Items.Add(setItem);
            }
        }

        CellUpdate();
    }

    public bool RemoveItem(ItemType type)
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Type == type)
            {
                Items.RemoveAt(i);
                CellUpdate();
                return true;
            }
        }

        return false;
    }

    private void CellUpdate()
    {
        foreach (var cell in Cells)
        {
            cell.sprite = null;
            cell.enabled = false;
        }

        for (var i = 0; i < Items.Count; i++)
        {
            Cells[i].sprite = Items[i].Sprite;
            Cells[i].SetNativeSize();
            Cells[i].enabled = true;

        }
    }
}