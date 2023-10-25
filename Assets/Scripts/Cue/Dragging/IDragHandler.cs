using UnityEngine;

namespace Cue.Dragging
{
    public interface IDragHandler
    {
        float DragStrength { get; }
        bool IsDragging { get; }
        Vector2 DragDirection { get; }
        void BeginDrag();
        void Drag(float value);
        void EndDrag();
    }
}