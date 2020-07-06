//---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
//----------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Models.Students;

namespace OtripleS.Web.Api.Services
{
    public interface IStudentService
    {
        ValueTask<Student> RegisterStudentAsync(Student student);
        ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId);
        ValueTask<Student> ModifyStudentAsync(Student student);
        ValueTask<Student> DeleteStudentAsync(Guid studentId);
    }
}