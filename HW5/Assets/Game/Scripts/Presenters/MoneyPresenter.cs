using System;
using Game.Views;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly MoneyStorage m_moneyStorage;
        private readonly MoneyView m_moneyView;

        public MoneyPresenter(MoneyStorage moneyStorage, MoneyView moneyView)
        {
            m_moneyStorage = moneyStorage;
            m_moneyView = moneyView;
        }

        void IInitializable.Initialize()
        {
            Setup();
            m_moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void Setup()
        {
            m_moneyView.RecordTransaction(m_moneyStorage.Money.ToString());
        }

        void IDisposable.Dispose()
        {
            m_moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int newvalue, int prevvalue)
        {
            m_moneyView.RecordTransaction(newvalue.ToString());
        }
    }
}