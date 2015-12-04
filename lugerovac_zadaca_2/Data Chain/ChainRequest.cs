namespace lugerovac_zadaca_2
{
    public enum RequestType
    {
        AddIdToListAndCheck,
        AddRecord,
        AreYouMydaddy,
        ChangeState,
        FindRoot,
        PrintAllData,
        PrintErronousData,
        SaveData,
        ValidationCheck
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
