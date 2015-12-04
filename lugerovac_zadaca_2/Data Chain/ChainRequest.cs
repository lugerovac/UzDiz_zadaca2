namespace lugerovac_zadaca_2
{
    /// <summary>
    /// Ova enumeracija sadrži sve moguće tipove zahtjeva
    /// </summary>
    public enum RequestType
    {
        AddIdToListAndCheck,
        AddRecord,
        AddToMemento,
        AreYouMydaddy,
        ChangeState,
        FindRoot,
        PrintAllData,
        PrintErronousData,
        SaveData,
        ValidationCheck
    }

    /// <summary>
    /// Ovom klasom stvaraju se objekti zahtjeva
    /// </summary>
    public class ChainRequest
    {
        public RequestType requestType;
        public object parameters;  //parametar je klase Object kako bi rukovane parametrima bilo što fleksibilnije

        public ChainRequest(RequestType requestType, object parameters)
        {
            this.requestType = requestType;
            this.parameters = parameters;
        }
    }
}
