using System;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IMoneyPresenter, IInitializable, IDisposable
    {
        public event Action OnMoneyChanged;
        public string Money => m_moneyStorage.Money.ToString();

        private readonly MoneyStorage m_moneyStorage;
        public MoneyPresenter(MoneyStorage moneyStorage)
        {
            m_moneyStorage = moneyStorage;
        }

        void IInitializable.Initialize()
        {
            m_moneyStorage.OnMoneyChanged += OnMoneyStorageChanged;
        }
        
        void IDisposable.Dispose()
        {
            m_moneyStorage.OnMoneyChanged -= OnMoneyStorageChanged;
        }

        private void OnMoneyStorageChanged(int newvalue, int prevvalue)
        {
            OnMoneyChanged?.Invoke();
        }
    }
}