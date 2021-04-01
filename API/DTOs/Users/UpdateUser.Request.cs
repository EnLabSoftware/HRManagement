using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Users
{
    public class UpdateUserRequest
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(125)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public int? DepartmentId { get; set; }
    }
}