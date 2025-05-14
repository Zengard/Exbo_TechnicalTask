using Articy.Articybrothel;
using Articy.Articybrothel.GlobalVariables;
using Articy.Unity;
using UnityEngine;

public class TestStorage : MonoBehaviour
{
    //private Dictionary<string, Item> _itemsMap = new Dictionary<string, Item>();
    //ArticyTypeProperties properties;

    //_itemsMap.Clear();

    //// Вывести результат
    //foreach (var item in allItemEntities)
    //{
    //    Debug.Log($"Найден предмет: {item.TechnicalName} (Шаблон: {item.DisplayName})");
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
            Debug.Log("Подвеска была добавлена ");

        if (aVariable == "GameState.Count" && ((int)aValue) < 1)
            Debug.Log("Подвески нет ");

        //int test = (aValue as ArticyChangedProperty).OldValue;
    }

    private void OnGoldChanged(ArticyChangedProperty aProperty)
    {
        Debug.Log("НУ ХОТЬ ЧТО-НИБУДЬ");

        var prevGoldCount = (int)aProperty.OldValue;
        var currentGoldCount = (int)aProperty.NewValue;

        if (prevGoldCount > currentGoldCount)
            Debug.Log("Подвески нет ");

        if (prevGoldCount < currentGoldCount)
            Debug.Log("Подвеска была добавлена ");

    }
}
