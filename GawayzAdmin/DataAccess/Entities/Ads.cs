using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Ads
    {
        [Key]
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        public string NavigateUrl { get; set; }
        public string AlternateText { get; set; }
        public string Keyword { get; set; }
        public int Impressions { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool SideAd { get; set; }
        public byte[] Image { get; set; }
    }
}
