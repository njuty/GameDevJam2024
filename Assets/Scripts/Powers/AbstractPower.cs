using UnityEngine;

public abstract class AbstractPower : MonoBehaviour
{
    public string powerName;
    public Texture2D powerIcon;
    
    public abstract void Activate();
}
