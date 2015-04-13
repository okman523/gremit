using System;

namespace GrEmit.StackMutators
{
    internal class NotStackMutator : StackMutator
    {
        public override void Mutate(GroboIL il, ILInstructionParameter parameter, ref EvaluationStack stack)
        {
            CheckNotEmpty(il, stack);
            var esType = stack.Pop();
            var cliType = ToCLIType(esType);
            if(cliType != CLIType.Int32 && cliType != CLIType.Int64 && cliType != CLIType.NativeInt && cliType != CLIType.Zero)
                ThrowError(il, string.Format("Unable to perform 'not' opertation on type '{0}'", esType));
            // !zero = -1 -> native int
            stack.Push(cliType == CLIType.Zero ? typeof(IntPtr) : Canonize(esType));
        }
    }
}