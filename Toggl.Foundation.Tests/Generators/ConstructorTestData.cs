﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Toggl.Foundation.Tests.Generators
{
    public abstract class ConstructorTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> data;

        protected ConstructorTestData(int parameterCount)
        {
            var iterationCount = (int)Math.Pow(2, parameterCount) - 1;

            data = Enumerable
                .Range(0, iterationCount)
                .Select(outerIndex =>
                    Enumerable
                        .Range(0, parameterCount)
                        .Select(innerIndex => (object)((outerIndex & (1 << innerIndex)) != 0))
                        .ToArray()
                ).ToList();
        }

        public IEnumerator<object[]> GetEnumerator() => data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TwoParameterConstructorTestData : ConstructorTestData
    {
        public TwoParameterConstructorTestData() : base(2) { }
    }

    public class ThreeParameterConstructorTestData : ConstructorTestData
    {
        public ThreeParameterConstructorTestData() : base(3) { }
    }

    public class ConstructorTestDataTests
    {
        public class TheGeneratedSequence
        {
            [Theory]
            [InlineData(typeof(TwoParameterConstructorTestData))]
            [InlineData(typeof(ThreeParameterConstructorTestData))]
            public void NeverReturnsASequenceWhereAllElementsAreTrue(Type contructorTestDataType)
            {
                var testData = Activator.CreateInstance(contructorTestDataType) as ConstructorTestData;
                testData.Any(x => x.All(y => (bool)y)).Should().BeFalse();
            }

            [Property]
            public void ReturnsAllPossiblePermutationsForTwoParameters(bool first, bool second)
            {
                if (first && second) return;

                var array = new List<object> { first, second };
                var testData = new TwoParameterConstructorTestData();
                testData.Any(x => x.SequenceEqual(array)).Should().BeTrue();
            }

            [Property]
            public void ReturnsAllPossiblePermutationsForThreeoParameters(bool first, bool second, bool third)
            {
                if (first && second && third) return;

                var array = new List<object> { first, second, third };
                var testData = new ThreeParameterConstructorTestData();
                testData.Any(x => x.SequenceEqual(array)).Should().BeTrue();
            }
        }
    }
}
