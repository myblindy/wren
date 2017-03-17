using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;

namespace wren.Components.IO
{
    public class InputComponent : ComponentBase
    {
        public InputComponent() => Memory = new Memory("acc");

        public override CommandState State => CommandState.Sleep;

        private int value;
        public int Value
        {
            get => value;
            set => Memory["acc"] = this.value = value;
        }

        public override bool ExecuteNextCommand() => true;

        public override void NewTick()
        {
        }

        public override void Reset()
        {
        }
    }
}
