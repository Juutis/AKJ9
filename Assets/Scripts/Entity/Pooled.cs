using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooled : MonoBehaviour
{
    private Pool pool;
    public void SetPool(Pool pool) { this.pool = pool; }
    public Pool GetPool() { return pool; }
}
