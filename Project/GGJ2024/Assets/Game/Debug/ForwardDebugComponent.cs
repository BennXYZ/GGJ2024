using UnityEngine;

public class ForwardDebugComponent : MonoBehaviour
{
    [SerializeField]
    private float length = 1;

    [SerializeField]
    private Color color = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * length);
    }
}
