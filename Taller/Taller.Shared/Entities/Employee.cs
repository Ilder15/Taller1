using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller.Shared.Entities
{
    public class Employee
    {

        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }
            
        [Display(Name = "Activo")]
        public Boolean IsActive { get; set; }

        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaHora { get; set; }

        [Display(Name = "Salario")]
        [Range(1000000.00, 9999999999.99, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Salary { get; set; }
    }
}
