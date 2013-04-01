using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AcceptanceTestDemo.Domain;
using Newtonsoft.Json;

namespace AcceptanceTestDemo.Repositories
{
    public class ConferencesRepository
    {
        readonly Dictionary<string, Conference> Conferences;

        public ConferencesRepository()
        {
            using (var re = File.OpenText("conferences.json"))
            {
                using (var reader = new JsonTextReader(re))
                {
                    var conferences = new JsonSerializer().Deserialize<LoadedConferences>(reader);
                    Conferences = conferences.Conferences.GroupBy(x => x.ConferenceName).ToDictionary(x => x.Key, x => x.First());
                }
            }
        }

        public Conference Load(string conferenceName)
        {
            if (Conferences.ContainsKey(conferenceName))
            {
                return Conferences[conferenceName];
            }
            throw new InvalidOperationException(string.Format("Conference {0} not found", conferenceName));
        }

        public IEnumerable<Conference> LoadAll()
        {
            return Conferences.Values;
        }
    }

    class LoadedConferences
    {
        public List<Conference> Conferences;
    }
}