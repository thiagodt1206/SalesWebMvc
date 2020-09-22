using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="{0} obrigatorio")]
        [StringLength(60,MinimumLength =3, ErrorMessage ="{0}: tamanho deve ter entre {2} e {1} caracteres")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} obrigatorio")]
        [EmailAddress(ErrorMessage ="Insira um e-mail valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} obrigatorio")]
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name="Base Salary")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        [Required(ErrorMessage = "{0} obrigatorio")]
        [Range(100, 50000,ErrorMessage ="Salario tem que ser entre {1} e {2}")]
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final)
                .Sum(sr => sr.Amount);
        }
    }
}
