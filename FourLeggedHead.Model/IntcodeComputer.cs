﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FourLeggedHead.Model
{
    public partial class IntcodeComputer
    {
        public IEnumerable<long> Program { get; }
        public List<long> Memory { get; private set; }
        public Queue<long> Input { get; private set; }
        public Queue<long> Output { get; private set; }
        public int InstructionPointer { get; private set; }
        public int RelativeBase { get; private set; }

        public IntcodeComputer(IEnumerable<long> program)
        {
            Program = program ?? throw new ArgumentNullException(nameof(program));
            Reset();
        }

        public IntcodeComputer(string program) : this(program?.Split(',').Select(long.Parse)) { }

        public IntcodeComputer(IEnumerable<int> program) : this(program?.Select(e => (long)e).ToArray()) { }

        public ProgramStatus RunProgram(int input) { return RunProgram((long)input); }

        public ProgramStatus RunProgram(long input) { return RunProgram(new long[] { input }); }

        public ProgramStatus RunProgram(IEnumerable<long> input)
        {
            var instructionCode = new InstructionCode(CheckInstruction(Memory[InstructionPointer]));

            if (input != null && input.Any())
            {
                foreach (var entry in input)
                {
                    Input.Enqueue(entry);
                }
            }

            while (instructionCode.Opcode != 99)
            {
                switch (instructionCode.Opcode)
                {
                    case 1:
                        OpcodeOne_AddTwoNumbers(instructionCode);
                        break;
                    case 2:
                        OpcodeTwo_MultiplyTwoNumbers(instructionCode);
                        break;
                    case 3:
                        if (!Input.Any()) return ProgramStatus.WaitingForInput;
                        OpcodeThree_ReadInput(instructionCode);
                        break;
                    case 4:
                        Output.Enqueue(OpcodeFour_ReturnOutput(instructionCode));
                        break;
                    case 5:
                        OpcodeFive_JumpIfTrue(instructionCode);
                        break;
                    case 6:
                        OpcodeSix_JumpIfFalse(instructionCode);
                        break;
                    case 7:
                        OpcodeSeven_LessThan(instructionCode);
                        break;
                    case 8:
                        OpcodeHeigth_AreEqual(instructionCode);
                        break;
                    case 9:
                        OpcodeNine_UpdateRelativeBase(instructionCode);
                        break;
                    default:
                        break;
                }

                instructionCode = new InstructionCode(CheckInstruction(Memory[InstructionPointer]));
            }

            return ProgramStatus.Terminated;
        }

        private static int CheckInstruction(long instruction)
        {
            return CheckMemoryValueIsInt(instruction);
        }

        private static int CheckMemoryValueIsInt(long memoryValue)
        {
            return checked((int)memoryValue);
        }

        public void FixInputMemory(int position, int value)
        {
            Memory[position] = value;
        }

        public void Reset()
        {
            Memory = new List<long>(Program.Select(e => (long)e));
            Input = new Queue<long>();
            Output = new Queue<long>();
            InstructionPointer = 0;
            RelativeBase = 0;
        }

        public override string ToString()
        {
            return string.Join("\t", Memory);
        }

        public string PrintOutput()
        {
            return $"Output: ({string.Join(',', Output.ToArray()).Trim(',')})";
        }
    }
}
