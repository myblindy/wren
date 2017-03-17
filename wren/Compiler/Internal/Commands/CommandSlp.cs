using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;

namespace wren.Compiler.Internal.Commands
{
    partial class CommandSlp : CommandBase
    {
        private uint LastTick = 0;

        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (Operand1Register?.Immediate == false && memory[Operand1Register, false] == null)
                state = CommandState.WaitOnRead;
            else
            {
                var val = Operand1Register == null ? Operand1Number : memory[Operand1Register, false];
                if (vm.Tick - LastTick >= val)
                {
                    LastTick = vm.Tick;
                    if (Operand1Register != null) val = memory[Operand1Register];   // force a clear
                    vm.MoveToNextCommand();
                    state = CommandState.Normal;
                }
                else
                    state = CommandState.Sleep;
            }
        }
    }
}
