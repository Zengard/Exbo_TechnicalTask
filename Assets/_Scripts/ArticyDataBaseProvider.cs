using UnityEngine;
using Articy.Unity;
using System.Collections.Generic;

public class ArticyDataBaseProvider : MonoBehaviour
{
    public ArticyRef myFirstArticyModel;

    public Dictionary<int, ArticyRef> test = new Dictionary<int, ArticyRef>();


    void Start()
    {
        var techName = myFirstArticyModel.GetObject().TechnicalName;

        if(myFirstArticyModel != null )
        Debug.Log(techName);
    }
}
