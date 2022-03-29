using UnityEngine;

namespace HexedHeroes.Utils
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasController : Singleton<CanvasController>
    {
        public float ScaleFactor => canvas.scaleFactor;
        
        private Canvas canvas;

        protected override void OnAwake()
        {
            base.OnAwake();
            canvas = GetComponent<Canvas>();
        }
    }
}
