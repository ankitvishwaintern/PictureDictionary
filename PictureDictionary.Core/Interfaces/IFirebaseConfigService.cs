using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureDictionary.Core.Interfaces
{
    public interface IFirebaseConfigService
    {
        string ApiKey { get; }
        string AuthDomain { get; }
        string ProjectId { get; }
        string StorageBucket { get; }
        string MessagingSenderId { get; }
        string AppId { get; }
        string MeasurementId { get; }

        Dictionary<string, string> GetAllConfig();
    }
}
