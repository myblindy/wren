using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wren.Compiler.Internal
{
    public class Memory
    {
        private class MemoryLocation
        {
            internal int? Value;
            internal List<(Memory memory, Register register)> Connections { get; } = new List<(Memory memory, Register register)>();
        }

        private Dictionary<Register, MemoryLocation> Registers;

        public Memory(params Register[] registers) { Registers = registers.ToDictionary(w => w, w => new MemoryLocation { Value = w.DefaultValue }); }
        public Memory(params string[] registers) { Registers = registers.Select(w => new Register(w)).ToDictionary(w => w, w => new MemoryLocation { Value = w.DefaultValue }); }

        public int? this[Register reg, bool clearx = true]
        {
            get
            {
                // if connected, reading gets the value from the other end
                var location = Registers[reg];
                var val = location.Value;

                // if not immediate, reading clears the value
                if (!reg.Immediate && clearx)
                    location.Value = null;

                return val;
            }
            set
            {
                var location = Registers[reg];
                location.Value = value;

                // also update the connections if any
                foreach (var conn in location.Connections)
                    conn.memory.Registers[conn.register].Value = value;
            }
        }

        public int? this[string reg, bool clearx = true]
        {
            get => this[new Register(reg), clearx];
            set => this[new Register(reg), clearx] = value;
        }

        public static void Connect(Memory m1, Register r1, Memory m2, Register r2)
        {
            var loc1 = m1.Registers[r1];
            var tuple1 = (m2, r2);
            if (!loc1.Connections.Contains(tuple1))
                loc1.Connections.Add(tuple1);

            var loc2 = m1.Registers[r2];
            var tuple2 = (m1, r1);
            if (!loc2.Connections.Contains(tuple2))
                loc2.Connections.Add(tuple2);
        }

        public static void Connect(Memory m1, string r1, Memory m2, string r2) =>
            Connect(m1, new Register(r1), m2, new Register(r2));

        public static void Disconnect(Memory m1, Register r1, Memory m2, Register r2)
        {
            m1.Registers[r1].Connections.Remove((m2, r2));
            m2.Registers[r2].Connections.Remove((m1, r1));
        }

        public static void Disconnect(Memory m1, string r1, Memory m2, string r2) =>
            Disconnect(m1, new Register(r1), m2, new Register(r2));
    }
}
