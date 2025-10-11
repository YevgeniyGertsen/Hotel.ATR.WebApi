using System.Net;

namespace Hotel.ATR.WebApi.Model
{
    public class ReturnResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessag { get; set; }

        //для получения каких то объектов
        public object Result { get; set; }
    }
}