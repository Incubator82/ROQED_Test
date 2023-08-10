using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class DropPlaceScr : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform LeftParititionBillet, RigthParititionBillet;

    public void OnDrop(PointerEventData eventData)
    {
        BilletMovementScr billet = eventData.pointerDrag.GetComponent<BilletMovementScr>();

        if (billet)
        {
            if (billet.DefaultParent == LeftParititionBillet && transform.name == "PartitionRigth")
            {
                var billetReference = GetBilletReference(billet.GetComponent<BilletInfoScr>(), true);

                BilletManager.BilletsLeftList.Remove(billetReference);
                BilletManager.BilletsRigthList.Add(billetReference);
            }

            if (billet.DefaultParent == RigthParititionBillet && transform.name == "PartitionLeft")
            {
                var billetReference = GetBilletReference(billet.GetComponent<BilletInfoScr>(), false);

                BilletManager.BilletsLeftList.Add(billetReference);
                BilletManager.BilletsRigthList.Remove(billetReference);
            }
            
            billet.DefaultParent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        BilletMovementScr billet = eventData.pointerDrag.GetComponent<BilletMovementScr>();

        if (billet)
            billet.DefaultTempBilletParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        BilletMovementScr billet = eventData.pointerDrag.GetComponent<BilletMovementScr>();

        if (billet && billet.DefaultTempBilletParent == transform)
            billet.DefaultTempBilletParent = billet.DefaultParent;
    }

    public Billet GetBilletReference(BilletInfoScr billetInfoScr, bool leftList)
    {
        var billetSelf = billetInfoScr.GetComponent<BilletInfoScr>();
        var billetSelfText = billetSelf.Text.text;
        int billetSelfNumber = int.Parse(billetSelf.Number.text);

        return leftList == true ? BilletManager.BilletsLeftList.Where(w => w.Text == billetSelfText && w.Number == billetSelfNumber).FirstOrDefault() :
                                  BilletManager.BilletsRigthList.Where(w => w.Text == billetSelfText && w.Number == billetSelfNumber).FirstOrDefault();
    }
}
