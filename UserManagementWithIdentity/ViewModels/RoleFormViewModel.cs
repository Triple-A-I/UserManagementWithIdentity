using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace UserManagementWithIdentity.ViewModels
{
    public class RoleFormViewModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
