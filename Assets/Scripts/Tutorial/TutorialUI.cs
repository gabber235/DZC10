using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        public Sprite cross;
        public Sprite check;

        public Image p1Status;
        public Image p2Status;
        public Image imageObj;
        public TMP_Text textObj;
        public Image backgroundObj;
        private bool _changedText;

        private float _cycleStart;

        private TutorialManager _tutorialManager;

        // Start is called before the first frame update
        private void Start()
        {
            _tutorialManager = FindObjectOfType<TutorialManager>();

            if (_tutorialManager == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _tutorialManager.OnTutorialStepChanged += OnTutorialStepChanged;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_tutorialManager == null) return;

            if (!_tutorialManager.steps.TryGetValue(_tutorialManager.Step, out var info)) return;
            textObj.text = info.text;

            _cycleStart += Time.deltaTime;

            p1Status.sprite = _tutorialManager.needsFinish.Contains(1) ? cross : check;
            p2Status.sprite = _tutorialManager.needsFinish.Contains(2) ? cross : check;

            if (info.images.Count <= 0) return;
            // Loop through the images, and show them in order.
            // Let each image be shown for 2 seconds.
            var index = (int)(_cycleStart / 2) % info.images.Count;
            imageObj.sprite = info.images[index];
        }

        private void OnTutorialStepChanged(TutorialStep step)
        {
            _cycleStart = 0;

            if (_changedText)
            {
                textObj.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 240);
                textObj.rectTransform.Translate(-35f, 0f, 0f);
                _changedText = false;
            }

            if (_tutorialManager.steps.TryGetValue(step, out var info))
            {
                for (var i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);

                textObj.text = info.text;

                if (info.images.Count > 0)
                {
                    imageObj.sprite = info.images[0];
                    textObj.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 160);
                    textObj.rectTransform.Translate(35f, 0f, 0f);
                    _changedText = true;
                }
                else
                {
                    imageObj.gameObject.SetActive(false);
                }

                if (!info.showP1Check) p1Status.gameObject.SetActive(false);
                if (!info.showP2Check) p2Status.gameObject.SetActive(false);

                backgroundObj.sprite = info.backgroundImage;
            }
            else
            {
                for (var i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}