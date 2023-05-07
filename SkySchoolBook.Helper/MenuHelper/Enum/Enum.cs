using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Helper
{
    [Serializable]
    public enum MenuType
    {
        Administration = 1,
        Profiles = 2,
        Files = 3,
        Services = 4,
        Reports = 5,
        Exam = 6,
        Financial = 7,
        Channels = 8,
        Queues = 9,
        SchoolSettings = 10,
        StudentLevels = 11,
        StudentGrades = 12,
        Dashboard = 13,
        Setup = 14,
        PredefinedSetup = 15,
        Outlook = 16,
    }

    public enum ExamsPage
    {
        ExamsSetup = 1,
        SignsExam = 2,
        CheckingTags = 3,
    }

    public enum ExamsStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        O,
        /// <summary>
        /// Rejected
        /// </summary>
        R,
        /// <summary>
        /// Approved
        /// </summary>
        A,

        /// <summary>
        /// Closed
        /// </summary>
        C,

        /// <summary>
        /// Pending
        /// </summary>
        P,
    }

    public enum Currency
    {
        /// <summary>
        /// The usd
        /// </summary>
        USD = 840,
        /// <summary>
        /// The LBP
        /// </summary>
        LBP = 422,

        /// <summary>
        /// The AED
        /// </summary>
        AED = 784,

        /// <summary>
        /// The JPY
        /// </summary>
        JPY = 392,

        /// <summary>
        /// The EGP
        /// </summary>
        EGP = 818,

        /// <summary>
        /// The GBP
        /// </summary>
        GBP = 826,

        /// <summary>
        /// The EUR
        /// </summary>
        EUR = 978,

        /// <summary>
        /// The PPT
        /// </summary>
        PPT = 5000,

        /// <summary>
        /// The DPT
        /// </summary>
        DPT = 5001,


    }

}

