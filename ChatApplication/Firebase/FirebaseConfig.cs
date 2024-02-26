namespace ChatApplication.Firebase
{
    using Google.Apis.Auth.OAuth2;
    using Newtonsoft.Json;
    using System.IO;

    namespace ChatApplication.Firebase
    {
        public class FirebaseConfig
        {
            public string Type { get; set; }
            public string ProjectId { get; set; }
            public string PrivateKeyId { get; set; }
            public string PrivateKey { get; set; }
            public string ClientEmail { get; set; }
            public string ClientId { get; set; }
            public string AuthUri { get; set; }
            public string TokenUri { get; set; }
            public string AuthProviderX509CertUrl { get; set; }
            public string ClientX509CertUrl { get; set; }
            public string UniverseDomain { get; set; }

            public static FirebaseConfig LoadFromFile(string filePath)
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<FirebaseConfig>(json);
            }

            public GoogleCredential GetCredential()
            {
                return GoogleCredential.FromJson(JsonConvert.SerializeObject(this));
            }
        }
    }

}
