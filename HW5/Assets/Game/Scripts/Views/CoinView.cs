using System;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField]
        private float m_animationDuration = 1.0f;

        private MoneyView m_moneyWidget;

        private ParticleAnimator m_particleAnimator;

        [Inject]
        private void Construct(MoneyView moneyView, ParticleAnimator particleAnimator)
        {
            m_moneyWidget = moneyView;
            m_particleAnimator = particleAnimator;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void PlayAnimation(Action callback = null)
        {
            m_particleAnimator.Emit(gameObject.transform.position, m_moneyWidget.MoneyIcon.position,
                m_animationDuration,
                callback);
        }
    }
}