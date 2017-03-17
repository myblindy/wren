using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wren.Compiler.Internal
{
    public enum RegisterType
    {
        R0, R1, R2, R3, RLast = R3,
        X0, X1, X2, X3, X4, X5, X6, X7, XLast = X7,
        ACC,
    }

    public class Register
    {
        public RegisterType RegisterType { get; }

        public bool Immediate => RegisterType < RegisterType.X0 || RegisterType > RegisterType.XLast;

        public int? DefaultValue => Immediate ? 0 : new int?();

        public Register(string reg) =>
            RegisterType = (RegisterType)Enum.Parse(typeof(RegisterType), reg, true);

        public Register(RegisterType reg) => RegisterType = reg;

        public override bool Equals(object obj) => RegisterType == (obj as Register)?.RegisterType;

        public override int GetHashCode() => RegisterType.GetHashCode();

        public static string[] GetValidRegisters() => Enum.GetNames(typeof(RegisterType));
    }
}
