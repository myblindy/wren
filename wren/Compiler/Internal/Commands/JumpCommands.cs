using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;

namespace wren.Compiler.Internal.Commands
{
    partial class CommandJmp : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            vm.MoveToLabel(Operand1Label);
            state = CommandState.Normal;
        }
    }

    partial class CommandJez : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (memory["acc"] == 0)
                vm.MoveToLabel(Operand1Label);
            else
                vm.MoveToNextCommand();
            state = CommandState.Normal;
        }
    }

    partial class CommandJgz : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (memory["acc"] > 0)
                vm.MoveToLabel(Operand1Label);
            else
                vm.MoveToNextCommand();
            state = CommandState.Normal;
        }
    }

    partial class CommandJlz : CommandBase
    {
        public override void Execute(Memory memory, WrenVM vm, ref CommandState state)
        {
            if (memory["acc"] < 0)
                vm.MoveToLabel(Operand1Label);
            else
                vm.MoveToNextCommand();
            state = CommandState.Normal;
        }
    }
}
