using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wren.Components.Programmable;
using wren.Compiler.Internal;
using wren.Components.IO;
using wren.Board;
using System.Collections.Generic;

namespace wren.tests
{
    [TestClass]
    public class PCTests
    {
        [TestMethod]
        public void TwoBasicPCs()
        {
            var pc1 = new ProgrammableComponent("acc", "r0", "r1");
            pc1.LoadCode(@"
add 1
mov r0 acc");

            var pc2 = new ProgrammableComponent("acc", "r0", "r1");
            pc2.LoadCode(@"mov r1 r0");

            Memory.Connect(pc1.Memory, "r0", pc2.Memory, "r0");

            Assert.AreEqual(0, pc1.Memory["acc"]);
            Assert.AreEqual(0, pc1.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(1, pc1.Memory["acc"]);
            Assert.AreEqual(0, pc1.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r1"]);

            pc2.ExecuteNextCommand();
            Assert.AreEqual(1, pc1.Memory["acc"]);
            Assert.AreEqual(0, pc1.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(1, pc1.Memory["acc"]);
            Assert.AreEqual(1, pc1.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r0"]);
            Assert.AreEqual(0, pc2.Memory["r1"]);

            pc2.ExecuteNextCommand();
            Assert.AreEqual(1, pc1.Memory["acc"]);
            Assert.AreEqual(1, pc1.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(2, pc1.Memory["acc"]);
            Assert.AreEqual(1, pc1.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(2, pc1.Memory["acc"]);
            Assert.AreEqual(2, pc1.Memory["r0"]);
            Assert.AreEqual(2, pc2.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(3, pc1.Memory["acc"]);
            Assert.AreEqual(2, pc1.Memory["r0"]);
            Assert.AreEqual(2, pc2.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r1"]);

            pc1.ExecuteNextCommand();
            Assert.AreEqual(3, pc1.Memory["acc"]);
            Assert.AreEqual(3, pc1.Memory["r0"]);
            Assert.AreEqual(3, pc2.Memory["r0"]);
            Assert.AreEqual(1, pc2.Memory["r1"]);

            pc2.ExecuteNextCommand();
            Assert.AreEqual(3, pc1.Memory["acc"]);
            Assert.AreEqual(3, pc1.Memory["r0"]);
            Assert.AreEqual(3, pc2.Memory["r0"]);
            Assert.AreEqual(3, pc2.Memory["r1"]);
        }

        [TestMethod]
        public void BasicIOTest()
        {
            var input = new InputComponent();
            var output = new OutputComponent();
            Memory.Connect(input.Memory, "acc", output.Memory, "acc");

            var board = new DesignBoard { input, output };

            TestUtils.TestBoard(board,
                new[] { (input, new[] { 2, 5, 7, 10 }) },
                new[] { (output, new[] { 2, 5, 7, 10 }, (List<int>)null) });
        }

        [TestMethod]
        public void DoublingIOTest()
        {
            var input = new InputComponent();
            var output = new OutputComponent();

            var pc = new ProgrammableComponent("acc", "r0", "r1");
            pc.LoadCode(@"
mov r0 acc
add acc
mov acc r1
");

            Memory.Connect(input.Memory, "acc", pc.Memory, "r0");
            Memory.Connect(pc.Memory, "r1", output.Memory, "acc");

            var board = new DesignBoard { input, output, pc };

            TestUtils.TestBoard(board,
                new[] { (input, new[] { 2, 5, 7, 10 }) },
                new[] { (output, new[] { 4, 10, 14, 20 }, (List<int>)null) });
        }
    }
}
