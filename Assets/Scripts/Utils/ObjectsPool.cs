using System;
using System.Collections.Generic;

namespace Pool
{
    public class ObjectsPool<T> where T : class
    {
        private readonly Queue<T> _entities;
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

        public T PutOutEntity()
        {
            if (_entities.Count == 0)
                return null;

            T entity = _entities.Dequeue();

            PutOut?.Invoke(entity);

            return entity;
        }

        public void PutInEntity(T entity)
        {
            _entities.Enqueue(entity ?? throw new ArgumentNullException(nameof(entity)));
            PutIn?.Invoke(entity);
        }

        public void RemoveEntity() =>
            Removed?.Invoke(_entities.Dequeue());

        public void Clear()
        {
            foreach (T entity in _entities)
                Removed?.Invoke(entity);

            _entities.Clear();
        }

        private Queue<T> CreateEntities(in int count)
        {
            Queue<T> entities = new Queue<T>();

            for (int i = 0; i < count; i++)
                entities.Enqueue(_created.Invoke());

            return entities;
        }
    }
}
