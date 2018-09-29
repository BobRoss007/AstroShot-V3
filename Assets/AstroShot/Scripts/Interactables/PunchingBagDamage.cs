using UnityEngine;

public class PunchingBagDamage : MonoBehaviour {

    [SerializeField]
    PunchingBag _punchingBagComponent;

    void OnCollisionEnter(Collision collision) {
        if(collision.rigidbody) {
            //Could use _punchingBagComponent.HealthInfo.Damage(...) but this is just an example
            HealthManager.Damage((int)(collision.relativeVelocity.magnitude * .25f), _punchingBagComponent.gameObject, collision.rigidbody.gameObject);
        }
    }
}