using System;
using NUnit.Framework;

namespace Homework
{
    public sealed class ConverterTests
    {
        [TestCase(20u, 20u, 40u, 1u, 2u, 15.0f)]
        [TestCase(50u, 100u, 50u, 25u, 25u, 30.0f)]
        [TestCase(25u, 40u, 25u, 1u, 1u, 20.0f)]
        public void ConvertFullOutputEmptyInput(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);

            int iteration = 1;
            while (converter.IsRunning)
            {
                Assert.AreEqual(Math.Max(startInputCount - inputDelta * iteration, 0),
                    (long)converter.InputCount, $"Input, cycle {iteration}");
                converter.Update(cycleTime);
                Assert.AreEqual(outputDelta * iteration, converter.OutputCount, $"Output, cycle {iteration}");
                ++iteration;
            }

            Assert.AreEqual(outputCapacity, converter.OutputCount);
            Assert.AreEqual(0, converter.InputCount);
            Assert.IsTrue(converter.IsFull());
        }

        [TestCase(30u, 30u, 40u, 1u, 2u, 15.0f)]
        [TestCase(70u, 100u, 50u, 25u, 25u, 30.0f)]
        [TestCase(32u, 40u, 25u, 1u, 1u, 20.0f)]
        public void ConvertFullOutputNotEmptyInput(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);

            int iteration = 1;
            while (converter.IsRunning)
            {
                Assert.AreEqual(Math.Max(startInputCount - inputDelta * iteration, 0),
                    (long)converter.InputCount, $"Input, cycle {iteration}");
                converter.Update(cycleTime);
                Assert.AreEqual(outputDelta * iteration, converter.OutputCount, $"Output, cycle {iteration}");
                ++iteration;
            }

            --iteration;

