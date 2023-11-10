using Elympics;
using UnityEngine;

namespace Cue.Dragging
{
    public interface IDragHandler
    {
        ElympicsFloat DragStrength { get; }
        ElympicsBool IsDragging { get; }
        Vector2 DragDirection { get; }
        void BeginDrag();
        void Drag(float value);
        void EndDrag();
    }
}