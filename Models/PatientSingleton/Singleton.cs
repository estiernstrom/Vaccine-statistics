using DSU24_Grupp5.Infrastructure;
using Microsoft.Azure.Amqp;

namespace DSU24_Grupp5.Models.HeadModel
{
    public sealed class Singleton
    {
        private static Singleton _instance;
        private static readonly object lockObject = new object();
        public List<Patient>? Patients { get; set; }

        private Singleton()
        {
            Patients = ApiClient.CreateListOfAllPatients();
        }

        public static Singleton GetSingleton()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }
    }
}
