using System.Net.NetworkInformation;

namespace StockTakeAPI.Helpers
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public T Value { get; set; }
        public string Msg { get; set; }

        public Response() { }

        public Response(bool status, T? value, string? msg = null)
        {
            Status = status;
            Value = value;
            Msg = msg;
        }
    }
}
