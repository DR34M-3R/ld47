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

    public void Awake() {
        Instance = this;
    }

    public void AddItem(ItemType type) {
        foreach(Item setItem in ItemList) {
            if(setItem.Type == type) {
                Items.Add(setItem);
            }
        }
        CellUpdate();
    }

    public void RemoveItem(ItemType type) {
        for(int i = 0; i < Items.Count; i++) {
            if(Items[i].Type == type) {
                Items.RemoveAt(i);
            }
        }
        CellUpdate();
    }

    public void CellUpdate() {
        for(int i = 0; i < Cells.Count; i++) {
            Cells[i].sprite = null;
        }
        for(int i = 0; i < Items.Count; i++) {
            Cells[i].sprite = Items[i].Sprite;
            Cells[i].SetNativeSize();
        }
    }

    
}
