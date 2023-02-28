using UnityEngine;

public class Inputs : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    
    private void Update()
    {
        if (Input.touchCount <= 0 || Input.GetTouch(0).phase != TouchPhase.Began)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.touches[0].position);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }

        if (!hit.transform.CompareTag("Circle"))
        {
            return;
        }

        Circle temp = hit.transform.GetComponent<Circle>();
        temp.Process();
    }
}
