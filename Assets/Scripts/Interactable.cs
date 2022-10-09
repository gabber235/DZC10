using System.Linq;
using QuickOutline;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    public float outlineWidth = 2.5f;

    private Interactor[] _interactors;

    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        if (_outline == null) _outline = gameObject.AddComponent<Outline>();

        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.OutlineWidth = 0f;

        _interactors = FindObjectsOfType<Interactor>();
    }

    // Update is called once per frame
    private void Update()
    {
        var interactors = _interactors.Where(i => i.Interactable == this);

        var enumerable = interactors.ToList();
        var isTargeted = enumerable.Any();

        if (isTargeted)
        {
            using var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                var color = enumerator.Current.interactableColor;

                while (enumerator.MoveNext()) color = Color.Lerp(color, enumerator.Current.interactableColor, 0.5f);

                _outline.OutlineColor = color;
            }
        }


        _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, isTargeted ? outlineWidth : 0, Time.deltaTime * 40f);
    }
}


internal interface IInteractingTrigger
{
    void OnInteract(Interactor interactor);
}

internal interface IInteractionCondition
{
    bool CanInteract(Interactor interactor);
}