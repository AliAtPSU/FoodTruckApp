using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FoodTruckApp
{
    public static class Constants
    {
        public static string ApplicationURL = @"https://foodtruckali.azurewebsites.net";
        private const string CosmosDBEndpointUrl = "https://foodtruckali.documents.azure.com:443/";
        private const string CosmosDBPrimaryKey = "hhqv20gzTJ6W03ypEwjMjfZBLOwNajwZjSrqSFg8xn8gjJcv3AkzzZgPkKmwmXsndlQsV5krj19Rc79aZJFmBw==";
        private readonly static DocumentClient client = new DocumentClient(new Uri(CosmosDBEndpointUrl), CosmosDBPrimaryKey);


    }
}
