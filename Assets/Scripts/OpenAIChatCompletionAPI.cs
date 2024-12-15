using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using ChatCompletionModels; // csの名前空間

public class OpenAIChatCompletionAPI
{
    private readonly JsonSerializerSettings jsonSettings;

    public OpenAIChatCompletionAPI()
    {
        jsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
    }

    public IEnumerator ChatCompletionRequest(string userMessage, System.Action<string> callback)
    {
        var messages = new List<ChatCompletionModels.Message>
        {
            new ChatCompletionModels.Message { role = "system", content = "あなたは高校に通う学生です。友達との会話を想像してください。" },
            new ChatCompletionModels.Message { role = "user", content = userMessage }
        };

        var requestData = new RequestData { messages = messages };
        string json = JsonConvert.SerializeObject(requestData, jsonSettings);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);

        using (var request = new UnityWebRequest(Credentials.OPENAI_API_URL, "POST"))
        {
            request.SetRequestHeader("Authorization", $"Bearer {Credentials.OPENAI_API_KEY}");
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("[OpenAIChatCompletionAPI] リクエストエラー: " + request.error);
                callback(null);
            }
            else
            {
                var responseText = request.downloadHandler.text;
                var response = JsonConvert.DeserializeObject<ResponseData>(responseText);

                if (response?.choices != null && response.choices.Count > 0)
                {
                    string responseContent = response.choices[0].message.content;
                    callback(responseContent);
                }
                else
                {
                    Debug.LogError("[OpenAIChatCompletionAPI] 応答の解析に失敗しました。");
                    callback(null);
                }
            }
        }
    }
}
