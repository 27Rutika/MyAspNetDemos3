using System;
using System.ComponentModel.DataAnnotations;

namespace MyDemos.Areas.Demo.ViewModels
{
    public class CustomerViewModel
    {

        [Display(Name = "ID")]
        [Required]
        public int CustomerId { get; set; }


        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "{0} cannot be empty!")]
        [MaxLength(80, ErrorMessage = "{0} can contain a maxium of {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} should contain a minimum of {1} characters.")]
        public string CustomerName { get; set; }


        [Required]
        [EmailAddress(ErrorMessage = "{0} is not valid.")]
        public string Email { get; set; }


        [Display(Name = "Customer was created on")]
        [Required]
        public DateTime CreatedOn { get; set; }


        [Display(Name = "Balance due from the Customer")]
        [Range(minimum: 0, maximum: 200000, ErrorMessage = "{0} has to be between {1} and {2}")]
        public decimal Balance { get; set; }


        [Display(Name = "Is Enabled?")]
        public bool IsEnabled { get; set; }

    }
}