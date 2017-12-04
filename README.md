# C-Unity-Pooling
C# Unity Pooling

  using Newb.GJ.Pooling.Component;
  Pool<PoolableSampleComponent> pool = new Pool<PoolableSampleComponent>();
  PoolableSampleComponent original;
  int min = 1, max = 5;
  pool.Prepare(original, min, max); // initialize pool
  pool.Get(); // get component in the pool
  pool.Free(poolable); // free and add to the pool
