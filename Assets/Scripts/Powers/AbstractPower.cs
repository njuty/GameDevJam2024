using UnityEngine;

public abstract class AbstractPower : MonoBehaviour
{
    [SerializeField] protected string powerName;
    [SerializeField] protected Texture2D powerIcon;
    
    protected abstract void Activate();
}
