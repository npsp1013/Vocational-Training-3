using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointOfSale : MonoBehaviour
{
    [SerializeField] private InputField _scannerInput;
    [SerializeField] private InputField _outputScreen;
    [SerializeField] private List<StoreProduct> _productPrefabs;
    [SerializeField] private float _total;
    [SerializeField] private GameObject _registerScreen;
    [SerializeField] private Mode _mode;
    [SerializeField] private OctopusCard _card;

    public float Total { get { return _total; } }
    enum Mode
    {
        Available, AddValue, CheckOut, ReadCard, Finished
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _scannerInput.ActivateInputField();
        ReadCard();
    }

    public void ScanProduct()
    {
        if (_mode != Mode.Available && _mode != Mode.CheckOut) return;

        foreach (StoreProduct product in _productPrefabs)
        {
            if (product.IsSameBarcode(_scannerInput.text) == true)
            {
                string outputText = string.Format("{0} - {1} 港幣\n", product.Name, product.Price);
                _outputScreen.text += outputText;
                _total += product.Price;
                _mode = Mode.CheckOut;
            }
        }
    }

    public void NextTransaction()
    {
        _mode = Mode.Available;
        _outputScreen.text = "";
        _total = 0;
        _card = null;
    }

    public void ShowRegisterScreen()
    {
        _registerScreen.SetActive(true);
    }

    public void HideRegisterScreen()
    {
        _registerScreen.SetActive(false);
    }

    public void CheckOut()
    {
        if (_mode != Mode.CheckOut && _mode != Mode.AddValue)
        {
            _outputScreen.text += "沒有貨品或增值款項\n";
            return;
        }
        _outputScreen.text += string.Format("\n總計 {0} 港幣\n請拍卡\n", _total);

        _mode = Mode.ReadCard;
        
    }

    public bool CanReadCard()
    {
        return _mode == Mode.ReadCard;
    }

    public void ReadCard()
    {
        if (_mode != Mode.ReadCard) return;

        if (_card == null) return;
        _card.AddValue(_total);
        _outputScreen.text += string.Format("\n交易已被接納\n餘額爲 {0} 港幣\n", _card.Value);
        _mode = Mode.Finished;
    }

    public void AddValue(float value)
    {
        if (_mode != Mode.Available && _mode != Mode.AddValue) return;
        _total += value;
        _outputScreen.text += string.Format("增值 {0} 港幣\n", value);
        _mode = Mode.AddValue;
    }

    public void AddCard(OctopusCard card)
    {
        _card = card;
    }
}