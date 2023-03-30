using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.CommonDtos
{
    public class ResultDto<T> : NormalResultProps
    {
        public T ResultData { get; set; }
    }

    public class ResultDto : NormalResultProps
    {
    }

    public abstract class NormalResultProps
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public FailureReason? Reason { get; set; }
    }

}
