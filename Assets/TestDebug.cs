using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestDebug : MonoBehaviour
{
    public List<VGO> objects = new List<VGO>();
    // Start is called before the first frame update
    void Start()
    {
        objects.Add(VOFactory.Create(VOType.VGO1) as VGO);
        objects.Add(VOFactory.Create(VOType.VGO2) as VGO);
        List<DerivedVGO1> vgo1s = objects.OfType<DerivedVGO1>().ToList();
        foreach (DerivedVGO1 vgo1 in vgo1s)
        {
            Debug.Log("Retrieving vgo1: " + vgo1.derivedField);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public interface ITest
{
    public virtual void Init(string name)
    {

    }
}

public class VGO : ITest
{
    public virtual void Init(string name)
    {
        Debug.Log("base vgo init");
    }
}

public class DerivedVGO1 : VGO
{
    public string derivedField = "test derived field";
    public override void Init(string name)
    {
        Debug.Log("Derived 1 vgo init");
        base.Init(name);
    }
}

public class DerivedVGO2 : VGO
{
    public override void Init(string name)
    {
        Debug.Log("Derived 2 vgo init");
        base.Init(name);
    }
}
public class VOFactory
{

    public static VGO Create(VOType objectType)
    {
        switch (objectType)
        {
            case VOType.VGO1:
                {
                    VGO testObj = new DerivedVGO1();
                    testObj.Init(objectType.ToString());
                    return testObj;
                }
            case VOType.VGO2:
                {
                    VGO test = new DerivedVGO2();
                    test.Init(objectType.ToString());
                    return test;
                }
            default:
                throw new System.Exception("BAD OBJECT TYPE");
        }
    }
}

public enum VOType
{
    VGO1 = 1,
    VGO2 = 2,
}