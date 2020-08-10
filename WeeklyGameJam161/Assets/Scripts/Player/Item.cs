using UnityEngine;

public class Item : ScriptableObject {

    public Sprite icon;
    public string name;

    public virtual bool Use(Transform clickedObject) {
        Debug.Log("Using Item");
        return false;
    }
}
