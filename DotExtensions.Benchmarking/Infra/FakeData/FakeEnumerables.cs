using System.Collections.Generic;
using Bogus;

namespace DotExtensions.Benchmarking.Infra.FakeData;

public class FakeEnumerables
{
   private Faker faker = new Faker();

   public IEnumerable<int> Indices(int count)
   {
       IList<int> list = faker.Make<int>(count, ()=> faker.Random.Int(min: 0));

       return list;
   }
    public IEnumerable<string> Create(int count)
    {
        IList<string> list = faker.Make<string>(count, faker.Address.FullAddress);

        return list;
    }
}