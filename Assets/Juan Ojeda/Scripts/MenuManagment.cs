using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{

    [SerializeField] GameObject InventoryMenu;
    private bool InventoryMenuActive;
    [SerializeField] Dictionary<string, Image> ItemsSlots = new Dictionary<string, Image>();
    [SerializeField] List<Image> SlotImages;
    [SerializeField] List <string> ItemNames;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       ItemsSlots.Clear();
        for (int i = 0; i < ItemNames.Count && i < SlotImages.Count; i++)
        {
            ItemsSlots[ItemNames[i]] = SlotImages[i];
            SlotImages[i].gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && InventoryMenuActive)
        {
            Time.timeScale = 1f;
            InventoryMenu.SetActive(false);
            InventoryMenuActive = false;
        }

        else if (Input.GetKeyDown(KeyCode.I) && !InventoryMenuActive)
        {
            Time.timeScale = 0f;
            InventoryMenu.SetActive(true);
            InventoryMenuActive = true;
        }

    }

    public void AddItemToInventory(string ItemName, Sprite ItemSprite)
    {
        Debug.Log("Intentando agregar {ItemName} al inventario. Sprite nulo: {ItemSprite == null}");
        if (ItemsSlots.TryGetValue(ItemName, out Image Slot))
        {
            Slot.sprite = ItemSprite;
            Slot.gameObject.SetActive(true);
            
        }
        else
        {
            Debug.LogWarning($"No se encontró un slot para el item: {ItemName}");
        }

    }
public void RemoveItemFromInventory(string ItemName)
    {
        if (ItemsSlots.TryGetValue(ItemName, out Image Slot))
        {
            Slot.sprite = null;
            Slot.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"No se encontró un slot para el item: {ItemName}");
        }
    }


}
