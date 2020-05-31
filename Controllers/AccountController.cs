using HealthCareAppointment.HealthCare_BLL;
using HealthCareAppointment.HealthCare_BLL.AccountModels;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthCareAppointment.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Register()
        {
            var userregister = new UserRegisters();
            userregister.RoleList = _unitOfWork.UserRoles.GetRoles().ToList();
            return View(userregister);
        }
        [HttpPost]
        public ActionResult RegisterSave(UserRegisters registerObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.UserRegisters.Add(new UserRegisters()
                {
                    UserName = registerObj.UserName,
                    Password = registerObj.Password,
                    ConfirmPassword = registerObj.ConfirmPassword,
                    RoleId = registerObj.RoleId
                });
                _unitOfWork.Complete();
                
            }
            return RedirectToAction("Login");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult ValidateLoginUsers(UserRegisters UserLoginDetails)
        {
            var loginuser = _unitOfWork.UserRegisters.ValidateLoginUsers(UserLoginDetails);
            if (loginuser != null)
            {
                Session["Username"] = loginuser.UserName;
                Session["UserId"] = loginuser.Id;
                if (loginuser.RoleId == 1)
                    Session["Role"] = "Admin";
                else
                    Session["Role"] = "Doctor";
            }
            return Redirect("/Home/Dashboard");
        }
    }
}