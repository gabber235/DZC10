using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, ISelectHandler
{
    private SoundManager _sm;

    void Start()
    {
        _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    public void OnSelect(BaseEventData eventData){
        _sm.playSoundEffect(1);
    }

    public void OnClick(){
        _sm.playSoundEffect(0);
    }
}
