using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{

    [SerializeField] GameObject InventoryMenu;
    private bool InventoryMenuActive;
    [SerializeField] Sprite KnifeImg;
    [SerializeField] Sprite GunImg;
    [SerializeField] Dictionary<string, Image> ItemsSlots = new Dictionary<string, Image>();
    [SerializeField] List<Image> SlotImages;
    [SerializeField] List <string> ItemNames;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip inventoryOpenSound;
    [SerializeField] private AudioClip inventoryCloseSound;


    void Awake()
    {
       ItemsSlots.Clear();
        for (int i = 0; i < ItemNames.Count && i < SlotImages.Count; i++)
        {
            ItemsSlots[ItemNames[i]] = SlotImages[i];
            SlotImages[i].gameObject.SetActive(false);

            AddItemToInventory("Knife", KnifeImg);
            AddItemToInventory("Gun", GunImg);

        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

    }

    public void ToggleInventory()
    {
        InventoryMenuActive = !InventoryMenuActive;
        InventoryMenu.SetActive(InventoryMenuActive);
        Time.timeScale = InventoryMenuActive ? 0f : 1f;

        if (audioSource != null)
        {
            AudioClip clipToPlay = InventoryMenuActive ? inventoryOpenSound : inventoryCloseSound;
            if (clipToPlay != null)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }

    public void AddItemToInventory(string ItemName, Sprite ItemSprite)
    {
        
        if (ItemsSlots.TryGetValue(ItemName, out Image Slot))
        {
            Slot.sprite = ItemSprite;
            Slot.gameObject.SetActive(true);
            
        }
       

    }
    public void RemoveItemFromInventory(string ItemName)
    {
        if (ItemsSlots.TryGetValue(ItemName, out Image Slot))
        {
            Slot.sprite = null;
            Slot.gameObject.SetActive(false);
        }
       
    }   


}
