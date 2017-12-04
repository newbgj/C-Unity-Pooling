using System;
using UnityEngine;

namespace Newb.GJ.Pooling
{
    public interface IPool<T> where T : IPoolable<T>
    {
        T Get();
        void Free(T poolable);
    }
    public interface IPoolable<T> where T : IPoolable<T>
    {
        IPool<T> pool { get; set; }
        void OnFree();
    }

    public abstract class Pool<T> : IPool<T> where T : IPoolable<T>
    {
        private T[] m_pool;

        public int max { get; private set; }
        public int length { get; private set; }

        public virtual void Prepare(int min, int max)
        {
            m_pool = new T[max];

            this.max = max;

            for (int i = min - 1; i >= 0; --i)
            {
                Free(New());
            }
        }

        protected abstract T New();
        private T Old() { return m_pool[--length]; }

        public T Get()
        {
            T poolable = (length > 0) ? Old() : New();
            poolable.pool = this;

            return poolable;
        }

        public void Free(T poolable)
        {

            if (length < max)
            {
                poolable.OnFree();
                m_pool[length] = poolable;
                ++length;
            }
            else
            {
                Remove(poolable);
            }
        }

        protected virtual void Remove(T poolable)
        {
            poolable.OnFree();
        }
    }
}

namespace Newb.GJ.Pooling.Component
{
    public class Pool<T> : Pooling.Pool<T> where T : UnityEngine.Component, IPoolable<T>
    {
        private T m_original;
        public T original { get { return m_original; } }

        public void Prepare(T original, int min, int max)
        {
            m_original = original;
            Prepare(min, max);
        }

        protected override T New()
        {
            return UnityEngine.Object.Instantiate(m_original);
        }
    }
}

