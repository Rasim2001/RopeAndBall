//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using System.Collections;
//using System;

//[System.Serializable]
//public class AutoCompleteInputField<T> : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject loader;
//    [SerializeField]
//    private RectTransform viewport;
//    [SerializeField]
//    private RectTransform resultsParent;
//    [SerializeField]
//    private RectTransform prefab;
//    [SerializeField]
//    private TMP_InputField field;
//    [SerializeField]
//    private Button clear;

//    private List<OrganizationModel> mockData = new List<OrganizationModel>();

//    private bool request = false;

//    public Action<string> organizationButtonTap;

//    public void Start()
//    {
//        field.onValueChanged.RemoveAllListeners();
//        field.onValueChanged.AddListener(OnInputValueChanged);
//        clear.onClick.RemoveAllListeners();
//        clear.onClick.AddListener(Clear);
//    }

//    private void GetValues(Action callback = null)
//    {
//        if (request)
//            return;

//        request = true;
//        mockData.Clear();
//        CoroutineObject.Instance.StartCoroutine(RequestAPI.GetOrganizations(field.text, (organizations) =>
//        {
//            request = false;
//            if (organizations == null)
//            {
//                return;
//            }

//            foreach (var organization in organizations)
//            {
//                mockData.Add(organization);
//            }
//            callback?.Invoke();
//        }));
//    }

//    private void OnInputValueChanged(string newText)
//    {
//        if (newText.Length != 0)
//        {
//            GetValues(() => {
//                FillResults(GetResults(newText));
//            });
//        }

//        if (newText.Length == 0)
//            clear.gameObject.SetActive(false);
//        else
//            clear.gameObject.SetActive(true);

//        ClearResults();

//        if (newText.Length == 0)
//        {
//            viewport.gameObject.SetActive(false);
//            return;
//        }

//        loader.SetActive(true);
//    }

//    private void ClearResults()
//    {
//        for (int childIndex = resultsParent.childCount - 1; childIndex >= 0; --childIndex)
//        {
//            Transform child = resultsParent.GetChild(childIndex);
//            child.SetParent(null);
//            GameObject.Destroy(child.gameObject);
//        }
//    }

//    private void Clear()
//    {
//        field.text = "";
//        field.Select();
//    }

//    private void FillResults(List<OrganizationModel> results)
//    {
//        viewport.gameObject.SetActive(true);
//        for (int resultIndex = 0; resultIndex < results.Count; resultIndex++)
//        {
//            RectTransform child = GameObject.Instantiate(prefab) as RectTransform;
//            child.GetComponentInChildren<OrganizationPanel>().InitMainMenu(results[resultIndex], organizationButtonTap);
//            child.SetParent(resultsParent);
//            child.localScale = Vector3.one;
//        }
//        CoroutineObject.Instance.StartCoroutine(NormalizeContentSizeFilter(resultsParent.GetComponent<ContentSizeFitter>()));
//        CoroutineObject.Instance.StartCoroutine(LoaderOff());
//    }

//    private List<OrganizationModel> GetResults(string input)
//    {
//        return mockData.FindAll((organization) => organization.title.ToLower().Trim().IndexOf(input.ToLower().Trim()) >= 0);
//    }

//    private IEnumerator NormalizeContentSizeFilter(ContentSizeFitter contentSizeFitter)
//    {
//        foreach (var aChild in contentSizeFitter.GetComponentsInChildren<ContentSizeFitter>(true))
//        {
//            aChild.SetLayoutVertical();
//        }

//        yield return new WaitForSeconds(0.05f);
//        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
//        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
//        contentSizeFitter.SetLayoutVertical();
//        yield return null;
//    }
//    private IEnumerator LoaderOff()
//    {
//        yield return new WaitForSeconds(0.5f);
//        loader.SetActive(false);
//    }

//    public void Refresh()
//    {
//        request = false;
//        ClearResults();
//        Clear();
//    }
//}


