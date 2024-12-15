using UnityEngine;

namespace ChatGPTAPI.Config
{
    [CreateAssetMenu(fileName = "UserProfile", menuName = "ChatGPTAPI/UserProfile", order = 2)]
    public class UserProfile : ScriptableObject
    {
        public string nickName;
        public string age;
        public string sex;
        public string freeForm;
    }
}