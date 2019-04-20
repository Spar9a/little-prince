using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    public float CurrentHP = 100f;
    public float MaxHP = 100f;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to
    }

    private void Start() {
        // Cache since Camera.main is super slo
        mainCamera = Camera.main;
    }

    private void Update() {
        // Only display on partial health
        if (CurrentHP < MaxHP) {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        } else {
            meshRenderer.enabled = false;
        }
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", CurrentHP / MaxHP);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

}