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
    [SerializeField] private Mode _payMode;
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
                _payMode = Mode.CheckOut;
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
        if (_mode == Mode.Finished)
        {
            _outputScreen.text += "交易完成，請按“取消”進行下一筆交易。\n";
            return;
        }
        if (_mode != Mode.CheckOut && _mode != Mode.AddValue)
        {
            _outputScreen.text += "沒有貨品或增值款項\n";
            return;
        }
        _outputScreen.text += string.Format("\n總計 {0} 港幣\n請拍卡\n", _total);
        
        _mode = Mode.ReadCard;
        MicrophoneCapture microphone = GameObject.Find("DialogFlow Manager").GetComponent<MicrophoneCapture>();
        StartCoroutine(microphone.StartCaptureAfterTime(0, 5));
    }

    public bool CanReadCard()
    {
        return _mode == Mode.ReadCard;
    }

    public void ReadCard()
    {
        if (_mode != Mode.ReadCard) return;
        if (_card == null) return;
        bool isTransactionSuccess = false;
        if (_payMode == Mode.AddValue)
        {
            _card.AddValue(_total);
            isTransactionSuccess = true;
        }
        if (_payMode == Mode.CheckOut)
        {
            isTransactionSuccess = _card.ReduceValue(_total);
        }
        if (isTransactionSuccess == true)
        {
            _outputScreen.text += string.Format("\n交易已被接納\n餘額爲 {0} 港幣\n", _card.Value);
        }
        if (isTransactionSuccess == false)
        {
            _outputScreen.text += string.Format("\n交易失敗\n餘額爲 {0} 港幣\n", _card.Value);
        }
        if (_card.Value <= 0.0f)
        {
            _outputScreen.text += "請儘快增值!\n";
        }

        MicrophoneCapture microphone = GameObject.Find("DialogFlow Manager").GetComponent<MicrophoneCapture>();
        StartCoroutine(microphone.StartCaptureAfterTime(0, 5));
        _mode = Mode.Finished;
    }

    public void AddValue(float value)
    {
        if (_mode != Mode.Available && _mode != Mode.AddValue) return;
        _total += value;
        _outputScreen.text += string.Format("增值 {0} 港幣\n", value);
        _mode = Mode.AddValue;
        _payMode = Mode.AddValue;
    }

    public void AddCard(OctopusCard card)
    {
        _card = card;
    }
}