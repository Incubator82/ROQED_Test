using UnityEngine;
using TMPro;

public class BilletInfoScr : MonoBehaviour
{
    public Billet SelfBillet;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Number;

    public void ShowBilletInfoScr(Billet billet)
    {
        SelfBillet = billet;

        Text.text = billet.Text;
        Number.text = billet.Number.ToString();

    }
}
