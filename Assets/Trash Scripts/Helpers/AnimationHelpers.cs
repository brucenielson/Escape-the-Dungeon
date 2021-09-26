using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class AnimationHelpers
    {
        public static float GetAnimationTime(Animator animator, string name, bool showLogs = false)
        {
            var animation = animator.runtimeAnimatorController.animationClips.FirstOrDefault(a => a.name == name);

            if (animation != null)
            {
                if (showLogs)
                {
                    Debug.Log($"Animation '{name}' time is {animation.length}");
                }
                return animation.length;
            }
            else
            {
                if (showLogs)
                {
                    Debug.Log($"Animation '{name}' not found");
                }
                return 0.0f;
            }
        }

        // From Milestone 1 Codebase from class
        public static void DrawRay(Ray ray, float rayLength, bool hitFound, RaycastHit hit, Color rayColor, Color hitColor)
        {

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, rayColor);

            if (hitFound)
            {
                //draw an X that denotes where ray hit
                const float ZBufFix = 0.01f;
                const float edgeSize = 0.2f;
                Color col = hitColor;

                Debug.DrawRay(hit.point + Vector3.up * ZBufFix, Vector3.forward * edgeSize, col);
                Debug.DrawRay(hit.point + Vector3.up * ZBufFix, Vector3.left * edgeSize, col);
                Debug.DrawRay(hit.point + Vector3.up * ZBufFix, Vector3.right * edgeSize, col);
                Debug.DrawRay(hit.point + Vector3.up * ZBufFix, Vector3.back * edgeSize, col);
            }
        }

        // From Milestone 1 Codebase from class
        public static bool CheckGroundNear(Vector3 charPos,
            float jumpableGroundNormalMaxAngle,
            float rayDepth, //how far down from charPos will we look for ground?
            float rayOriginOffset, //charPos near bottom of collider, so need a fudge factor up away from there            
            out Vector3 hitLocation,
            bool showRay = false)
        {
            float totalRayLen = rayOriginOffset + rayDepth;
            Ray ray = new Ray(charPos + Vector3.up * rayOriginOffset, Vector3.down);
            int layerMask = 1 << LayerMask.NameToLayer("Default");
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(ray, out hitInfo, totalRayLen, layerMask);
            //RaycastHit groundHit = new RaycastHit();
            //foreach (RaycastHit hit in hits)
            //{
            //    if (hit.collider.gameObject.CompareTag("ground"))
            //    {a
            //        ret = true;
            //        groundHit = hit;
            //        _isJumpable = Vector3.Angle(Vector3.up, hit.normal) < jumpableGroundNormalMaxAngle;
            //        break; //only need to find the ground once
            //    }
            //}

            if (showRay)
                DrawRay(ray, totalRayLen, hit, hitInfo, Color.magenta, Color.green);

            hitLocation = hitInfo.point;
            return hit;
        }
    }


}
