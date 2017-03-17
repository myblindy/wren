using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal.Commands;

namespace wren.Compiler.Internal
{
    public class WrenVM
    {
        private ProgrammableComponentCompiler PCC;

        internal CommandState State;

        public int CurrentCommand { get; private set; }

        public uint Tick { get; private set; }

        internal void MoveToLabel(string lbl)
        {
            for (var cmd = 0; cmd < PCC.Commands.Count; ++cmd)
                if (PCC.Commands[cmd].Label == lbl)
                {
                    CurrentCommand = cmd;
                    return;
                }

            throw new ArgumentException("Invalid label: " + lbl);
        }

        public WrenVM(ProgrammableComponentCompiler pcc)
        {
            PCC = pcc;
            Reset();
        }

        public bool ExecuteNextCommand(Memory memory)
        {
            if (CurrentCommand >= PCC.Commands.Count && PCC.Commands.Count == 0)
                return false;
            else if (CurrentCommand >= PCC.Commands.Count)
                CurrentCommand = 0;

            PCC.Commands[CurrentCommand].Execute(memory, this, ref State);

            return true;
        }

        public void MoveToNextCommand() => ++CurrentCommand;

        public void NewTick() => ++Tick;

        public void Reset()
        {
            Tick = 1;
            CurrentCommand = 0;
            State = CommandState.Normal;
        }
    }
}
