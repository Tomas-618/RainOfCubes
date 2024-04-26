using System;
using System.Collections.Generic;

namespace Pool
{
    public class ObjectsPool<T> where T : class
    {
        private readonly Stack<T> _entities;
        private readonly Func<T> _created;

        public ObjectsPool(Func<T> onCreating, in int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(count.ToString());

            _created = onCreating ?? throw new ArgumentNullException(nameof(onCreating));
            _entities = CreateEntities(count);
        }

        public event Action<T> PutIn;

        public event Action<T> PutOut;

        public event Action<T> Removed;

        public IReadOnlyCollection<T> Entities => _entities;

        public void PutOutEntity()
        {
            if (_entities.Count == 0)
                return;

            PutOut?.Invoke(_entities.Pop());
        }

        public void PutInEntity(T entity)
        {
            _entities.Push(entity ?? throw new ArgumentNullException(nameof(entity)));
            PutIn?.Invoke(entity);
        }
        
        public void RemoveEntity() =>
            Removed?.Invoke(_entities.Pop());

        public void Clear()
        {
            foreach (T entity in _entities)
                Removed?.Invoke(entity);

            _entities.Clear();
        }

        private Stack<T> CreateEntities(in int count)
        {
            Stack<T> entities = new Stack<T>();

            for (int i = 0; i < count; i++)
                entities.Push(_created.Invoke());

            return entities;
        }
    }
}
