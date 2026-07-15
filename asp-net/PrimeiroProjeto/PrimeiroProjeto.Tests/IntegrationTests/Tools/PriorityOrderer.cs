using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace PrimeiroProjeto.Tests.IntegrationTests.Tools
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases
            <TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var sortedMethods = testCases.OrderBy(tc =>
            {
                var attr = tc.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute))
                    .FirstOrDefault();

                if (attr == null) return 0;

                // Tenta obter o argumento posicional do construtor (geralmente o primeiro, índice 0)
                var constructorArgs = attr.GetConstructorArguments();
                if (constructorArgs != null && constructorArgs.Any())
                {
                    return (int)constructorArgs.First();
                }

                // Caso tenha sido passado como argumento nomeado
                return attr.GetNamedArgument<int>("Priority");
            });

            return sortedMethods;
        }
    }
    [AttributeUsage(AttributeTargets.Method,
        AllowMultiple =false)]
    public class TestPriorityAttribute : Attribute
    {
        public int Priority { get;}
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
