﻿using System;
using System.Collections.Generic;

namespace BankServices.Client.OutModels
{
    public partial class AspNetUserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Discriminator { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
