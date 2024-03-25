using System.Diagnostics;
using FluentAssertions;

namespace AdventOfCode2023Tests.Extensions;

[DebuggerStepThrough]
public static class FluentValidationExtensions
{
    public static Option<T> ShouldBeSome<T>(this Option<T> option, Action<T> assertion) =>
        option.Map(x =>
        {
            assertion(x);
            return x;
        }).IfNone(() => raise<T>(new Exception("Assertion failed")));
    
    public static Option<T> ShouldBeNone<T>(this Option<T> option) =>
        option.Map(_ => raise<T>(new Exception("Assertion failed")));
    
    public static Either<TLeft,TRight> ShouldBeLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> assertion)
    {
        return either.BiMap(
            _ => raise<TRight>(new Exception("Assertion failed")),
            l =>
            {
                assertion(l);
                return l;
            });
    }
    
    public static Either<TLeft,TRight> ShouldBeRight<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TRight> assertion)
    {
        return either.BiMap(
            r =>
            {
                assertion(r);
                return r;
            },
            _ => raise<TLeft>(new Exception("Assertion failed")));
    }
}