using UnityEngine;
using UnityEngine.EventSystems;
public class BilletMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera _mainCamera;
    Vector3 _offset; // используем для плавного перетаскивания плашки
    public Transform DefaultParent; // используем для возврата плашки в начальное положение
    public Transform DefaultTempBilletParent; // обьект временной плашки
    GameObject _tempBillet; // визуальная темповая плашка

    void Awake()
    {
        _mainCamera = Camera.allCameras[0];
        _tempBillet = GameObject.Find("BilletTemp");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - _mainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTempBilletParent = transform.parent;

        _tempBillet.transform.SetParent(DefaultParent);
        _tempBillet.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = _mainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + _offset;

        if (_tempBillet.transform.parent != DefaultTempBilletParent)
            _tempBillet.transform.SetParent(DefaultTempBilletParent);

        ChekPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(_tempBillet.transform.GetSiblingIndex());
        _tempBillet.transform.SetParent(GameObject.Find("Canvas").transform);
        _tempBillet.transform.localPosition = new Vector3(1300, 230);
    }

    void ChekPosition()
    {
        int newIndex = DefaultTempBilletParent.childCount;

        for (int i = 0; i < DefaultTempBilletParent.childCount; i++)
        {
            if (transform.position.y > DefaultTempBilletParent.GetChild(i).position.y)
            {
                newIndex = i;

                if (_tempBillet.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;
            }
        }

        _tempBillet.transform.SetSiblingIndex(newIndex);
    }
}
