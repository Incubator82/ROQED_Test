using System.IO;
using UnityEditor;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public GameObject BilletPref;
    GameObject _leftParititionBillet, _rigthParititionBillet;

    private void Awake()
    {
        _leftParititionBillet = GameObject.Find("PartitionLeft");
        _rigthParititionBillet = GameObject.Find("PartitionRigth");
    }
    public void LoadJsonFromFile()
    {
        string path = EditorUtility.OpenFilePanel("Выберете файл Json", "", "json");
        if (path.Length != 0)
        {
            var dataFromStartJson = LoadJson(path);

            BilletManagerScr.LoadBilletManagerData(dataFromStartJson, true);

            foreach (Transform child in _leftParititionBillet.transform)
                Destroy(child.gameObject);

            foreach (Transform child in _rigthParititionBillet.transform)
                Destroy(child.gameObject);

            foreach (var item in BilletManager.BilletsLeftList)
            {
                GameObject billetObj = Instantiate(BilletPref, _leftParititionBillet.transform, false);
                billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            }

            foreach (var item in BilletManager.BilletsRigthList)
            {
                GameObject billetObj = Instantiate(BilletPref, _rigthParititionBillet.transform, false);
                billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            }
        }
    }

    public void SaveJsonToFile()
    {
        JsonModel jsonModel = new JsonModel();

        foreach (var item in BilletManager.BilletsLeftList)
            jsonModel.LeftParititionBillets.Add(new LeftParititionBillets() { Text = item.Text, Number = item.Number });

        foreach (var item in BilletManager.BilletsRigthList)
            jsonModel.RigthParititionBillets.Add(new RigthParititionBillets() { Text = item.Text, Number = item.Number });

        string json = JsonUtility.ToJson(jsonModel);

        var path = EditorUtility.SaveFilePanel(
            "Сохранить файл в формате JSON",
            "",
            "outJson.json",
            "json");

        if (path.Length != 0)
            System.IO.File.WriteAllText(path, json);
    }

    private JsonModel LoadJson(string path) => JsonUtility.FromJson<JsonModel>(File.ReadAllText(path));
}
