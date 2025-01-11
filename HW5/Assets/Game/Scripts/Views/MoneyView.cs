using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        public RectTransform MoneyIcon => m_moneyIcon;

        public bool CanAutoPlayTransactions { get; private set; } = true;

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
            
            if (CanAutoPlayTransactions)
                PlayTransactionAnimationForced();
        }

        public void DisableTransactionAutoPlay()
        {
            CanAutoPlayTransactions = false;
        }

        public void EnableTransactionAutoPlay()
        {
            CanAutoPlayTransactions = true;
        }

        public void PlayTransactionAnimationForced()
        {
            m_textAnimator.AnimateAsInt(m_moneyText.text, m_nextMoneyValue, m_moneyText);
        }
    }
}