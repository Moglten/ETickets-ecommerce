using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_Tickets.Models.Operations
{
    public class DropDownListData
    {
        public MultiSelectList actors { get; set; }

        public SelectList cinemas { get; set; }

        public MultiSelectList producers { get; set; }


    }
}
