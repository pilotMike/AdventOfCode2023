using LanguageExt.ClassInstances;

namespace AdventOfCode2023.Domain;

public class MyLongRange(long from, long to, long step) : Range<MyLongRange, TLong, long>(from, to, step)
{
    
}