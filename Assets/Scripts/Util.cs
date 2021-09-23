using UnityEngine;

static class Util {
    // Magnitude of the xz component of a 3-dimensional vector
    public static float xzMagnitude(Vector3 v) {
        return Mathf.Sqrt(v.x * v.x + v.z * v.z);
    }

    // Convert mouse position to ground location
    public static Vector3 screenToGroundPoint(Vector2 screenPoint, Camera camera) {
        Ray ray = camera.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Ground"))) {
            return hit.point;
        } else {
            return new Vector3(0, 0, 0);
        }
    }
}
