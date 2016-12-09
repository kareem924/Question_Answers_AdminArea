using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class CodeViewModel
    {
        public int CodeId { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public int CodeType { get; set; }
        public int CodeStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Size { get; set; }
       
    }

    public class GroupByData
    {
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}