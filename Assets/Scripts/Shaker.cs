using System.Collections.Generic;
using Tutorial;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Have the shaker switch between the list of players. Such that only one player has the shaker.
/// </summary>
public class Shaker : MonoBehaviour
{
    public List<Player> players;
    public List<InputActionReference> throwsActionReference;
    public int currentShakerIndex;
    public Vector3 offset;

    // Used for LOS shaker switching
    public Vector3 LOS_offset;
    public LayerMask ignoreLayer;
    public float maxThrowDistance;

    private Vector3 _velocity;
    private SoundManager SM;

    private void Start()
    {
        for (var i = 0; i < throwsActionReference.Count; i++)
        {
            var index = i;
            throwsActionReference[i].action.Enable();
            throwsActionReference[i].action.performed += _ => SwitchShaker(index);
        }

        players[currentShakerIndex].shaker = true;

        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            players[currentShakerIndex].transform.position + offset,
            ref _velocity,
            0.1f
        );

        // Debug.DrawRay(players[0].transform.position + LOS_offset, (players[1].transform.position + LOS_offset) - (players[0].transform.position + LOS_offset), Color.yellow);
    }

    // If the player is holding the shaker, switch to the next player
    private void SwitchShaker(int index)
    {
        if (currentShakerIndex != index) return;
        if (players[0] == null || players[1] == null) return;
        
        // Ensure line-of-sight when switching shaker
        RaycastHit hit;
        if (Physics.Raycast(players[0].transform.position + LOS_offset, (players[1].transform.position + LOS_offset) - (players[0].transform.position + LOS_offset), out hit, maxThrowDistance, ~ignoreLayer)) {
            if (hit.collider.CompareTag("Player")) {
                // Debug.Log("Player in sight");
                SM.playSoundEffect(20);
                players[currentShakerIndex].shaker = false;
                currentShakerIndex = (currentShakerIndex + 1) % players.Count;
                players[currentShakerIndex].shaker = true;
            }
            else {
                // Debug.Log("No Player LOS");
            }
        }

        // Ensure line-of-sight when switching shaker
        if (!Physics.Raycast(players[0].transform.position + LOS_offset,
                players[1].transform.position + LOS_offset - (players[0].transform.position + LOS_offset),
                out var hit,
                maxThrowDistance, ~ignoreLayer
            )) return;
        if (!hit.collider.CompareTag("Player")) return;

        players[currentShakerIndex].shaker = false;
        FindObjectOfType<TutorialManager>()?.FinishStep(TutorialStep.Throw, currentShakerIndex + 1);
        currentShakerIndex = (currentShakerIndex + 1) % players.Count;
        players[currentShakerIndex].shaker = true;
    }
}