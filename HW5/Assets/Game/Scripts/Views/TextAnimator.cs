using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    [Serializable]
    public class TextAnimator
    {
        [SerializeField]
        private float m_duration = 1.0f;

        public void AnimateAsInt(string start, string end, TMP_Text text)
        {
            int startVal = int.Parse(start);
            int endVal = int.Parse(end);
            DOTween.To(() => startVal, x => startVal = x, endVal, m_duration)
                .OnUpdate(() => text.text = startVal.ToString());
        }
    }
}