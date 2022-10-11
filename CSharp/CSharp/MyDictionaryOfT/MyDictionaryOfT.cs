using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDictionaryOfT
{
    class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int DEFAULT_SIZE = 1000;
        private TValue[] _data = new TValue[1000];
        public TValue this[TKey key]
        {
            get
            {
                return _data[Hash(key)];
            }
            set
            {
                _data[Hash(key)] = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            _data[Hash(key)] = value;
        }

        public void Remove(TKey key)
        {
            _data[Hash(key)] = default(TValue);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            try
            {
                value = _data[Hash(key)];
            }
            catch
            {
                return false;
            }

            return true;
        }


        private int Hash(TKey key)
        {
            string tmpString = key.ToString();
            int tmpHash = 0;

            for (int i = 0; i < tmpString.Length; i++)
            {
                tmpHash += tmpString[i];
            }
            tmpHash %= DEFAULT_SIZE;

            return tmpHash;
        }



        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
