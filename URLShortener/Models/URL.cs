using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Constants;

namespace URLShortener.Models
{
    public class URL
    {
        public Guid Id { get; set; }

        [MaxLength(MaxLengths.SourceURL, ErrorMessage = "Source URL cannot exceed {1} characters")]
        public string SourceURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter source URL")]

        [MaxLength(MaxLengths.DestinationURL)]
        public string DestinationURL { get; set; } = string.Empty;
    }
}