            Assert.AreEqual(outputCapacity, converter.OutputCount);
            Assert.AreEqual(startInputCount - inputDelta * iteration, converter.InputCount);
            Assert.AreNotEqual(0, converter.InputCount);
            Assert.IsTrue(converter.IsFull());
        }

        [TestCase(30u, 30u, 62u, 11u, 2u, 15.0f)]
        [TestCase(70u, 100u, 101u, 25u, 25u, 30.0f)]
        [TestCase(32u, 40u, 33u, 5u, 1u, 20.0f)]
        public void ConvertNotFullOutputNotEmptyInput(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);

            int iteration = 1;
            while (converter.IsRunning)
            {
                Assert.AreEqual(Math.Max(startInputCount - inputDelta * iteration, 0),
                    (long)converter.InputCount, $"Input, cycle {iteration}");
                converter.Update(cycleTime);
                Assert.AreEqual(outputDelta * iteration, converter.OutputCount, $"Output, cycle {iteration}");
                ++iteration;
            }

            --iteration;

            Assert.AreEqual(outputDelta * iteration, converter.OutputCount);
            Assert.AreEqual(startInputCount - inputDelta * iteration, converter.InputCount);
            Assert.AreNotEqual(0, converter.InputCount);
            Assert.AreNotEqual(outputCapacity, converter.OutputCount);
            Assert.IsFalse(converter.IsFull());
        }

        [TestCase(30u, 30u, 62u, 5u, 2u, 15.0f)]
        [TestCase(75u, 100u, 101u, 25u, 25u, 30.0f)]
        [TestCase(32u, 40u, 33u, 2u, 1u, 20.0f)]
        public void ConvertNotFullOutputEmptyInput(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);

            int iteration = 1;
            while (converter.IsRunning)
            {
                Assert.AreEqual(Math.Max(startInputCount - inputDelta * iteration, 0),
                    (long)converter.InputCount, $"Input, cycle {iteration}");
                converter.Update(cycleTime);
                Assert.AreEqual(outputDelta * iteration, converter.OutputCount, $"Output, cycle {iteration}");
                ++iteration;
            }

            --iteration;

            Assert.AreEqual(outputDelta * iteration, converter.OutputCount);
            Assert.AreEqual(startInputCount - inputDelta * iteration, converter.InputCount);
            Assert.AreEqual(0, converter.InputCount);
            Assert.AreNotEqual(outputCapacity, converter.OutputCount);
            Assert.IsFalse(converter.IsFull());
        }


        [TestCase(32u, 40u, 33u, 2u, 1u, 20.0f)]
        public void StopConverterOnDeactivateImmediate(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);
            Assert.IsTrue(converter.IsRunning);
            converter.Deactivate();
            Assert.IsFalse(converter.IsRunning);
            Assert.AreEqual(0, converter.OutputCount);
        }

        [TestCase(32u, 40u, 33u, 2u, 1u, 20.0f)]
        public void StopConverterOnDeactivate(uint startInputCount, uint inputCapacity, uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);
            Assert.IsTrue(converter.IsRunning);

            Assert.AreEqual(Math.Max(startInputCount - inputDelta, 0),
                (long)converter.InputCount);
            converter.Update(cycleTime);
            Assert.AreEqual(outputDelta, converter.OutputCount);

            //resources taken - then must be returned
            converter.Update(cycleTime / 2.0f);
            converter.Deactivate();
            Assert.IsFalse(converter.IsRunning);

            converter.Update(cycleTime);
            Assert.AreEqual(outputDelta, converter.OutputCount);
            Assert.AreEqual(Math.Max(startInputCount - inputDelta, 0),
                (long)converter.InputCount);
        }

        [TestCase(10u, 40u, 20u, 1u, 2u, 15.0f)]
        public void StopConverterOnDeactivateThenStartAgainToFullOutput(uint startInputCount, uint inputCapacity,
            uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);
            Assert.IsTrue(converter.IsRunning);
            converter.Deactivate();
            Assert.IsFalse(converter.IsRunning);
            Assert.AreEqual(0, converter.OutputCount);

            Assert.IsTrue(converter.TryActivate());
            int iteration = 1;
            while (converter.IsRunning)
            {
                Assert.AreEqual(Math.Max(startInputCount - inputDelta * iteration, 0),
                    (long)converter.InputCount, $"Input, cycle {iteration}");
                converter.Update(cycleTime);
                Assert.AreEqual(outputDelta * iteration, converter.OutputCount, $"Output, cycle {iteration}");
                ++iteration;
            }

            --iteration;

            Assert.AreEqual(outputCapacity, converter.OutputCount);
            Assert.AreEqual(startInputCount - inputDelta * iteration, converter.InputCount);
            Assert.IsTrue(converter.IsFull());
        }

        [TestCase(20u, 40u, 20u, 10u, 2u, 15.0f, 30u)]
        public void TrimExcessAddedConverterResourcesAfterDeactivate(uint startInputCount, uint inputCapacity,
            uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime, uint addedResources)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            converter.AddResources(startInputCount);
            Assert.IsTrue(converter.IsRunning);
            converter.Update(cycleTime / 2.0f);
            converter.AddResources(addedResources);
            converter.Deactivate();
            Assert.AreEqual(Math.Min(startInputCount + addedResources, inputCapacity), converter.InputCount);
        }

        [TestCase(0u, 40u, 20u, 10u, 2u, 15.0f)]
        public void WhenConverterEmptyCantStart(uint startInputCount, uint inputCapacity,
            uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            Assert.IsFalse(converter.IsRunning);
            Assert.IsFalse(converter.TryActivate());
        }

        [TestCase(20u, 40u, 20u, 10u, 2u, 15.0f, 30u)]
        public void StartConverterAfterAddingResourcesToEmpty(uint startInputCount, uint inputCapacity,
            uint outputCapacity,
            uint inputDelta,
            uint outputDelta, float cycleTime, uint addedResources)
        {
            Converter converter
                = new Converter(inputCapacity, outputCapacity, inputDelta, outputDelta, cycleTime);

            Assert.IsFalse(converter.IsRunning);
            converter.AddResources(addedResources);
            Assert.IsTrue(converter.IsRunning);
        }
    }
}