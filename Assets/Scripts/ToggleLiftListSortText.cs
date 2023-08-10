using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleLiftListSortText : MonoBehaviour
{
    public Toggle selectedToggle;
    GameObject _leftParititionBillet;
    public GameObject BilletPref;

    private void Awake()
    {
        _leftParititionBillet = GameObject.Find("PartitionLeft");

        selectedToggle = GetComponent<Toggle>();
        selectedToggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChangedOccured(selectedToggle);
        });
    }
    void ToggleValueChangedOccured(Toggle toggle)
    {
        SortText(toggle.isOn);
    }

    public void SortText(bool orderBy)
    {

        BilletManager.BilletsLeftList = orderBy == true ? BilletManager.BilletsLeftList.OrderBy(_ => _.Text).ToList() : 
                                                          BilletManager.BilletsLeftList.OrderByDescending(_ => _.Text).ToList();

        foreach (Transform child in _leftParititionBillet.transform)
            Destroy(child.gameObject);

        foreach (var item in BilletManager.BilletsLeftList)
        {
            GameObject billetObj = Instantiate(BilletPref, _leftParititionBillet.transform, false);
            billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
        }

    }
}
