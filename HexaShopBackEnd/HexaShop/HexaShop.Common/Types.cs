using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }

    public enum Score
    {
        SoBad = 0,
        NotGood = 1,
        Normal = 2,
        Nice = 3,
        Greate = 4
    }

    public enum FailureReason
    {
        Unknown = 0,
        InvalidModel = 1,
        NotFound = 2,
        UnSuccessful = 3,
        InvalidFileExtension = 4
    }
}
