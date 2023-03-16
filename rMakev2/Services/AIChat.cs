using OpenAI.GPT3.Managers;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using rMakev2.Utilities;
namespace rMakev2.Services
{
 
        public class AIChat
        {
            protected OpenAIService openAiService;
            public  AIChat()
            {
                openAiService = new OpenAIService(new OpenAiOptions()
                {
                    ApiKey = Utilities.Utilities.GptKey
                }); ;
            }

            public async Task<string> UseChatService(string message)
            {
                var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = new List<ChatMessage>
                {
                    ChatMessage.FromUser(message),
                },
                    Model = OpenAI.GPT3.ObjectModels.Models.ChatGpt3_5Turbo,
                    MaxTokens = 100//optional
                });
                if (!completionResult.Successful)
                {
                    return "ChatGpt no esta disponible en este momento";
                }
                return completionResult.Choices.First().Message.Content;
            }
        }
   
}
