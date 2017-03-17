  



using Eto.Parse;
using Eto.Parse.Parsers;
using wren.Compiler.Internal;
using wren.Compiler.Internal.Commands;
using System.Linq;
using System;

namespace wren.Compiler
{
	public partial class ProgrammableComponentCompiler
	{
		static private Grammar BuildGrammar()
		{
            // whitespace
            var ws = -Terminals.WhiteSpace;

            // terminals
            var r = Register.GetValidRegisters().Select(w => (Parser)new LiteralTerminal(w) { CaseSensitive = false }).Aggregate((a, b) => a | b);
            var i = -Terminals.Set('-') & +Terminals.Digit;
			var ri = r | i; 
			var l = +Terminals.LetterOrDigit;
			var lbldef = l.Named("lbl") & ws & ':';

			// the commands
            var command = ws & -lbldef & ws & (
																		(Terminals.Literal("mov").Named("cmd") & ws 
																				& (r.Named("op0")) & ws
																					& -Terminals.Set(',') & ws
														& (ri.Named("op1")) & ws
												)
									|					(Terminals.Literal("add").Named("cmd") & ws 
																				& (ri.Named("op0")) & ws
												)
									|					(Terminals.Literal("sub").Named("cmd") & ws 
																				& (ri.Named("op0")) & ws
												)
									|					(Terminals.Literal("mul").Named("cmd") & ws 
																				& (ri.Named("op0")) & ws
												)
									|					(Terminals.Literal("div").Named("cmd") & ws 
																				& (ri.Named("op0")) & ws
												)
									|					(Terminals.Literal("jmp").Named("cmd") & ws 
																				& (l.Named("op0")) & ws
												)
									|					(Terminals.Literal("jez").Named("cmd") & ws 
																				& (l.Named("op0")) & ws
												)
									|					(Terminals.Literal("jgz").Named("cmd") & ws 
																				& (l.Named("op0")) & ws
												)
									|					(Terminals.Literal("jlz").Named("cmd") & ws 
																				& (l.Named("op0")) & ws
												)
									|					(Terminals.Literal("slp").Named("cmd") & ws 
																				& (ri.Named("op0")) & ws
												)
								);

            // and store the grammar
            return new Grammar(command);
		}

		private void ParseCommand(string line)
		{
			if(string.IsNullOrWhiteSpace(line))
				return;

            var m = Grammar.Match(line);

	        if (m.Success)
				switch ((string)m["cmd"].Value)
				{
										case "mov":
						Commands.Add(new CommandMov(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
															, (string)m["op1"].Value
							));
						return;
										case "add":
						Commands.Add(new CommandAdd(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "sub":
						Commands.Add(new CommandSub(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "mul":
						Commands.Add(new CommandMul(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "div":
						Commands.Add(new CommandDiv(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "jmp":
						Commands.Add(new CommandJmp(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "jez":
						Commands.Add(new CommandJez(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "jgz":
						Commands.Add(new CommandJgz(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "jlz":
						Commands.Add(new CommandJlz(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
										case "slp":
						Commands.Add(new CommandSlp(
							(string)m["lbl"].Value,
															 (string)m["op0"].Value
							));
						return;
									}
            throw new InvalidOperationException("Invalid command: " + m["cmd"].Value);
		}
	}
}

// Commands boilererplate
namespace wren.Compiler.Internal.Commands
{
		public partial class CommandMov
	{
		// fields
									internal Register Operand1Register;
																		internal Register Operand2Register;
										internal int Operand2Number;
								
		// constructors
		public CommandMov(string label
							, string op1
							, string op2
					) 
		{
			Label = label;
												Operand1Register = new Register(op1);
																																				SetField(op2, out Operand2Number, out Operand2Register);
													}
	}
		public partial class CommandAdd
	{
		// fields
									internal Register Operand1Register;
										internal int Operand1Number;
								
		// constructors
		public CommandAdd(string label
							, string op1
					) 
		{
			Label = label;
																				SetField(op1, out Operand1Number, out Operand1Register);
													}
	}
		public partial class CommandSub
	{
		// fields
									internal Register Operand1Register;
										internal int Operand1Number;
								
		// constructors
		public CommandSub(string label
							, string op1
					) 
		{
			Label = label;
																				SetField(op1, out Operand1Number, out Operand1Register);
													}
	}
		public partial class CommandMul
	{
		// fields
									internal Register Operand1Register;
										internal int Operand1Number;
								
		// constructors
		public CommandMul(string label
							, string op1
					) 
		{
			Label = label;
																				SetField(op1, out Operand1Number, out Operand1Register);
													}
	}
		public partial class CommandDiv
	{
		// fields
									internal Register Operand1Register;
										internal int Operand1Number;
								
		// constructors
		public CommandDiv(string label
							, string op1
					) 
		{
			Label = label;
																				SetField(op1, out Operand1Number, out Operand1Register);
													}
	}
		public partial class CommandJmp
	{
		// fields
															internal string Operand1Label;
					
		// constructors
		public CommandJmp(string label
							, string op1
					) 
		{
			Label = label;
																								Operand1Label = op1;
									}
	}
		public partial class CommandJez
	{
		// fields
															internal string Operand1Label;
					
		// constructors
		public CommandJez(string label
							, string op1
					) 
		{
			Label = label;
																								Operand1Label = op1;
									}
	}
		public partial class CommandJgz
	{
		// fields
															internal string Operand1Label;
					
		// constructors
		public CommandJgz(string label
							, string op1
					) 
		{
			Label = label;
																								Operand1Label = op1;
									}
	}
		public partial class CommandJlz
	{
		// fields
															internal string Operand1Label;
					
		// constructors
		public CommandJlz(string label
							, string op1
					) 
		{
			Label = label;
																								Operand1Label = op1;
									}
	}
		public partial class CommandSlp
	{
		// fields
									internal Register Operand1Register;
										internal int Operand1Number;
								
		// constructors
		public CommandSlp(string label
							, string op1
					) 
		{
			Label = label;
																				SetField(op1, out Operand1Number, out Operand1Register);
													}
	}
	}