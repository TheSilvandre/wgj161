using UnityEngine;

[CreateAssetMenu(fileName = "BoolStorage", menuName = "VariableStorage/FloatStorage")]
public class FloatStorage : ScriptableObject, ISerializationCallbackReceiver {
    
    [SerializeField] private float initialValue;

    public float value;
    
    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize() {
        value = initialValue;
    }
}
