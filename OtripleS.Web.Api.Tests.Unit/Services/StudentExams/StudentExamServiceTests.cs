// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Moq;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.StudentExams;
using OtripleS.Web.Api.Services.StudentExams;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;

namespace OtripleS.Web.Api.Tests.Unit.Services.StudentExams
{
    public partial class StudentExamServiceTests
    {
        
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly IStudentExamService studentEaxmService;

        public StudentExamServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            this.studentEaxmService = new StudentExamService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }
        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private IQueryable<StudentExam> CreateRandomStudentGuardians() =>
           CreateStudentExamFiller(DateTimeOffset.UtcNow).Create(GetRandomNumber()).AsQueryable();


        private StudentExam CreateRandomStudentExam(DateTimeOffset dateTime) =>
            CreateStudentExamFiller(dateTime).Create();

        private static Filler<StudentExam> CreateStudentExamFiller(DateTimeOffset dates)
        {
            var filler = new Filler<StudentExam>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(StudentExam => StudentExam.CreatedDate).Use(dates)
                .OnProperty(StudentExam => StudentExam.UpdatedDate).Use(dates)
                .OnProperty(StudentExam => StudentExam.Student).IgnoreIt()
                .OnProperty(StudentExam => StudentExam.Exam).IgnoreIt()
                .OnProperty(StudentExam => StudentExam.ReviewingTeacher).IgnoreIt();

            return filler;
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                expectedException.Message == actualException.Message
                && expectedException.InnerException.Message == actualException.InnerException.Message;
        }
    }
}
