using UnityEngine;

public abstract class CustomBehaviour : MonoBehaviour
{
    public virtual void Initialize()
    {
        
    }
}

public abstract class CustomBehaviour<T> : MonoBehaviour
{
    public T CachedComponent { get; private set; }
    public virtual void Initialize(T _cachedComponent)
    {
        CachedComponent = _cachedComponent;
    }
}
