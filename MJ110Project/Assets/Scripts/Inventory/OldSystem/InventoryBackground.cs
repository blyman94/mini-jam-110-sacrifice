using UnityEngine;
using UnityEngine.UI;


public class InventoryBackground : MonoBehaviour
    {
    [SerializeField] private TetrisInventorySystem inventoryTetris;

    private void Start() {
        // Create background
        Transform template = transform.Find("Template");
        template.gameObject.SetActive(false);

        for (int x = 0; x < inventoryTetris.GetGrid().GetWidth(); x++) {
            for (int y = 0; y < inventoryTetris.GetGrid().GetHeight(); y++) {
                Transform backgroundSingleTransform = Instantiate(template, transform);
                backgroundSingleTransform.gameObject.SetActive(true);
            }
        }

        GetComponent<GridLayoutGroup>().cellSize = new Vector2(inventoryTetris.GetGrid().getCellSize(), inventoryTetris.GetGrid().getCellSize());

        GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryTetris.GetGrid().GetWidth(), inventoryTetris.GetGrid().GetHeight()) * inventoryTetris.GetGrid().getCellSize();

        GetComponent<RectTransform>().anchoredPosition = inventoryTetris.GetComponent<RectTransform>().anchoredPosition;
    }
    }