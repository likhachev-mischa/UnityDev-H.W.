using System;

namespace Game.Presenters
{
    public interface IMoneyPresenter
    {
        event Action<int> OnMoneyChanged;

        bool IsTransactionAnimationEnabled { get; }

        void DisableTransactionAnimation();
        void EnableTransactionAnimation();
        void PlayTransactionAnimation();
    }
}