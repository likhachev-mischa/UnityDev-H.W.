using System;

namespace Homework
{
    /**
       Конвертер представляет собой преобразователь ресурсов, который берет ресурсы
       из зоны погрузки (справа) и через несколько секунд преобразовывает его в
       ресурсы другого типа (слева).

       Конвертер работает автоматически. Когда заканчивается цикл переработки
       ресурсов, то конвертер берет следующую партию и начинает цикл по новой, пока
       можно брать ресурсы из зоны загрузки или пока есть место для ресурсов выгрузки.

       Также конвертер можно выключать. Если конвертер во время работы был
       выключен, то он возвращает обратно ресурсы в зону загрузки. Если в это время
       были добавлены еще ресурсы, то при переполнении возвращаемые ресурсы
       «сгорают».

       Характеристики конвертера:
       - Зона погрузки: вместимость бревен
       - Зона выгрузки: вместимость досок
       - Кол-во ресурсов, которое берется с зоны погрузки
       - Кол-во ресурсов, которое поставляется в зону выгрузки
       - Время преобразования ресурсов
       - Состояние: вкл/выкл
     */
    public sealed class Converter
    {
        public bool IsRunning { get; private set; }
        public uint InputCount { get; private set; }
        public uint OutputCount { get; private set; }

        private readonly uint m_inputCapacity;
        private readonly uint m_outputCapacity;

        private readonly uint m_inputDelta;
        private readonly uint m_outputDelta;

        private readonly float m_cycleTime;
        private float m_timer;

        public Converter(uint inputCapacity,
            uint outputCapacity, uint inputDelta, uint outputDelta, float cycleTime)
        {
            m_inputCapacity = inputCapacity;
            m_outputCapacity = outputCapacity;
            m_inputDelta = inputDelta;
            m_outputDelta = outputDelta;
            m_cycleTime = cycleTime;

            m_timer = 0;
            TryCycleStart();
        }

        public void Update(float deltaTime)
        {
            if (!IsRunning)
            {
                return;
            }

            m_timer += deltaTime;
            if (m_timer >= m_cycleTime)
            {
                CycleEnd();
                m_timer = 0;
            }
        }

        public bool TryActivate()
        {
            return TryCycleStart();
        }

        public void Deactivate()
        {
            CycleAbort();
        }

        public void AddResources(uint value)
        {
            InputCount = Math.Min(m_inputCapacity, InputCount + value);

            if (!IsRunning)
            {
                TryCycleStart();
            }
        }

        public bool IsFull()
        {
            return OutputCount == m_outputCapacity;
        }

        private bool TryCycleStart()
        {
            long newInput = (long)InputCount - m_inputDelta;
            if (newInput < 0 || OutputCount + m_outputDelta > m_outputCapacity)
            {
                return false;
            }

            InputCount = (uint)newInput;
            IsRunning = true;
            return true;
        }

        private void CycleAbort()
        {
            if (!IsRunning)
            {
                return;
            }

            AddResources(m_inputDelta);
            IsRunning = false;
        }

        private void CycleEnd()
        {
            OutputCount += m_outputDelta;
            IsRunning = false;

            TryCycleStart();
        }
    }
}