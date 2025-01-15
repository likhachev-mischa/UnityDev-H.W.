using System;
using UnityEngine;

namespace Game.Presenters
{
    public interface IPlanetPresenter
    {
        event Action OnUnlocked;

        event Action OnIncomeTimeChanged;
        event Action OnIncomeReady;
        event Action OnGathered;
        
        Sprite Icon { get; }
        
        string Price { get; }
        string IncomeTime { get; }
        float IncomeProgress { get; }
        
        bool IsUnlocked { get; }
        bool IsReadyToGather { get; }
        
        void Unlock();
        void ShowPopup();
        void Gather();
    }
}