using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;

namespace DotExtensions.Benchmarking.Infra.FakeData;

public class FakeEnumerables
{
   private Faker faker = new Faker();

    public IEnumerable<string> Create(int count)
    {
        IList<string> list = faker.Make<string>(count, faker.Address.FullAddress);

        return list;
    }
}