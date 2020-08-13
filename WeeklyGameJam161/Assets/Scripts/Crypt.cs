using UnityEngine;

public class Crypt : MonoBehaviour {


    [SerializeField] private GameObject balloon;
    [SerializeField] private Pedestal lastPedestal;

    private bool releasedBalloon;

    
    private void Update() {
        if (!releasedBalloon && lastPedestal.IsReady() && lastPedestal.IsOrientedCorrectly()) {
            ReleaseBalloon();
        }
    }

    private void ReleaseBalloon() {
        releasedBalloon = true;
        balloon.SetActive(true);
        Debug.Log("Releasing Balloon!");
    }
}
