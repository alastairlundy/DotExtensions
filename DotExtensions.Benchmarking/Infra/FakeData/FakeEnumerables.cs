/*
 * Copyright (c) 2025 Alastair Lundy
 *
 * SPDX-License-Identifier: MIT
 */ 

using System.Collections.Generic;
using Bogus;

namespace DotExtensions.Benchmarking.Infra.FakeData;

public class FakeEnumerables
{
    private readonly Faker _faker = new Faker();

    public IEnumerable<int> Indices(int count)
    {
        IList<int> list = _faker.Make<int>(count, () => _faker.Random.Int(min: 0));

        return list;
    }

    public IEnumerable<string> Create(int count)
    {
        IList<string> list = _faker.Make<string>(count, _faker.Address.FullAddress);

        return list;
    }
}
