using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;

namespace wren.Components.Programmable
{
    public class ProgrammableComponent : ComponentBase
    {
        private ProgrammableComponentCompiler Compiler = new ProgrammableComponentCompiler();

        public override CommandState State => Compiler.VM.State;

        public ProgrammableComponent(params string[] registers) : base() => Memory = new Memory(registers);

        public void LoadCode(string code) => Compiler.LoadCode(code);

        public override bool ExecuteNextCommand() => Compiler.VM.ExecuteNextCommand(Memory);

        public override void Reset() => Compiler.VM.Reset();

        public override void NewTick() => Compiler.VM.NewTick();
    }
}
