using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private KeyCode assignKey;

    public string ItemName => itemName;
    public Sprite ItemSprite => itemSprite;
    public GameObject ItemPrefab => itemPrefab;
    public KeyCode AssignKey => assignKey;

}
