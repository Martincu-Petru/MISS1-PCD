using Google.Cloud.Translation.V2;
using System.Threading.Tasks;

namespace Code
{
    public class TranslateService
    {
        private readonly TranslationClient _translationClient;

        public TranslateService()
        {
            _translationClient = TranslationClient.Create();
        }

        public async Task<string> Translate(string targetLanguage, string text)
        {
            var result = await _translationClient.TranslateTextAsync(
                text: text,
                targetLanguage: targetLanguage,
                model: TranslationModel.Base);

            return result.TranslatedText;
        }
    }
}
