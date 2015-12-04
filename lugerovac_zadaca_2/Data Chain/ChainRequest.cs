namespace lugerovac_zadaca_2
{
    public enum RequestType
    {
        SaveData,
        AddRecord,
        PrintAllData,
        AddIdToListAndCheck,
        ValidationCheck,
        FindRoot,
        AreYouMydaddy
    }

    public class ChainRequest
    {
        public RequestType requestType;
        public object parameters;

        public ChainRequest(RequestType requestType, object parameters)
        {
            this.requestType = requestType;
            this.parameters = parameters;
        }
    }
}
