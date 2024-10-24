using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 提交畫面 : MonoBehaviour
{
    public item_SO item;
    [SerializeField]
    private GameObject 提交道具UI;
    [SerializeField]
    private GameObject 欄位;
    [SerializeField]
    private GameObject 道具預置物;
    [SerializeField]
    private 控制 f1;
    public Button 提交按鈕;
    private bool[] 是否滿足;
    private 提交道具 提交道具;
    void Start()
    {
        提交道具UI.SetActive(false);
        f1 = GameObject.Find("程式/控制").GetComponent<控制>();
    }
    public void 消耗道具(int[] ID, int[] 數量)
    {
        for (int i = 0; i < item.物品.Count; i++)
        {
            if (item.物品[i].物品ID == ID[i])
            {
                item.物品[i].數量 -= 數量[i];
                if (item.物品[i].數量 <= 0)
                {
                    item.物品[i].數量 = 0;
                }
            }
        }
    }
    public void 顯示提交道具(int[] ID, int[] 數量,提交道具 f2)
    {
        提交道具 = f2;
        提交按鈕.onClick.RemoveAllListeners();
        提交按鈕.onClick.AddListener(提交道具.確定提交);
        f1.CursorUnLock();
        提交道具UI.SetActive(true);
        是否滿足 = new bool[ID.Length];
        foreach (Transform child in 欄位.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ID.Length; i++)
        {
            GameObject item = Instantiate(道具預置物, 欄位.transform);
            item.GetComponent<物品>().物品ID = ID[i];
            item.GetComponent<物品>().更新數量();
            item.GetComponent<物品>().判斷提交(數量[i]);
            是否滿足[i]= item.GetComponent<物品>().是否滿足;
        }
        for (int i = 0; i < 是否滿足.Length; i++)
        {
            if (是否滿足[i] == false)
            {
                提交按鈕.interactable = false;
                break;
            }
            else
            {
                提交按鈕.interactable = true;
            }
        }
    }
    public void 隱藏提交道具()
    {
        提交道具UI.SetActive(false);
        提交道具 = null;
        f1.CursorLock();
    }
}
