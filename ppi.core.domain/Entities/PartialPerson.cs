using System;

namespace PPI.Core.Domain.Entities
{
    partial class Person
    {
        public bool selected { get; set; }
        public DateTime LastReminderSent { get; set; }
        public DateTime InviteSent { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public int UploadRowNumber { get; set; }

        public string HPICompletedDate { get; set; }
        public string HDSCompletedDate { get; set; }
        public string MVPICompletedDate { get; set; }
    }

}
