using UnityEngine;

namespace Cue.Dragging
{
    public interface IDragHandler
    {
        float DragStrength { get; }
        void BeginDrag(Vector2 mousePosition);
        void Drag(Vector2 mousePosition);
        void EndDrag();
    }
}