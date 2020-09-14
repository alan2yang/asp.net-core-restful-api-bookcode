using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class AuthorForCreationDto
    {
        [Required(ErrorMessage = "必须提供姓名")]
        [MaxLength(20)]
        public string Name { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }

        [Required(ErrorMessage = "必须提供出生地")]
        [MaxLength(40)]
        public string BirthPlace { get; set; }

        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        public ICollection<BookForCreationDto> Books { get; set; }
        = new List<BookForCreationDto>();
    }
}