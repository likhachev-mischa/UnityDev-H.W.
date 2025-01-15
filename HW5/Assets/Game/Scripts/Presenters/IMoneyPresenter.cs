using System;

namespace Game.Presenters
{
    public interface IMoneyPresenter
    {
        public event Action OnMoneyChanged;
        public string Money { get; }
    }
}