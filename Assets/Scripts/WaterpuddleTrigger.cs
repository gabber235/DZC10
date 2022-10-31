using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterpuddleTrigger : MonoBehaviour
{
    public int electricuteCooldown;

    private int electricuteCountdown;

    private SoundManager _sm;
    private List<Player> _players;

    private void Start()
    {
        _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
        _players = new List<Player>();
    }

    private void Update(){
        checkElectricute();
    }

    private void checkElectricute(){
        if(_players.Count < 1) return;
        if(electricuteCountdown<=0){
            foreach (Player player in _players){
                player.Damage(1);
                _sm.playSoundEffect(27);
            }
            electricuteCountdown = electricuteCooldown;
        }else{
            electricuteCountdown -= 1;
        }
    }

    private void OnTriggerEnter(Collider coll){
        var player = coll.gameObject.GetComponent<Player>();
        if (player == null) return;
        _players.Add(player);
    }

    private void OnTriggerExit(Collider coll){
        var player = coll.gameObject.GetComponent<Player>();
        if (player == null) return;
        _players.Remove(player);
    }
}
