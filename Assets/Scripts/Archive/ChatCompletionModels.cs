using System.Collections.Generic;

namespace ChatCompletionModels
{
    public class Message
    {
        public string role;
        public string content;
    }

    public class Choice
    {
        public Message message;
        public string finish_reason;
        public int index;
    }

    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }

    public class RequestData
    {
        public string model = "gpt-4o-mini";
        public List<Message> messages;
        public float? temperature = null;
        public float? top_p = null;
        public int? max_tokens = null;
        public float? presence_penalty = null;
        public float? frequency_penalty = null;
        public Dictionary<int, int> logit_bias = null;
        public string user = null;
    }

    public class ResponseData
    {
        public string id;
        public string @object;
        public int created;
        public string model;
        public Usage usage;
        public List<Choice> choices;
    }
}
