using UnityEngine;
using System.Collections;
using System;

public class DrakMalovaniBod : MonoBehaviour, IDragable, ITouchable
{
    private const float nextPointTreshold = 0.1f; // minimalni vzdalenost dalsihobodu při kreslení
    private const float nextPointToFinishRadius = 0.2f; // velikost okoli bodu, do ktereho kreslime
    private const string rukaPosition = "Position"; // pozice animace ruky v controlleru


    public bool isActive; // je bod aktivní? Lze začít v něm kreslit?
    public bool isLast; // je to poslední bod kreslení? (resp. předposlední) - ukončení malování pro propojení
 
    public DrakMalovaniBod pointToFinish; // bod se kterým se má spojit

    private int _startIndex = 0; // počáteční bod v linerenderu od kterého přikreslujeme další čaru 
    private bool _beginDraw = false; // zacalo se kreslit?
    private bool _wasDrawFinished = false; // bylo malovani dotazene az do daneho bodu ?
    private Drak _drak; // drak, kterého kreslíme
    private Vector2 _lastPointPosition; // posledni pozice bodu

    /// <summary>
    /// Při tažení prstem
    /// </summary>
    /// <param name="position"></param>
    public void OnDrag(Vector2 position)
    {
        if (!isActive || !_beginDraw) // pokud je aktivní a můžeme kreslit
            return;

        if (Vector2.Distance(_lastPointPosition, position) < nextPointTreshold)
            return;

        //TODO ověření kam malování směřuje, popř. zrušit
        
        if(Vector2.Distance(pointToFinish.transform.position, position) <= nextPointToFinishRadius)
        {
            _drak.AddLinePositions(pointToFinish.transform.position);
            _wasDrawFinished = true;
            _beginDraw = false;
        }
        else
        { 
            _drak.AddLinePositions(position);
            _lastPointPosition = position;
        }
    }

    /// <summary>
    /// Konec tažení prstu
    /// </summary>
    public void OnDragEnded()
    {
        if (_wasDrawFinished) // pokud bylo kreslení dokončeno
        {
            isActive = false; // zakazat aktualni bod
            if(!isLast)
            {
                pointToFinish.isActive = true; // povolit novy bod
                pointToFinish.pointToFinish.gameObject.SetActive(true); // aktivovat dalsi bod
                gameObject.SetActive(false); // zruseni akualniho bodu
            }
            else // posledni usek kresleni
            {
                //TODO notifikace o dokonceni kresleni => spusteni animace apod
                pointToFinish.gameObject.SetActive(false); // zruseni ciloveho bodu
                gameObject.SetActive(false); // zruseni aktualniho bodu kresleni
            }
        }
        else if (_beginDraw)// pokud nebylo kreslení dokončeno a bylo započato (nebo nezrušeno v průběhu kreslení), je třeba smazat linku
        {
            _drak.RemoveLinePositions(_startIndex);
            _beginDraw = false;
        }
    }


    /// <summary>
    /// Prvotní klik na bod
    /// </summary>
    public void OnTouch()
    {
        if (!isActive) // pokud je bod neaktivní tak nekreslíme
            return;

        _beginDraw = true; // zaciname kreslit
        _startIndex = _drak.LineCurrectIndex; // index počatečního bodu kreslení
        _drak.AddLinePositions(transform.position);
        _lastPointPosition = transform.position;
    }

    // Use this for initialization
    void Start ()
    {
        _drak = GetComponentInParent<Drak>();
    }


    
}
