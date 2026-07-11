using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeSkills.Application.DTOs.EmployeeSkill;
public class UpdateEmployeeSkillDto
{
    public string ProficiencyLevel { get; set; }
    public int YearsOfExperience { get; set; }
    public bool IsPrimary { get; set; }
}