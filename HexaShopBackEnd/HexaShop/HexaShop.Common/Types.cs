using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common
{
    public enum Gender
    {
        /// <summary>
        /// مذکر
        /// </summary>
        Male = 0,
        /// <summary>
        /// مونث
        /// </summary>
        Female = 1
    }
    
    public enum ResourceUri
    {
        NextPage = 0,
        PreviouseNext = 1,
        FirstPage = 2,
        LastPage = 3,
    }

    public enum HistoryExceptTables
    {
        AppIdentityUser = 0,
        AppIdentityRole = 1,
        AppIdentityUserRole = 2,
        RefereshToken = 3,
        History = 4,
    }


    public enum OrderProgressLevel
    {
        /// <summary>
        /// در انتظار پرداخت
        /// </summary>
        WaitToPayment = 0,
        /// <summary>
        /// پرداخت شده
        /// </summary>
        Paid = 1,
        /// <summary>
        /// تایید شده
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// رد شده
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// در حال بسته بندی
        /// </summary>
        Packaging = 4,
        /// <summary>
        /// آماده سازی برای ارسال
        /// </summary>
        PrePairngToSend = 5,
        /// <summary>
        /// ارسال
        /// </summary>
        Delivery = 6,
        /// <summary>
        /// تحویل داده شده
        /// </summary>
        RecievedByCustomer = 7, 
        /// <summary>
        /// کنسل شده
        /// </summary>
        Cancelled = 15
    }

}
