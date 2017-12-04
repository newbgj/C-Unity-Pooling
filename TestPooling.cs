using System.Collections;
using UnityEngine;
using System;

public class TestPooling : MonoBehaviour
{
    private PoolSample pool = new PoolSample();

    [SerializeField] private PoolableSample m_original;
    private Newb.GJ.Pooling.Component.Pool<PoolableSample> pool_component = new Newb.GJ.Pooling.Component.Pool<PoolableSample>();


    private void Awake()
    {
        pool.Prepare(1, 5);
        pool_component.Prepare(m_original, 1, 5);
    }

    private IEnumerator Start()
    {
        PoolableSampleClass poolable = pool.Get();
        yield return new WaitForSeconds(0.25f);
        //poolable.pool.Free(poolable);
        pool.Free(poolable);

        yield return new WaitForSeconds(0.5f);

        PoolableSample poolable_component = pool_component.Get();
        yield return new WaitForSeconds(0.25f);
        //poolable.pool.Free(poolable); 
        pool_component.Free(poolable_component);
    }

}

public class PoolSample : Newb.GJ.Pooling.Pool<PoolableSampleClass>
{
    protected override PoolableSampleClass New()
    {
        return new PoolableSampleClass(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
    }
}
