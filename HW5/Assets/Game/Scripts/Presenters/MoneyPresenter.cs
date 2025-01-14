using System;
using Game.Views;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        public bool IsTransactionAnimationEnabled { get; private set; } = false;
        
        private readonly MoneyStorage m_moneyStorage;
        private readonly MoneyView m_moneyView;

        public MoneyPresenter(MoneyStorage moneyStorage, MoneyView moneyView)
        {
            m_moneyStorage = moneyStorage;
            m_moneyView = moneyView;
        }

        public void EnableTransactionAnimation()
        {
            if (IsTransactionAnimationEnabled)
                return;

            IsTransactionAnimationEnabled = true;
            m_moneyView.OnTransactionRecorded += PlayTransactionAnimation;
            
        }

        public void DisableTransactionAnimation()
        {
            if (!IsTransactionAnimationEnabled)
                return;

            IsTransactionAnimationEnabled = false;
            m_moneyView.OnTransactionRecorded -= PlayTransactionAnimation;
        }

        public void PlayTransactionAnimation()
        {
            m_moneyView.PlayTransactionAnimation();
        }
        
        void IInitializable.Initialize()
        {
            Setup();
            m_moneyStorage.OnMoneyChanged += OnMoneyChanged;
            EnableTransactionAnimation();
        }

        private void Setup()
        {
            m_moneyView.RecordTransaction(m_moneyStorage.Money.ToString());
        }

        void IDisposable.Dispose()
        {
            m_moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            DisableTransactionAnimation();
        }

        private void OnMoneyChanged(int newvalue, int prevvalue)
        {
            m_moneyView.RecordTransaction(newvalue.ToString());
        }
    }
}