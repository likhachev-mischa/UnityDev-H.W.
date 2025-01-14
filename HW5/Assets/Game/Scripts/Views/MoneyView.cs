using System;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        public event Action OnTransactionRecorded;
        
        public RectTransform MoneyIcon => m_moneyIcon;

        [SerializeField]
        private RectTransform m_moneyIcon;

        [SerializeField]
        private TMP_Text m_moneyText;

        [SerializeField]
        private TextAnimator m_textAnimator = new();

        private string m_nextMoneyValue;

        public void RecordTransaction(string value)
        {
            m_nextMoneyValue = value;
            OnTransactionRecorded?.Invoke();
        }

        public void PlayTransactionAnimation()
        {
            m_textAnimator.AnimateAsInt(m_moneyText.text, m_nextMoneyValue, m_moneyText);
        }
    }
}