using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Octoedge007.Models
{
    public class VoiceChatbot
    {
        private readonly Dictionary<string, string> responses = new Dictionary<string, string>
    {
        {"hello", "Hello! How can I assist you today?"},
        {"how are you", "I'm just a computer program, but thanks for asking!"},
        // Add more responses as needed
    };

        public string GetResponse(string userQuery)
        {
            if (responses.TryGetValue(userQuery.ToLower(), out var response))
                return response;
            return "I'm sorry, I don't understand your request.";
        }
    }

}