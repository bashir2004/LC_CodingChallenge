using CsvHelper.Configuration;
using LC_CodingChallenge.Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC_CodingChallenge.Helper
{
    public class CsvReader
    {
        public static List<T> Read<T>(CsvConfiguration configuration, string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvHelper.CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<LeaseMap>();
                List<T> list = new List<T>();
                while (csv.Read())
                {
                    list.Add(csv.GetRecord<T>());
                }
                return list;
            }
        }
    }
}
