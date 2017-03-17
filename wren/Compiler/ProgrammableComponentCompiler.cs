using Eto.Parse;
using Eto.Parse.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;

namespace wren.Compiler
{
    public partial class ProgrammableComponentCompiler
    {
        public WrenVM VM { get; }

        public ProgrammableComponentCompiler() => VM = new WrenVM(this);

        public ProgrammableComponentCompiler(string code) : this() => LoadCode(code);

        public List<CommandBase> Commands { get; } = new List<CommandBase>();

        static private Grammar grammar = null;
        static private Grammar Grammar
        {
            get
            {
                if (grammar == null)
                    grammar = BuildGrammar();
                return grammar;
            }
        }

        public void LoadCode(string code)
        {
            Commands.Clear();

            string line;
            using (var stream = new StringReader(code))
                while ((line = stream.ReadLine()) != null)
                    ParseCommand(line);

            VM.Reset();
        }
    }
}
