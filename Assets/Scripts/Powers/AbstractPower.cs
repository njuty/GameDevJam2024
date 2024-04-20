using UnityEngine;

public abstract class AbstractPower : MonoBehaviour
{
    [SerializeField] protected string powerName;
    [SerializeField] protected Texture2D powerIcon;
    
    public abstract void Activate();
}
