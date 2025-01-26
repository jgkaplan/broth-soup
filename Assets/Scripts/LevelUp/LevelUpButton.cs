using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUpButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private TextMeshProUGUI displayName;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private Image image;

    private Button button;

    private BaseItem item;

    public GameObject spoonSelector;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void AssignBaseItem(BaseItem i)
    {
        item = i;
        displayName.text = i.Name;
        description.text = i.Description;
        Debug.Log(i.Icon);
        image.sprite = i.Icon;
        button.enabled = false;
        StartCoroutine(EnableLater());
    }

    IEnumerator EnableLater()
    {
        yield return new WaitForSecondsRealtime(1f);
        button.enabled = true;
    }

    public void OnSelect()
    {
        item.LevelUp();
        spoonSelector.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        spoonSelector.SetActive(true);
        spoonSelector.transform.position = transform.position + new Vector3(3f, 3.5f, 0f);
    }
}
