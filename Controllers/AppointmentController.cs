﻿using HealthCareAppointment.HealthCare_BLL;
using HealthCareAppointment.HealthCare_BLL.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System;
using System.Net.Mail;
using System.Net;

namespace HealthCareAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AppointmentController));
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Appointmentdashboard()
        {
            try
            {
                //if (@Session["Role"].ToString() == "Admin")
                //{
                    var appointments = await _unitOfWork.Appointment.GetAllAppointments();
                    return View(appointments);
                //}
                //else
                //{
                //    int doctorid = 3;
                //    var appointments = await _unitOfWork.Appointment.GetAppointmentByDoctorId(doctorid);
                //    return View(appointments);
                //}


            }
            catch (Exception ex)
            {
                logger.Info("Appointment dashboard error : " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }

        // GET: Appointment
        public async Task<ActionResult> Appointment()
        {
            try
            {
                var appointment = new Appointment();
                var statelist = await _unitOfWork.States.GetAll();
                appointment.StateList = statelist.ToList();
                var locationlist = await _unitOfWork.Locations.GetAll();
                appointment.LocationList = locationlist.ToList();
                var doctorlist = await _unitOfWork.Doctors.GetAll();
                appointment.DoctorList = doctorlist.ToList();
                var patientlist = await _unitOfWork.Patient.GetAll();
                appointment.PatientList = patientlist.ToList();
                var timeslot = await _unitOfWork.TimeSlot.GetAll();
                appointment.TimeSlotList = timeslot.ToList();
                var specializationlist = await _unitOfWork.Specialization.GetAll();
                appointment.SpecializationList = specializationlist.ToList();
                return View(appointment);
            }
            catch (Exception ex)
            {
                logger.Info("Appointment error : " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentSave(Appointment appointment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Appointment.Add(new Appointment()
                    {
                        AppointmentDate = appointment.AppointmentDate,
                        StateId = appointment.StateId,
                        LocationId = appointment.LocationId,
                        DoctorId = appointment.DoctorId,
                        PatientId = appointment.PatientId,
                        TimeSlotId = appointment.TimeSlotId,
                        Status = 1,
                        SpecializationId = appointment.SpecializationId
                    });
                    _unitOfWork.Complete();
                    string result = SendEmail();
                    TempData["ResultMessage"] = " Appointment booked and mail sent successfully.";
                    return RedirectToAction("Appointment");
                }
                return View();
            }
            catch (Exception ex)
            {
                logger.Info("Appointment error : " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }

        public string SendEmail()
        {
            string result = "Message Sent Successfully..!!";
            try
            {

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("shaaslam007@gmail.com");
                message.To.Add(new MailAddress("shaaslam007@gmail.com"));
                message.Subject = "Appointment booked";
                message.Body = "Appointment has been booked successfully.";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("shaaslam007@gmail.com", "Mobin@1994");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                //result = "Error sending email.!!!";
                logger.Info("Mail sending error : " + ex);
                logger.Debug(ex);
            }

            return result;
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Appointment appointment = new Appointment();
                appointment = await _unitOfWork.Appointment.GetAppointmentById(id);
                var statelist = await _unitOfWork.States.GetAll();
                appointment.StateList = statelist.ToList();
                var locationlist = await _unitOfWork.Locations.GetAll();
                appointment.LocationList = locationlist.ToList();
                var doctorlist = await _unitOfWork.Doctors.GetAll();
                appointment.DoctorList = doctorlist.ToList();
                var patientlist = await _unitOfWork.Patient.GetAll();
                appointment.PatientList = patientlist.ToList();
                var timeslot = await _unitOfWork.TimeSlot.GetAll();
                appointment.TimeSlotList = timeslot.ToList();
                var specializationlist = await _unitOfWork.Specialization.GetAll();
                appointment.SpecializationList = specializationlist.ToList();
                return View(appointment);
            }
            catch (Exception ex)
            {
                logger.Info("Error while editing appointment: " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            try
            {
                var appointmentresult = _unitOfWork.Appointment.GetAppointment(appointment.AppointmentId);
                appointmentresult.StateId = appointment.StateId;
                appointmentresult.LocationId = appointment.LocationId;
                appointmentresult.SpecializationId = appointment.SpecializationId;
                appointmentresult.DoctorId = appointment.DoctorId;
                appointmentresult.PatientId = appointment.PatientId;
                appointmentresult.TimeSlotId = appointment.TimeSlotId;
                appointmentresult.AppointmentDate = appointment.AppointmentDate;

                _unitOfWork.Complete();
                TempData["EditResultMessage"] = " Appointment updated successfully.";
                return RedirectToAction("Appointmentdashboard");
            }
            catch (Exception ex)
            {
                logger.Info("Appointment editing error : " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }

        public ActionResult Deleteappointment(int id)
        {
            try
            {
                Appointment appointment = new Appointment();
                appointment = _unitOfWork.Appointment.GetAppointment(id);
                _unitOfWork.Appointment.Remove(appointment);
                _unitOfWork.Complete();
                return RedirectToAction("Appointmentdashboard");
            }
            catch (Exception ex)
            {
                logger.Info("Appointment error while deleting: " + ex);
                logger.Debug(ex);
                return View("Error");
            }
        }


    }
}