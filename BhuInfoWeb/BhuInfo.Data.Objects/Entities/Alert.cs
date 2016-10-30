﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhuInfo.Data.Objects.Entities
{
    public class Alert
    {
        public long AlertId { get; set; }
        public string AlertType { get; set; }
        public string Message { get; set; }
        public long MessageId { get; set; }
        public DateTime DateCreated { get; set; }
        public long CreatedBy { get; set; }

    }
}
