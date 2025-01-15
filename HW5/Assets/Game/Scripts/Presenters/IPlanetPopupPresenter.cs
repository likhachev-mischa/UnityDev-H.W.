using System;
using UnityEngine;

namespace Game.Presenters
{
    public interface IPlanetPopupPresenter
    {
        event Action OnPlanetChanged;
        
        event Action OnStatsChanged;

        event Action OnUpgraded;
        
        event Action OnUpgradeStatusChanged;
        
        Sprite Icon { get; }
        string Name { get; }
        
        string Population { get; }        
        string Income { get; }
        
        string UpgradePrice { get; }
        string Level { get; }
        bool IsPlanetMaxLevel { get; }
        
        bool CanUpgrade { get; }

        void Upgrade();
    }
}