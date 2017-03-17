using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;

namespace wren.Compiler.Internal.Commands
{
    public enum CommandState
    {
        WaitOnWrite,
        WaitOnRead,
        Sleep,
        SleepX,
        Normal
    }

    public abstract class CommandBase
    {
        internal string Label;

        private static bool IsNumeric(string s) => int.TryParse(s, out int val);

        protected void SetField(string val, out int numval, out Register reg)
        {
            if (IsNumeric(val))
            {
                numval = Convert.ToInt32(val);
                reg = null;
            }
            else
            {
                numval = 0;
                reg = new Register(val);
            }
        }

        public abstract void Execute(Memory memory, WrenVM vm, ref CommandState state);
    }
}
