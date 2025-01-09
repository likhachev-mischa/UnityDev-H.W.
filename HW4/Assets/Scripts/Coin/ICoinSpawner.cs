using System;
using System.Collections.Generic;
using Modules;

namespace SnakeGame
{
    public interface ICoinSpawner
    {
        event Action<ICoin> CoinDespawned;
        List<ICoin> Coins { get; }
        ICoin Spawn();
        void Despawn(ICoin coin);
        bool IsEmpty();
    }
}