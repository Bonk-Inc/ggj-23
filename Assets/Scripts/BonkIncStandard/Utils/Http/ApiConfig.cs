using UnityEngine;

namespace BonkIncStandard.Utils.Http
{
    [CreateAssetMenu(fileName = "ApiConfig", menuName = "http/api config")]
    public class ApiConfig : ScriptableObject
    {
        [SerializeField] 
        private string host;

        [SerializeField] 
        private int port;

        public string Host => host;
        public int Port => port;
        public string URL => $"{host}:{port}";
    }
}