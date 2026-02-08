using System.Collections.Generic;
using Bogus;

namespace DotExtensions.Benchmarking.Infra.FakeData;

public class FakeEnumerableHelper
{
    private readonly Faker _faker = new();

    public IEnumerable<int> Indices(int count)
    {
        IList<int> list = _faker.Make(count, () => _faker.Random.Int(min: 0));

        return list;
    }

    public IEnumerable<string> Create(int count)
    {
        IList<string> list = _faker.Make<string>(count, _faker.Address.FullAddress);

        return list;
    }
}
