using System.Collections.Generic;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A system for batch animation of fading sprites.
    /// </summary>
    public class FadingSpriteSystem : MonoBehaviour
    {
        void Update()
        {
            foreach (var c in FadingSprite.Instances)
            {
                if (c == null) continue; // Evita errores si el objeto ha sido destruido

                if (c.gameObject.activeSelf)
                {
                    c.alpha = Mathf.SmoothDamp(c.alpha, c.targetAlpha, ref c.velocity, 0.1f, 1f);
                    c.spriteRenderer.color = new Color(1, 1, 1, c.alpha);
                }
            }
        }
    }
}