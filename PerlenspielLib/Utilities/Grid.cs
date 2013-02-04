using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielEngine
{
    class Grid<T>
    {
        private Dictionary<Tuple<int, int>, T> _contents; 

        private bool Contains(int x, int y)
        {
            var coords = new Tuple<int, int>(x, y);
            return _contents.Keys.Contains(coords);
        }

        private T GetEntry(int x, int y)
        {
            var coords = new Tuple<int, int>(x, y);
            foreach (var item in _contents)
            {
                if (item.Key.Equals(coords))
                {
                    return item.Value;
                }
            }
            return default(T);
        }

        private void SetEntry(int x, int y, T newEntry)
        {
            var coords = new Tuple<int, int>(x, y);
            foreach (var item in _contents.Where(item => item.Key.Equals(coords)))
            {
                _contents[item.Key] = newEntry;
                return;
            }
            _contents[coords] = newEntry;
        }

        public T this[int x, int y]
        {
            get { return GetEntry(x, y); }
            set { SetEntry(x, y, value); }
        }
    }
}
