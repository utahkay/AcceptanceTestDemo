using System;
using System.Collections.Generic;
using AcceptanceTestDemo.Domain;

namespace AcceptanceTestDemo.Repositories
{
    public class ConferencesRepository
    {
        public Conference Load(string conferenceName)
        {
            return new Conference(new DateTime(2013, 07, 13),
                                  new Dictionary<int, decimal>
                                      {
                                          {0, 700m},
                                          {7, 500m},
                                          {45, 300m}
                                      },
                                  new Dictionary<int, decimal>
                                      {
                                          {0, 0m},
                                          {5, 0.1m}
                                      },
                                  new Dictionary<string, decimal>
                                      {
                                          {"", 0m},
                                          {"HALFOFF", 0.5m}
                                      });
        }
    }
}