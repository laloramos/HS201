using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;

namespace HS201_FinalAssignment.Entities
{
    public class Conference
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}