using UnityEngine;

namespace HexedHeroes
{
    public class Kazak : MonoBehaviour
    {
        private Animator animator;
        private Transform _transform;

        private Camera cam;
        
        private void Update()
        {
            Vector3 pos = _transform.position;
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            _transform.position = new Vector3(mousePos.x, mousePos.y, pos.z);
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            _transform = transform;
            cam = Camera.main;
        }
    }
}
