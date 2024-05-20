using System;
using System.Collections.Generic;
using System.Linq;

namespace Pool
{
    public class ObjectsPool<T> where T : class
    {
        private readonly List<T> _storedEntities;

        private List<T> _allEnteties;

        public ObjectsPool(Func<T> created, in int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(count.ToString());

            _storedEntities = CreateEntities(created ?? throw new ArgumentNullException(nameof(created)), count);
            _allEnteties = _storedEntities
                .ToList();
        }

        public event Action<T> PutIn;

        public event Action<T> PutOut;

        public event Action<T> Removed;

        public IReadOnlyCollection<T> AllEntities => _allEnteties;

        public IReadOnlyCollection<T> StoredEntities => _storedEntities;

        public T PutOutEntity()
        {
            if (_storedEntities.Count == 0)
                return null;

            T entity = _storedEntities
                .First();

            _storedEntities.Remove(entity);
            PutOut?.Invoke(entity);

            return entity;
        }

        public void PutInEntity(T entity)
        {
            _storedEntities.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
            PutIn?.Invoke(entity);
        }

        public void RemoveStoredEntity()
        {
            T entity = _storedEntities
                .First();

            _storedEntities.Remove(entity);
            _allEnteties.Remove(entity);
            Removed?.Invoke(entity);
        }

        public void RemoveEntity()
        {
            T entity = _allEnteties
                .First();

            if (_storedEntities.Contains(entity))
                _storedEntities.Remove(entity);

            _allEnteties.Remove(entity);
            Removed?.Invoke(entity);
        }

        public void ClearStoredEntities()
        {
            if (_storedEntities.Count == 0)
                return;

            foreach (T entity in _storedEntities)
                Removed?.Invoke(entity);

            _allEnteties = _allEnteties
                .Except(_storedEntities)
                .ToList();

            _storedEntities.Clear();
        }

        public void ClearAllEntities()
        {
            if (_allEnteties.Count == 0)
                return;

            foreach (T entity in _allEnteties)
                Removed?.Invoke(entity);

            _storedEntities.Clear();
            _allEnteties.Clear();
        }

        private List<T> CreateEntities(Func<T> created, in int count)
        {
            List<T> entities = new List<T>();

            for (int i = 0; i < count; i++)
                entities.Add(created.Invoke());

            return entities;
        }
    }
}
