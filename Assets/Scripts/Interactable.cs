using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    
    private QuickOutline.Outline _outline;

    public float outlineWidth = 2.5f;

    private Interactor[] _interactors;
    
    void Start()
    {
        _outline = GetComponent<QuickOutline.Outline>();
        if(_outline == null)
        {
            _outline = gameObject.AddComponent<QuickOutline.Outline>();
        }
        
        _outline.OutlineMode = QuickOutline.Outline.Mode.OutlineVisible;
        _outline.OutlineWidth = 0f;

        _interactors = FindObjectsOfType<Interactor>();
    }

    // Update is called once per frame
    void Update()
    {
        var interactors = _interactors.Where((i) => i.Interactable == this);

        var enumerable = interactors.ToList();
        var isTargeted = enumerable.Any();

        if (isTargeted)
        {
            using var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                var color = enumerator.Current.interactableColor;

                while (enumerator.MoveNext())
                {
                    color = Color.Lerp(color, enumerator.Current.interactableColor, 0.5f);
                }
                
                _outline.OutlineColor = color;
            }
        }
        
        
        _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, isTargeted ? outlineWidth : 0, Time.deltaTime * 40f);
    }
}


interface IInteractingTrigger
{
    void OnInteract(Interactor interactor);
}

