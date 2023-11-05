﻿using Balls;
using Medicine;
using UnityEngine;

namespace Cue.Dragging
{
    public class CueTouchDrag : MonoBehaviour, IDragHandler
    {
        [Header("Drag Settings")] 
        [SerializeField] private float minDrag = 0.25f;
        
        [Inject.Single] private WhiteBall WhiteBall { get; }
        
        public float DragStrength { get; private set; }
        public bool IsDragging { get; private set; }
        public Vector2 DragDirection { get; private set; }

        private void OnEnable()
        {
            EndDrag();
        }

        public void BeginDrag()
        {
            IsDragging = true;
            DragDirection = transform.right;
        }

        public void Drag(float value)
        {
            DragStrength = value;
        }

        public void EndDrag()
        {
            Drag(minDrag);
            IsDragging = false;
            DragStrength = minDrag;
        }
    }
}