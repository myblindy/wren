using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;

namespace wren.Components.IO
{
    public class OutputComponent : ComponentBase
    {
        public OutputComponent() => Memory = new Memory("acc");

        public int Value => Memory["acc"].Value;

        public override CommandState State => CommandState.Sleep;

        public override bool ExecuteNextCommand() => true;

        public override void NewTick()
        {
        }

        public override void Reset()
        {
        }
    }
}
