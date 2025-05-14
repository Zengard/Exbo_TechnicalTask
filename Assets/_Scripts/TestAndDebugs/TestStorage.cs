using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using UnityEngine;

public class TestStorage : MonoBehaviour
{
    //private Dictionary<string, Item> _itemsMap = new Dictionary<string, Item>();
    //ArticyTypeProperties properties;

    //_itemsMap.Clear();

    //// ������� ���������
    //foreach (var item in allItemEntities)
    //{
    //    Debug.Log($"������ �������: {item.TechnicalName} (������: {item.DisplayName})");
    //   // itemCache[item.TechnicalName] = item.getProp("Item.StrObtained");
    //}

    private void Start()
    {
        ArticyDatabase.DefaultGlobalVariables.Notifications.AddListener("Item_*", OnItemChanged);
        ArticyDatabase.ObjectNotifications.AddListener("Item_", OnGoldChanged);
    }

    private void OnItemChanged(string aVariable, object aValue)
    {
        if (aVariable == "Item_VilyaPendant" && ((int)aValue) > 1)
            Debug.Log("�������� ���� ��������� ");

        if (aVariable == "GameState.Count" && ((int)aValue) < 1)
            Debug.Log("�������� ��� ");

        //int test = (aValue as ArticyChangedProperty).OldValue;
    }

    private void OnGoldChanged(ArticyChangedProperty aProperty)
    {
        Debug.Log("�� ���� ���-������");

        var prevGoldCount = (int)aProperty.OldValue;
        var currentGoldCount = (int)aProperty.NewValue;

        if (prevGoldCount > currentGoldCount)
            Debug.Log("�������� ��� ");

        if (prevGoldCount < currentGoldCount)
            Debug.Log("�������� ���� ��������� ");

    }
}
