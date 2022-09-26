using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    public Player player;

    public TMP_Text healthText;

    public Sprite defaultItemImage;
    
    public List<Image> itemImagesList;
    
    [ItemAttribute] public List<string> itemTypesList;
    
    public List<Sprite> itemSpritesList;
    
    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{player.health}";
        
        for(var i = 0; i < itemImagesList.Count; i++)
        {
            var inventory = player.inventory;
            if(inventory.Items.Count > i)
            {
                var item = inventory.Items[i];
                var typeIndex = itemTypesList.IndexOf(item);
                itemImagesList[i].sprite = typeIndex != -1 && typeIndex < itemSpritesList.Count ? itemSpritesList[typeIndex] : defaultItemImage;
            }
            else
            {
                itemImagesList[i].sprite = defaultItemImage;
            }
        }
    }
}
