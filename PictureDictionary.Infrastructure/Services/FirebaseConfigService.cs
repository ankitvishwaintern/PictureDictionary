using PictureDictionary.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureDictionary.Infrastructure.Services
{
    public class FirebaseConfigService : IFirebaseConfigService
    {
        public string ApiKey => "AIzaSyA9Yq2UCZTGzmhGV4Uv9CNXuS7rL_yR7ks";
        public string AuthDomain => "picturedictionary-a2149.firebaseapp.com";
        public string ProjectId => "picturedictionary-a2149";
        public string StorageBucket => "picturedictionary-a2149.firebasestorage.app";
        public string MessagingSenderId => "249299812094";
        public string AppId => "1:249299812094:web:96c7906db266f64c88afaf";
        public string MeasurementId => "G-DD99CEG1VQ";

        public Dictionary<string, string> GetAllConfig()
        {
            Dictionary<string, string> keyValuePairs = new();
            keyValuePairs.Add("apiKey", ApiKey);
            keyValuePairs.Add("authDomain", AuthDomain);
            keyValuePairs.Add("projectId", ProjectId);
            keyValuePairs.Add("storageBucket", StorageBucket);
            keyValuePairs.Add("messagingSenderId", MessagingSenderId);
            keyValuePairs.Add("appId", AppId);
            keyValuePairs.Add("measurementId", MeasurementId);
            
            return keyValuePairs;
        }
    }
}
