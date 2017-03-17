using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Board;
using wren.Components.IO;

namespace wren.tests
{
    static class TestUtils
    {
        internal static void TestBoard(DesignBoard board,
            (InputComponent component, int[] values)[] InputValues,
            (OutputComponent component, int[] expectedvalues, List<int> actualvalues)[] OutputValues)
        {
            var tickcount = InputValues.Max(w => w.values.Length);

            if (tickcount != InputValues.Min(w => w.values.Length))
                throw new ArgumentException("Input values don't have the same number of items");
            if (tickcount != OutputValues.Min(w => w.expectedvalues.Length) || tickcount != OutputValues.Max(w => w.expectedvalues.Length))
                throw new ArgumentException("Not enough output values");

            board.Reset();

            for (int tick = 0; tick < tickcount; ++tick)
            {
                // set up inputs
                foreach (var item in InputValues)
                    item.component.Value = item.values[tick];

                // run the simulation
                board.ExecuteUntilAllAsleep();

                // test outputs
                int idx = 0;
                foreach (var item in OutputValues)
                {
                    if (item.component.Value != item.expectedvalues[tick])
                        throw new InvalidOperationException($"Testing failed during tick {tick} for output {idx}, expected {item.expectedvalues[tick]}, got {item.component.Value}.");
                    else
                        item.actualvalues?.Add(item.component.Value);

                    ++idx;
                }

                // next tick
                board.NewTick();
            }
        }
    }
}
