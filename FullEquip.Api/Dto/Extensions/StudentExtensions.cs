using FullEquip.Core.Entities;

namespace FullEquip.Api.Dto.Extensions
{
    public static class StundentExtensions
    {
        public static StudentDto ToDto(this Student student)
        {
            return new StudentDto(student.Id, student.Name, student.Email);
        }
    }
}
