using System.Collections;
using UnityEngine;

public class Chatting : MonoBehaviour
{
    OpenAIChatCompletionAPI apiClient;
    public string prompt;
    private string response;

    void Start()
    {
        apiClient = new OpenAIChatCompletionAPI();
        StartCoroutine(ChatCompletionCoroutine());
    }

    IEnumerator ChatCompletionCoroutine()
    {
        yield return StartCoroutine(apiClient.ChatCompletionRequest(prompt, res =>
        {
            response = res;
            if (response != null)
            {
                Debug.Log("AIの応答: " + response);
            }
            else
            {
                Debug.LogError("応答の取得に失敗しました。");
            }
        }));
    }
}
