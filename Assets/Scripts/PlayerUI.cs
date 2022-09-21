using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Player player;

    [SerializeField] TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = $"{player.health}"; 
    }
}
