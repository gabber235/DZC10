using UnityEngine;
using UnityEngine.Serialization;

public class EndZone : MonoBehaviour
{
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    [FormerlySerializedAs("_playersPresent")] [SerializeField]
    private int playersPresent;

    [FormerlySerializedAs("FadeToNext")] [SerializeField]
    private Animator fadeToNext;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        playersPresent++;

        if (playersPresent == 2)
            // Could have an end-screen here instead.
            fadeToNext.SetTrigger(FadeOut);
    }

    public void OnTriggerExit(Collider other)
    {
        playersPresent--;
    }
}