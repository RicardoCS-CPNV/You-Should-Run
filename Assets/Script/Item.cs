using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    public int id;
    public string named;
    public string description;
    public Sprite image;
    public int price;
    public int hpGiven;
    public int speedGiven;
    public float speedDuration;
}
