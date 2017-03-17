using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;

namespace wren.Compiler.Internal.Commands
{
    partial class CommandAdd : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (Operand1Register?.Immediate == false && memory[Operand1Register, false] == null)
                state = CommandState.WaitOnRead;
            else
            {
                memory["acc"] += Operand1Register == null ? Operand1Number : memory[Operand1Register];
                vm.MoveToNextCommand();
                state = CommandState.Normal;
            }
        }
    }
}
