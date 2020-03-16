using Google.Cloud.Translation.V2;

namespace Code
{
    public class TranslateService
    {
        private TranslationClient client;

        public TranslateService()
        {

        }

        public string Translate(string targetLanguage, string text)
        {
            client = TranslationClient.Create();
            var originalLanguage = client.DetectLanguage(text).Language;
            var result = client.TranslateText(text, targetLanguage, originalLanguage).TranslatedText;
            
            return result;
        }
    }
}
