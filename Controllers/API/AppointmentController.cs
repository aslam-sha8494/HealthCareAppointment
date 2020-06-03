using HealthCareAppointment.HealthCare_BLL;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HealthCareAppointment.Controllers.API
{
    public class AppointmentController : ApiController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AccountController));
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/Appointment/GetDoctors")]
        [HttpGet]
        public HttpResponseMessage GetDoctors()
        {
            try
            {
                var result = _unitOfWork.Doctors.GetAll();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            }
            catch (Exception ex)
            {
                logger.Info("Register error : " + ex);
                logger.Debug(ex);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest);
                return response;
            }
        }
    }
}
