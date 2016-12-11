using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class AdsViewModel
    {
        public int ID { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.Url, ErrorMessage = "Please enter valid URL")]
        [Required(ErrorMessage = "Navigate Url is Required")]
        public string NavigateUrl { get; set; }
        [Required(ErrorMessage = "Alternate Text is Required")]
        public string AlternateText { get; set; }
        [Required(ErrorMessage = "KeyWord is Required")]
        public string Keyword { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Impressions { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Width { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Height { get; set; }
        public bool SideAd { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase File { get; set; }
    }
}