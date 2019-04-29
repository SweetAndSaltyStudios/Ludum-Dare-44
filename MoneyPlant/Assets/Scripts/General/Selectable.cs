using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool FirstPlacement { get; set; } = false;
    public bool Indestuctable { get; set; } = false;

    public virtual void ChangeColor(Color newColor)
    {
       
    }
}
