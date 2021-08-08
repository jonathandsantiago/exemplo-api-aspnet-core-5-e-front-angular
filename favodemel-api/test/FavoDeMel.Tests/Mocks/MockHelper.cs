using FavoDeMel.Domain.Common;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Tests.Mocks
{
    public static class MockHelper
    {
        public static T Obter<T>()
            where T : Entity
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static IEnumerable<T> ObterTodos<T>()
           where T : Entity
        {
            return new List<T>() { (T)Activator.CreateInstance(typeof(T)) };
        }
    }
}
