using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newb.GJ.Pooling;
using System;

public class PoolableSampleClass : IPoolable<PoolableSampleClass>
{
    public IPool<PoolableSampleClass> pool { get; set; }

    private int m_value;

    public PoolableSampleClass(int value)
    {
        m_value = value;
    }

    public void OnFree()
    {
        m_value = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        Debug.Log(m_value);
    }
}


public class PoolableSample : MonoBehaviour, IPoolable<PoolableSample>
{
    public IPool<PoolableSample> pool { get; set; }

    private int m_value;

    public void OnFree()
    {
        m_value = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        gameObject.SetActive(false);

        Debug.Log(m_value);
    }
}
