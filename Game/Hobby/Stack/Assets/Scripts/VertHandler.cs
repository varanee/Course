using UnityEngine;
// This scripts follows the target object on the straight line
// It is executed in edit mode

[ExecuteInEditMode]
public class VertHandler : MonoBehaviour {

	// the object to follow
	public Transform follow;

	// max allowed distance
	public float maxDistance = 2;

	void Update() {
		// change this object position only if the distance is greater than maxDistance
		float actualDistance = Vector3.Distance(this.transform.position, follow.position);
		if (actualDistance > maxDistance) {

			// compute the normalized diff vector
			var followToCurrent = (transform.position - follow.position).normalized;

			// scale it to maxDistance
			followToCurrent.Scale(new Vector3(maxDistance, maxDistance, maxDistance));

			// set the new position
			transform.position = follow.position + followToCurrent;
		}
	}

}