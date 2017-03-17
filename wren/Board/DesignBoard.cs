using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal.Commands;
using wren.Components;
using wren.Components.Programmable;

namespace wren.Board
{
    public class DesignBoard : List<ComponentBase>
    {
        public bool AllAsleep => this.All(w => w.State == CommandState.Sleep || w.State == CommandState.SleepX);

        public void ExecuteNextCommand()
        {
            foreach (var item in this)
                item.ExecuteNextCommand();
        }

        public void Reset()
        {
            foreach (var item in this)
                item.Reset();
        }

        public void NewTick()
        {
            foreach (var item in this)
                item.NewTick();
        }

        public void ExecuteUntilAllAsleep()
        {
            do
            {
                ExecuteNextCommand();
            } while (!AllAsleep);
        }
    }
}
