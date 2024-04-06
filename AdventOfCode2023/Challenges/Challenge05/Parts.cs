using System.Collections.ObjectModel;
using LanguageExt.ClassInstances;

namespace AdventOfCode2023.Challenges.Challenge05;

interface IFarmValue { int Value { get; } }

interface IFarmValue<T> : IFarmValue, IEquatable<T>;

abstract class FarmType<T>(int value)
    : NumType<T, TInt, int>(value) where T : NumType<T, TInt, int>
    , IFarmValue<T>
{
    public new int Value { get; } = value;
}

public class FarmMaterial(string name) : NewType<FarmMaterial, string>(name);
public readonly record struct MaterialValue(FarmMaterial Material, int Value);

public class MaterialRange(FarmMaterial material, int start, int count)
{
    private IntegerRange _range = IntegerRange.FromMinMax(start, start + count - 1, 1);
    public FarmMaterial Material => material;
    public int Start => _range.From;
    public int End => _range.To;

    public Option<MaterialValue> MaterialAtIndex(int index) => 
        Optional(_range.ElementAtOrDefault(index)).Map(v => new MaterialValue(material, v));

    public Option<int> IndexOf(MaterialValue material)
    {
        if (material.Material != this.Material) throw new ArgumentException("checked for wrong material");
        if (!_range.InRange(material.Value)) return None;
        int idx = 0;
        foreach (var item in _range)
        {
            if (item == material.Value)
                return idx;
            
            idx++;
        }
        return None;
    }
}


public record MaterialMap(MaterialRange Source, MaterialRange Destination)
{
    private Func<MaterialValue, Option<MaterialValue>> _map = material =>
        Source.IndexOf(material)
            .Bind(v => Destination.MaterialAtIndex(v));
            // .Match(some =>
            // {
            //     // Console.WriteLine($"Found {some} for {material}");
            //     return Some(some);
            // }, () =>
            // {
            //     // Console.WriteLine($"Found none for {material}");
            //     return None;
            // });

    public Option<MaterialValue> MapToNextMaterial(MaterialValue from) => _map(from);

    public MaterialMap LinkTo(MaterialMap other)
    {
        // these nulls are a sign of bad design. probably should have stuck with
        // functions
        return new(null!, null!)
        {
            _map = item =>
                _map(item).Bind(to => other._map(to))
        };
    }
}

public delegate Option<MaterialValue> MapMaterialDelegate(MaterialValue materialValue);
public class MaterialMapCollection
{
    public FarmMaterial Source { get; }
    public FarmMaterial Target { get; }

    private static readonly IComparer<MaterialMap> Comparer =
        Comparer<MaterialMap>.Create((a, b) => a.Source.Start.CompareTo(b.Source.Start));

    private readonly SortedSet<MaterialMap> _maps = new(Comparer);

    private readonly Func<MaterialValue, Option<MaterialValue>> _map;

    public MaterialMapCollection(FarmMaterial source, FarmMaterial target,
        IEnumerable<MaterialMap> maps)
    {
        Source = source;
        Target = target;
        _map = mv =>
            mv.Material != Source
                ? throw new ArgumentOutOfRangeException(nameof(mv), $"material {mv} is not the same as source {Source}")
                : _maps.Select(m => m.MapToNextMaterial(mv)).DefaultIfEmpty(None).First();

        maps.Iter(x => _maps.Add(x));
    }


    public Option<MaterialValue> MapValue(MaterialValue from) => _map(from);
    
    /// <returns>the current instance</returns>
    /// <exception cref="ArgumentOutOfRangeException">if you try to add a map that isn't of the same source and target</exception>
    public MaterialMapCollection Add(MaterialMap map)
    {
        if (map.Source.Material != Source || map.Destination.Material != Target)
            throw new ArgumentOutOfRangeException(nameof(map), "wrong source or target");
        // skip checking for duplicates/bad data
        _maps.Add(map);
        return this;
    }

    public MaterialMapFunc Link(MaterialMapCollection other)
    {
        if (other.Source != this.Target) throw new ArgumentException("source and target don't match");

        return new MaterialMapFunc
        {
            FarmMaterials = (this.Source, other.Target),
            MapFunc = mv => this.MapValue(mv).Bind(mv2 => other.MapValue(mv2))
        };
    }

    public MaterialMapFunc ToMapFunc() => new ()
    {
        FarmMaterials = (this.Source, this.Target),
        MapFunc = mv => _map(mv)
    };
}

