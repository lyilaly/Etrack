using System.ComponentModel.DataAnnotations;

namespace Etrack.Features.Administration.Roles
{
    public class CreateRoleViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }


    public class RoleViewModel
    {
        [Display(Name = "Role Id")]
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
    public class DeleteRoleViewModel
    {
        [Display(Name = "Role Id")]
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
