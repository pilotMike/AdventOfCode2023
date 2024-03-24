namespace AdventOfCode2023.Domain;

public interface IChallengeObserver
{
    void Observe<TIn, TOut>(TIn input, TOut output);
}
public struct ChallengeObserver : IChallengeObserver
{
    public void Observe<TIn, TOut>(TIn input, TOut output) =>
        Console.WriteLine($"Output: {output}\nInput: {input}");
}

public struct NopObserver : IChallengeObserver
{
    public void Observe<TIn, TOut>(TIn input, TOut output) { }
}