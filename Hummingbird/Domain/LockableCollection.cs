using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hummingbird.Domain
{
    public class LockableCollection<T> : Collection<T>
    {
        private bool _isLocked = false;
        private Guid _key;
        private object _syncLock = new object();

        public Guid Lock()
        {
            lock (_syncLock)
            {
                if (_isLocked)
                    throw new InvalidOperationException("Collection is already locked and cannot be locked again");

                _isLocked = true;
                _key = Guid.NewGuid();
            }

            return _key;
        }

        public void Add(T item, Guid key)
        {
            lock (_syncLock)
            {
                ValidateKey(key);
                base.Add(item);
            }
        }

        public new void Add(T item)
        {
            lock (_syncLock)
            {
                ValidateKey(null);
                base.Add(item);
            }
        }

        public void Insert(int index, T item, Guid key)
        {
            lock(_syncLock)
            {
                ValidateKey(key);
                Insert(index, item);
            }
        }

        public new void Insert(int index, T item)
        {
            lock (_syncLock)
            {
                ValidateKey(null);
                base.Insert(index, item);
            }
        }

        public bool Remove(T item, Guid key)
        {
            lock (_syncLock)
            {
                ValidateKey(key);
                return Remove(item);
            }
        }

        public new bool Remove(T item)
        {
            lock (_syncLock)
            {
                ValidateKey(null);
                return base.Remove(item);
            }
        }

        public void RemoveAt(int index, Guid key)
        {
            lock (_syncLock)
            {
                ValidateKey(key);
                RemoveAt(index);
            }
        }

        public new void RemoveAt(int index)
        {
            lock (_syncLock)
            {
                ValidateKey(null);
                base.RemoveAt(index);
            }
        }

        private void ValidateKey(Guid? key)
        {
            if (_isLocked && key != _key)
                throw new InvalidOperationException("Cannot add and item to a locked collection");
        }

    }
}
