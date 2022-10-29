using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComicController : MonoBehaviour
{

    [SerializeField] private InputActionReference anyKeyReference;
    private int curPageIndex = 0;

    private Image _image;
    
    public Sprite[] pages;
    
    // Start is called before the first frame update
    void Start()
    {
        anyKeyReference.action.Enable();
        anyKeyReference.action.performed += NextPage;
        
        _image = GetComponent<Image>();
        _image.sprite = pages[0];
    }

    void NextPage(InputAction.CallbackContext context) {
        if (curPageIndex < pages.Length - 1)
        {
            curPageIndex++;
            _image.sprite = pages[curPageIndex];
        }
        else
        {
            GameObject.Find("FadeToBlack").GetComponent<Animator>().SetTrigger("FadeOut");
        }
    }
    
}
