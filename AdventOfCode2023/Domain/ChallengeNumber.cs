using System.Runtime.Serialization;
using LanguageExt.ClassInstances;
using LanguageExt.ClassInstances.Const;
using LanguageExt.ClassInstances.Pred;

namespace AdventOfCode2023.Domain;

public sealed class ChallengeNumber : NewType<ChallengeNumber, int, GreaterThan<TInt, int, I0>>
{
    public ChallengeNumber(int value) : base(value) { }

    public ChallengeNumber(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override string ToString() => base.Value.ToString("00");
}