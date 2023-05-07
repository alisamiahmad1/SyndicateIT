using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.UtilityComponent
{
    [Serializable]

    public enum Cycle
    {
        PreeSchool = 9,
        Elementary = 10,
        Intermediate = 11,

    }

    public enum ProfileSourceType
    {
        ProfileInforamtion = 1,
        FileStudents = 2,
        FileStudent = 3,
        FileParent = 4,
        DashboardAdmin = 5,
        DashboardTeacher = 6,
        DashboardParent = 7,
        DashboardStudent = 8,
        MyProfile = 9,

    }

    public enum Semester
    {
        Semester1 = 5,
        Semester2 = 7,
        Semester3 = 9,

    }

    public enum Language
    {
        english = 1,
        arabic = 2,
        French = 3,

    }
    public enum UserStatus
    {
        Active = 1,
        Desactive = 2,
    }
    public enum MessageState
    {
        Seen = 1

    }
    public enum ReportPeriodType
    {
        TTD = 1,
        MTD = 2,
        YTD = 3,

    }

    public enum Languages
    {

        /// <summary>
        /// The add
        /// </summary>
        English = 1,
    }
    public enum AlertType
    {
        Warning = 1,
        Success = 2,
        Info = 3,
        Danger = 4,

    }
    public enum UserRole
    {

        SuperAdministrator = 1,
        Administrator = 2,
        CustomerSupport = 3,
    }
    public enum TypeAction
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        Search = 4,
    }
    public enum UserType
    {
        SuperAdministrator = 1,
        Administrator = 2,
        Employees = 3,
        MemberBoard = 4,
        Members = 5,
        Agent = 6,

    }
    public enum ServiceType
    {
        Agenda = 1,
        Schedule = 2,
        PeriodSchedule = 3,
        DistributionStudents = 4,
        ExamProgram = 5,
        Duties = 6,
        LevelBehavior = 7,
        LevelIntelligence = 8,
        Attendance = 9,
        AbsorptionGrades = 10,
        DegreesParticipationActivites = 11,
        AgendaDaily = 12,
        AttendanceMonthly = 13,
        DutiesMonthly = 14,
        LevelBehaviorMonthly = 15,
        LevelIntelligenceMonthly = 15,
        AbsorptionGradesMonthly = 16,
        DegreesParticipationMonthly = 17,
    }
    public enum FileType
    {
        DataEntryOperator = 1,
        EnrolmentCommitte = 2,
        FinancialCommitte = 3,       
    }


    public enum Currency
    {

        USD = 840,
        LBP = 422,
        AED = 784,
        JPY = 392,
        EGP = 818,
        GBP = 826,
        EUR = 978,
        PPT = 5000,
        DPT = 5001,
    }
}
