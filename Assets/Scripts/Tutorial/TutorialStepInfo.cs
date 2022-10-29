using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "Create Tutorial Step", menuName = "Custom/Tutorial Step")]
    public class TutorialStepInfo : ScriptableObject
    {
        public string text;
        public List<Sprite> images;

        public Sprite backgroundImage;

        public bool showP1Check = true;
        public bool showP2Check = true;
    }
}