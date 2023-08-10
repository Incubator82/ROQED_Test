using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using System.IO;
public struct Billet
{
    public string Text;
    public int Number;

    public Billet(string text, int number)
    {
        Text = text;
        Number = number;
    }
}

public static class BilletManager
{
    public static List<Billet> BilletsLeftList = new List<Billet>();
    public static List<Billet> BilletsRigthList = new List<Billet>();
}
public class BilletManagerScr : MonoBehaviour
{
    public TextMeshProUGUI TextLeftParitition;
    public TextMeshProUGUI TextRigthParitition;
    public Transform LeftParititionBillet, RigthParititionBillet;
    public GameObject BilletPref;
    private void Awake()
    {
    /*
        //BilletManager.BilletsLeftList.Add(new Billet() { Text = "Test11", Number = 11 });
        //BilletManager.BilletsLeftList.Add(new Billet() { Text = "Test22", Number = 22 });
        //BilletManager.BilletsLeftList.Add(new Billet() { Text = "Test33", Number = 33 });
        //BilletManager.BilletsRigthList.Add(new Billet() { Text = "Test44", Number = 44 });
        //BilletManager.BilletsRigthList.Add(new Billet() { Text = "Test55", Number = 55 });
        //BilletManager.BilletsRigthList.Add(new Billet() { Text = "Test66", Number = 66 });
        //BilletManager.BilletsRigthList.Add(new Billet() { Text = "Test77", Number = 77 });
        BilletManager.BilletsLeftList.Add(new Billet("sest11", 11));
        BilletManager.BilletsLeftList.Add(new Billet("gest22", 22));
        BilletManager.BilletsLeftList.Add(new Billet("uest33", 33));
        BilletManager.BilletsRigthList.Add(new Billet("oest44", 44));
        BilletManager.BilletsRigthList.Add(new Billet("kest55", 55));
        BilletManager.BilletsRigthList.Add(new Billet("qest66", 66));
        BilletManager.BilletsRigthList.Add(new Billet("Test77", 77));
        */
    }

    void Start()
    {
        var dataFromStartJson = LoadStartJson();

        LoadBilletManagerData(dataFromStartJson);

        foreach (var item in BilletManager.BilletsLeftList)
        {
            GameObject billetObj = Instantiate(BilletPref, LeftParititionBillet, false);
            billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            //LeftParititionLists.Add(billetObj.GetComponent<BilletInfoScr>());
        }

        foreach (var item in BilletManager.BilletsRigthList)
        {
            GameObject billetObj = Instantiate(BilletPref, RigthParititionBillet, false);
            billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            //RigthParititionLists.Add(billetObj.GetComponent<BilletInfoScr>());
        }
        /*
            foreach (var item in BilletManager.BilletsLeftList)
            {
                GameObject billetObj = Instantiate(BilletPref, LeftParititionBillet, false);
                billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            }

            foreach (var item in BilletManager.BilletsRigthList)
            {
                GameObject billetObj = Instantiate(BilletPref, RigthParititionBillet, false);
                billetObj.GetComponent<BilletInfoScr>().ShowBilletInfoScr(item);
            }
            */
    }

    void Update()
    {
        TextLeftParitition.text = $"Первый список: актуальное количество элементов = {BilletManager.BilletsLeftList.Count()}";
        TextRigthParitition.text = $"Второй список: актуальное количество элементов = {BilletManager.BilletsRigthList.Count()}";
    }

    private JsonModel LoadStartJson() => JsonUtility.FromJson<JsonModel>(File.ReadAllText(Application.dataPath + "/Resources/Json/start.json"));

    public static void LoadBilletManagerData(JsonModel jsonModel, bool clear = false)
    {
        if (clear)
        {
            BilletManager.BilletsLeftList.Clear();
            BilletManager.BilletsRigthList.Clear();
        }
        foreach (var item in jsonModel.LeftParititionBillets)
            BilletManager.BilletsLeftList.Add(new Billet() { Text = item.Text, Number = item.Number });

        foreach (var item in jsonModel.RigthParititionBillets)
            BilletManager.BilletsRigthList.Add(new Billet() { Text = item.Text, Number = item.Number });
    }
}
