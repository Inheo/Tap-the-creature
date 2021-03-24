using System;
using UnityEngine;

public class BankaForCreature : MonoBehaviour
{
    public SpriteRenderer bank,
                        cap;
    private float coefficientScale = 1;
    public float speedMoveChild = 2;
    private float _HeightBank = 0;
    private float _HeightCap = 0;
    private Vector3 startPositionBank,
                    startPositionCap,
                    endPositionBank,
                    endPositionCap;
    public bool moveBank = false;

    public bool Ready { get; private set; }
    public event Action eventSelectNextCreature;
    void Start()
    {
        moveBank = false;
        Ready = false;
        coefficientScale = 1 / transform.localScale.y;
        _HeightBank = (bank.sprite.rect.height / bank.sprite.pixelsPerUnit) * transform.localScale.y;
        _HeightCap = (cap.sprite.rect.height / cap.sprite.pixelsPerUnit) * transform.localScale.y;

        startPositionBank = new Vector3(0, -(Camera.main.Height() + _HeightBank / 2) * coefficientScale, 0);
        startPositionCap = new Vector3(0, (Camera.main.Height() + _HeightCap / 2) * coefficientScale, 0);

        endPositionBank = Vector3.zero;
        endPositionCap = new Vector3(0, ((_HeightBank / 2) + (_HeightCap / 2)) / transform.localScale.y, 0);

        bank.transform.localPosition = startPositionBank;
        cap.transform.localPosition = startPositionCap;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveBank)
        {
            MoveChild(bank.transform, endPositionBank);
        }
        if (bank.transform.localPosition == endPositionBank)
        {
            MoveChild(cap.transform, endPositionCap);
        }
        if(cap.transform.localPosition == endPositionCap && !Ready)
        {
            Invoke("BankClose", 0.2f);
        }
    }

    public void InstantStartPositioning()
    {
        bank.transform.localPosition = endPositionBank;
        cap.transform.localPosition = endPositionCap;
    }
    private void BankClose()
    {
        if (!Ready)
        {
            Ready = true;
            eventSelectNextCreature?.Invoke();
        }
    }

    private void MoveChild(Transform transformChild, Vector3 endPosition)
    {
        transformChild.localPosition = Vector3.MoveTowards(transformChild.localPosition, endPosition, speedMoveChild * Time.deltaTime);
    }
}
