using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] TMP_Text healthText;

    [SerializeField] Sprite defaultItemImage;
    
    [SerializeField] List<Image> itemImagesList;
    
    [SerializeField] List<ItemType> itemTypesList;
    [SerializeField] List<Sprite> itemSpritesList;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = $"{player.health}";
        
        for(var i = 0; i < itemImagesList.Count; i++)
        {
            var inventory = player.Inventory;
            if(inventory.Items.Count > i)
            {
                var item = inventory.Items[i];
                var typeIndex = itemTypesList.IndexOf(item.Type);
                itemImagesList[i].sprite = typeIndex != -1 && typeIndex < itemSpritesList.Count ? itemSpritesList[typeIndex] : defaultItemImage;
            }
            else
            {
                itemImagesList[i].sprite = defaultItemImage;
            }
        }
    }
}
