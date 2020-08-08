using UnityEngine;

[CreateAssetMenu(fileName = "BoolStorage", menuName = "VariableStorage/BoolStorage")]
public class BoolStorage : ScriptableObject, ISerializationCallbackReceiver {
    
    [SerializeField] private bool initialValue;

    public bool value;
    
    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize() {
        value = initialValue;
    }
}
