using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_commander.Responses
{
    public class Response
    {
        public string CommandString { get; set; }
        public ResponseCode ResponseCode { get; } = ResponseCode.UNRECOGNIZED_RESPONSE_CODE;
        public List<string> Data { get; set; } = new List<string>();

        public Response(string msg)
        {
            var data = msg.Split(',');
            CommandString = data[0];

            if (Enum.TryParse<ResponseCode>(data[1], out var responseCode))
            {
                ResponseCode = responseCode;
            }

            Data.AddRange(data[2..]);
        }
    }
}
