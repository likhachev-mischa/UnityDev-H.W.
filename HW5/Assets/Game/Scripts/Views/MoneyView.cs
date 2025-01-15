using Game.Presenters;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        public RectTransform MoneyIcon => m_moneyIcon;

        public bool IsTransactionAutoAnimationEnabled { get; private set; } = true;

        [SerializeField]
        private RectTransform m_moneyIcon;

        [SerializeField]
        private TMP_Text m_moneyText;

        [SerializeField]
        private TextAnimator m_textAnimator = new();

        private string m_nextMoneyValue;

        private IMoneyPresenter m_moneyPresenter;

        [Inject]
        public void Construct(IMoneyPresenter moneyPresenter)
        {
            m_moneyPresenter = moneyPresenter;
            OnMoneyChanged();
        }

        public void EnableTransactionAnimation()
        {
            IsTransactionAutoAnimationEnabled = true;
        }

        public void DisableTransactionAnimation()
        {
            IsTransactionAutoAnimationEnabled = false;
        }

        public void PlayTransactionAnimation()
        {
            m_textAnimator.AnimateAsInt(m_moneyText.text, m_nextMoneyValue, m_moneyText);
        }

        private void OnEnable()
        {
            m_moneyPresenter.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            m_moneyPresenter.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged()
        {
            m_nextMoneyValue = m_moneyPresenter.Money;

            if (IsTransactionAutoAnimationEnabled)
                PlayTransactionAnimation();
        }
    }
}