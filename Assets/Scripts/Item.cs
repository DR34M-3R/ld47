using UnityEngine;

[System.Serializable]
public class Item
{
    public Sprite Sprite;
    public ItemType Type;

    public Item(Sprite sprite, ItemType type) {
        Sprite = sprite;
        Type = type;
    }
}

public enum ItemType {
    key,
    battary,
    gun,
}
