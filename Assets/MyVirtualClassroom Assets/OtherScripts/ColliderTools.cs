using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxHitSide { NONE, TOP, BOTTOM, FORWARD, BACK, LEFT, RIGHT }

public class ColliderTools
{
    /// <summary>
    /// ref: https://answers.unity.com/questions/339532/how-can-i-detect-which-side-of-a-box-i-collided-wi.html
    /// </summary>
    /// <param name="anObj1"></param>
    /// <param name="anObj2"></param>
    /// <returns></returns>
    public static BoxHitSide GetHitSide(GameObject anObj1, GameObject anObj2)
    {
        //BoxHitSide hitSide = BoxHitSide.NONE;

        BoxHitSide hitDirection = BoxHitSide.NONE;
        RaycastHit MyRayHit;
        Vector3 direction = (anObj1.transform.position - anObj2.transform.position).normalized;
        Ray MyRay = new Ray(anObj2.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {

                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.up) { hitDirection = BoxHitSide.TOP; }
                if (MyNormal == -MyRayHit.transform.up) { hitDirection = BoxHitSide.BOTTOM; }
                if (MyNormal == MyRayHit.transform.forward) { hitDirection = BoxHitSide.FORWARD; }
                if (MyNormal == -MyRayHit.transform.forward) { hitDirection = BoxHitSide.BACK; }
                if (MyNormal == MyRayHit.transform.right) { hitDirection = BoxHitSide.RIGHT; }
                if (MyNormal == -MyRayHit.transform.right) { hitDirection = BoxHitSide.LEFT; }
            }
        }
        return hitDirection;
    }

    /// <summary>
    /// Calculation of the bounds of the inserted object with childs
    /// It will return a new bounds data based the number of childs and their size in the
    /// parentObject
    /// </summary>
    /// <param name="parentObject">The parent object to be get a bounds data</param>
    /// <returns>The calculated bound data of the parentObject</returns>
    public static Bounds GetChildRendererBounds(GameObject parentObject)
    {
        Renderer[] childRenderes = parentObject.GetComponentsInChildren<Renderer>();
        if (childRenderes.Length > 0)
        {
            Bounds bounds = childRenderes[0].bounds;
            for (int i = 1, ni = childRenderes.Length; i < ni; i++)
                bounds.Encapsulate(childRenderes[i].bounds);
            return bounds;
        }
        else
            return new Bounds();
    }
}
