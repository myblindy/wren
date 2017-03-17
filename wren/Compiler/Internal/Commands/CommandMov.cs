using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;

namespace wren.Compiler.Internal.Commands
{
    partial class CommandMov : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (Operand1Register?.Immediate == false && memory[Operand1Register, false] != null)
                state = CommandState.WaitOnWrite;
            else if (Operand2Register?.Immediate == false && memory[Operand2Register, false] == null)
                state = CommandState.WaitOnRead;
            else
            {
                memory[Operand1Register] = Operand2Register == null ? Operand2Number : memory[Operand2Register];
                vm.MoveToNextCommand();
                state = CommandState.Normal;
            }
        }
    }
}
