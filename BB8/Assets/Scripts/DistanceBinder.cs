using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

[VFXBinder("Transform/LocalPosition")]
public class LocalPosition : VFXBinderBase
{
    // VFXPropertyBinding attributes enables the use of a specific
    // property drawer that populates the VisualEffect properties of a
    // certain type.
    [VFXPropertyBinding("System.Single")]
    public ExposedProperty localPosition;

    public Transform target;

    // The IsValid method need to perform the checks and return if the binding
    // can be achieved.
    public override bool IsValid(VisualEffect component)
    {
        return target != null && component.HasVector3(localPosition);
    }

    // The UpdateBinding method is the place where you perform the binding,
    // by assuming that it is valid. This method will be called only if
    // IsValid returned true.
    public override void UpdateBinding(VisualEffect component)
    {
        //var distance = target.position - transform.position;
        //component.SetVector3(distanceProperty, Vector3.Distance(transform.position, target.position));
        component.SetVector3(localPosition, target.transform.localPosition);
    }
}
