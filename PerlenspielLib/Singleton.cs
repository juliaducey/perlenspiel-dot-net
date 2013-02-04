using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielLib
{
    public sealed class Singleton<T> where T : class, new()
    {
        public static readonly T Instance = new T();
    }
}
