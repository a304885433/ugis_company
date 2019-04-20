using System.ComponentModel.DataAnnotations;

namespace MyERP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}