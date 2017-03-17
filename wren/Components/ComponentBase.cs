using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;

namespace wren.Components
{
    public abstract class ComponentBase
    {
        public Memory Memory;

        public abstract CommandState State { get; }

        public abstract void Reset();
        public abstract void NewTick();
        public abstract bool ExecuteNextCommand();
    }
}