public record MaterialMapFunc
{
    public required (FarmMaterial Source, FarmMaterial Destination) FarmMaterials { get; set; }
    public required MapMaterialDelegate MapFunc { get; set; }

    public MaterialMapFunc LinkTo(MaterialMapFunc next) =>
        new()
        {
            FarmMaterials = (FarmMaterials.Source, next.FarmMaterials.Destination),
            MapFunc = mv => MapFunc(mv).Bind(x => next.MapFunc(x)).IfNone(() => mv with { Material = next.FarmMaterials.Destination })
        };
}

static class Parts
 {
     public static int Part1(IChallenge05Input input) => new Parser().Parse(input)
         .Apply(data =>
         {
             var combinedMap = LinkAll(data.maps);
             var all = data.seeds.Map(seed => (seed, combinedMap.MapFunc(seed).IfNone(seed)));
             var min = all
                 .MinBy(t => t.Item2.Value)
                 .Item2.Value;

             return min;
         });

     private static MaterialMapFunc LinkAll(Seq<MaterialMapCollection> dataMaps)
     {
         MaterialMapFunc mapFunc = dataMaps.Head.ToMapFunc();
         dataMaps = dataMaps.Tail;
         while (dataMaps.Length > 0)
         {
             var next = dataMaps.Head;
             foreach (var map in dataMaps)
             {
                 if (map.Source == mapFunc.FarmMaterials.Destination)
                 {
                     mapFunc = mapFunc.LinkTo(next.ToMapFunc());
                     break;
                 }
             }

             dataMaps = dataMaps.Tail;
         }

         return mapFunc;
     }
 }


// class Seed(int value) :FarmType<Seed>(value), IFarmValue<Seed>;
// class Soil(int value) :FarmType<Soil>(value), IFarmValue<Soil>;
// class Fertilizer(int value) :FarmType<Fertilizer>(value), IFarmValue<Fertilizer>;
// class Water(int value) :FarmType<Water>(value), IFarmValue<Water>;
// class Light(int value) :FarmType<Light>(value), IFarmValue<Light>;
// class Temperature(int value) :FarmType<Temperature>(value), IFarmValue<Temperature>;
// class Humidity(int value) :FarmType<Humidity>(value), IFarmValue<Humidity>;
// class Location(int value) :FarmType<Location>(value), IFarmValue<Location>;
//
// class FarmRange<T> : Range<FarmRange<T>, TInt, int>
//     where T : IFarmValue<T>
// {
//     public FarmRange<T> New(int start, int count) => new (start, start + count - 1, 1);
//     protected FarmRange(int from, int to, int step) : base(from, to, step) { }
//
//     public Option<int> IndexOf(T item)
//     {
//         if (!InRange(item.Value)) return Option<int>.None;
//         int i = 0;
//         foreach (var x in this)
//         {
//             i++;
//             if (x.Equals(item.Value))
//                 return i;
//         }
//
//         return Option<int>.None;
//     }
// }
//
// class FarmMap<TFrom, TTo>(FarmRange<TFrom> source, FarmRange<TTo> destination)
//     where TFrom : IFarmValue<TFrom>
//     where TTo : FarmType<TTo>, IFarmValue<TTo>
// {
//     private Func<TFrom, TTo> _map = (item) =>
//         source.IndexOf(item)
//             .Bind(idx => Optional(destination.ElementAtOrDefault(idx)))
//             .IfNone(item.Value)
//             .Apply(v => FarmType<TTo>.New(v));
//
//     public TTo Map(TFrom item) => _map(item);
//         
//
//     public FarmMap<TFrom, A> LinkTo<A>(FarmMap<TTo, A> next)
//         where A : FarmType<A>, IFarmValue<A> =>
//         // these nulls are a sign of bad design. probably should 
//         // have stuck with functions
//         new(null!, null!)
//         {
//             _map = item => this._map(item)
//                 .Apply(to => next._map(to))
//         };
// }
//
// class FarmMapCollection
// {
//     private readonly Dictionary<(Type, Type), object> _fromToMap = new();
//
//     public void Add<TFrom, TTo>(FarmMap<TFrom, TTo> map) where TFrom : IFarmValue<TFrom> where TTo : FarmType<TTo>, IFarmValue<TTo>
//     {
//         var types = (typeof(TFrom), typeof(TTo));
//         _fromToMap[types] = map;
//     }
// }
//
//
// public class Parts
// {
//     
// }