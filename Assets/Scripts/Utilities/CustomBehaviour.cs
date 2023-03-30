using UnityEngine;

public abstract class CustomBehaviour : MonoBehaviour
{
    public GameManager GameManager { get; private set; }
    public virtual void Initialize(GameManager _gameManager)
    {
        GameManager = _gameManager;
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
