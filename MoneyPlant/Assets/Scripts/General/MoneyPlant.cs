using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class MoneyPlant : Selectable
    {
        private const int MAX_SEGMENTS = 10;
        private const float MIN_SEGMENT_GROW_DURATION = 2f;
        private const float MAX_SEGMENT_GROW_DURATION = 8f;

        private Transform growPointContainer;

        private SpriteRenderer[] spriteRenderers;   

        public float GrowNextSegmentDuration
        {
            get 
            {
                return Random.Range(MIN_SEGMENT_GROW_DURATION, MAX_SEGMENT_GROW_DURATION);
            }
        }

        private void Awake()
        {
            growPointContainer = transform.Find("GrowPointContainer");

            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public override void ChangeColor(Color newColor)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                if (spriteRenderers[i] == null)
                    continue;

                spriteRenderers[i].color = newColor;
            }
        }
    }
}
