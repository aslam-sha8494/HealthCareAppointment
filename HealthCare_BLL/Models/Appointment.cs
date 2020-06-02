using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthCareAppointment.HealthCare_BLL.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AppointmentDate { get; set; }

        public int Status { get; set; }

        [NotMapped]
        public ICollection<States> StateList { get; set; }

        public int StateId { get; set; }
        [ForeignKey("StateId")]

        public States States { get; set; }

        [NotMapped]
        public ICollection<Locations> LocationList { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]

        public Locations Locations { get; set; }

        [NotMapped]
        public ICollection<Doctors> DoctorList { get; set; }

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]

        public Doctors Doctors { get; set; }

        [NotMapped]
        public ICollection<Patient> PatientList { get; set; }

        public int PatientId { get; set; }
        [ForeignKey("PatientId")]

        public Patient Patient { get; set; }

        [NotMapped]
        public ICollection<TimeSlot> TimeSlotList { get; set; }

        public int TimeSlotId { get; set; }
        [ForeignKey("TimeSlotId")]

        public TimeSlot TimeSlot { get; set; }

        [NotMapped]
        public ICollection<Specialization> SpecializationList { get; set; }

        public int SpecializationId { get; set; }
        [ForeignKey("SpecializationId")]
        public Specialization Specialization { get; set; }

        public Appointment()
        {
            StateList = new Collection<States>();
            LocationList = new Collection<Locations>();
            DoctorList = new Collection<Doctors>();
            PatientList = new Collection<Patient>();
            TimeSlotList = new Collection<TimeSlot>();
            SpecializationList = new Collection<Specialization>();
        }
    }
}