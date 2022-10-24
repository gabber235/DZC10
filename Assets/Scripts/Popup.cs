using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Optional<string> text;
    public Optional<Sprite> image;

    // Start is called before the first frame update
    private void Start()
    {
        var textObj = GetComponentInChildren<TMP_Text>();
        if (text.Enabled)
            textObj.text = text.Value;
        else
            textObj.gameObject.SetActive(false);

        var imageObj = GetComponentsInChildren<Image>().FirstOrDefault(i => !i.CompareTag("PopupBackground"));
        if (imageObj == null) throw new Exception("No image in popup could be found. Something wrong with the prefab?");
        if (image.Enabled)
            imageObj.sprite = image.Value;
        else
            imageObj.gameObject.SetActive(false);


        if (text.Enabled && image.Enabled)
        {
            textObj.transform.localPosition = new Vector3(0, 1.04f, 0);
            textObj.alignment = TextAlignmentOptions.Top;

            imageObj.transform.localPosition = new Vector3(0, -0.73f, 0);
        }
        else if (text.Enabled)
        {
            textObj.transform.localPosition = new Vector3(0, 0, 0);
            textObj.alignment = TextAlignmentOptions.Center;
        }
        else if (image.Enabled)
        {
            imageObj.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}