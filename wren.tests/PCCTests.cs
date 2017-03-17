using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wren.Compiler;
using System.Linq;
using wren.Compiler.Internal;

namespace wren.tests
{
    [TestClass]
    public class PCCTests
    {
        [TestMethod]
        public void BasicMovTest()
        {
            var pcc = new ProgrammableComponentCompiler(@"
mov r0, 3
mov r1, 4
mov r3, r0
");

            var r0 = new Register("r0");
            var r1 = new Register("r1");
            var r2 = new Register("r2");
            var r3 = new Register("r3");
            var acc = new Register("acc");
            var memory = new Memory(r0, r1, r2, r3, acc);

            Assert.AreEqual(0, memory[r0]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(3, memory[r0]);

            Assert.AreEqual(0, memory[r1]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(4, memory[r1]);

            Assert.AreEqual(0, memory[r3]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(3, memory[r3]);
        }

        [TestMethod]
        public void BasicArithmeticTest()
        {
            var pcc = new ProgrammableComponentCompiler(@"
mov acc, 4
add 2

mov r0, 3
add r0
mul 2

mov r0, acc
");

            var memory = new Memory("r0", "r1", "r2", "r3", "acc");

            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(4, memory["acc"]);

            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(6, memory["acc"]);

            Assert.AreEqual(0, memory["r0"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(3, memory["r0"]);

            Assert.AreEqual(6, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(9, memory["acc"]);

            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(18, memory["acc"]);

            Assert.AreEqual(3, memory["r0"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(18, memory["r0"]);
        }

        [TestMethod]
        public void BasicJmpTest()
        {
            var pcc = new ProgrammableComponentCompiler(@"
e: add 2
jmp a
add 3
a: jmp e
mov acc, 0
");

            var memory = new Memory("r0", "r1", "r2", "r3", "acc");

            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(4, memory["acc"]);
        }

        [TestMethod]
        public void BasicTests()
        {
            var pcc = new ProgrammableComponentCompiler(@"
mov acc -10
l: add 2
il: jgz il
jmp l
");

            var memory = new Memory("r0", "r1", "r2", "r3", "acc");

            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-10, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-8, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-8, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-8, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-6, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-6, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-6, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-4, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-4, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-4, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(-2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(0, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
            pcc.VM.ExecuteNextCommand(memory);
            Assert.AreEqual(2, memory["acc"]);
        }
    }
}
