﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.Data.CustomEntities;

namespace University.Service.Interface
{
    public interface IPaymentGatewayService
    {
        bool SaveTransactionDetails(CardTransactionDetails CardTransactionDetail);
        bool SaveCardDetails(CardDetails CardDetails);

        List<CardDetails> GetCardDetails(int UserId);

        IEnumerable<ProductVideos> GetUserVideosList();
        IEnumerable<CardTransactionDetailsMappings> GetCardTransDeatilsMapp();

    }
}
