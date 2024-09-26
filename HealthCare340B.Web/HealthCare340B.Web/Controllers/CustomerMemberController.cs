using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    [Route("Profile/Pasien")]
    public class CustomerMemberController : Controller
    {
        private readonly CustomerMemberModel _customerMemberModel;
        private readonly BloodGroupModel _bloodGroupModel;
        private readonly CustomerRelationModel _customerRelationModel;

        private readonly int _pageSize;
        private readonly string _imageFolder;

        public CustomerMemberController(IConfiguration configuration)
        {
            _customerMemberModel = new CustomerMemberModel(configuration);
            _bloodGroupModel = new BloodGroupModel(configuration);
            _customerRelationModel = new CustomerRelationModel(configuration);

            _pageSize = int.Parse(configuration["PageSize"]);
            _imageFolder = configuration["ImageFolder"];
        }

        [Route("")]
        public async Task<IActionResult> Index(
            string? filter,
            int? pageNumber,
            int? currPageSize,
            string? orderBy,
            string? orderDirection
        )
        {
            List<VMMCustomerMember>? data = new List<VMMCustomerMember>();

            try
            {
                data = await _customerMemberModel.GetByFilter(
                    string.IsNullOrEmpty(filter) ? "" : filter
                );
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            //Process data Order
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderDirection == "asc")
                {
                    if (orderBy == "name")
                    {
                        data = data?.OrderBy(d => d.Fullname).ToList();
                    }
                    else if (orderBy == "age")
                    {
                        data = data?.OrderBy(d => d.Age).ToList();
                    }
                }
                else if (orderDirection == "desc")
                {
                    if (orderBy == "name")
                    {
                        data = data?.OrderByDescending(d => d.Fullname).ToList();
                    }
                    else if (orderBy == "age")
                    {
                        data = data?.OrderByDescending(d => d.Age).ToList();
                    }
                }
            }

            ViewBag.Title = "Daftar Pasien";
            ViewBag.imgFolder = _imageFolder;
            ViewBag.Role = "ROLE_PASIEN";
            ViewBag.Filter = filter;
            ViewBag.PageSize = currPageSize ?? _pageSize;
            ViewBag.OrderBy = orderBy ?? "name";
            ViewBag.OrderDirection = orderDirection ?? "asc";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem
                {
                    Name = "Beranda",
                    Controller = "Home",
                    Action = "Index",
                },
                new BreadcrumbItem
                {
                    Name = "Profile",
                    Controller = "Profile",
                    Action = "Index",
                },
                new BreadcrumbItem { Name = "Pasien", IsActive = true },
            };

            return View(
                Pagination<VMMCustomerMember>.Create(
                    data ?? new List<VMMCustomerMember>(),
                    pageNumber ?? 1,
                    currPageSize ?? _pageSize
                )
            );
        }

        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Title = "Tambah Pasien";
            ViewBag.BloodGroups = await _bloodGroupModel.GetAll();
            ViewBag.CustomerRelations = await _customerRelationModel.GetAll();

            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<VMResponse<VMMCustomerMember>?> CreateAsync(VMMCustomerMember data)
        {
            VMResponse<VMMCustomerMember>? response = null;

            try
            {
                data.CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!);

                response = await _customerMemberModel.CreateAsync(data);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long id)
        {
            VMMCustomerMember? data = new VMMCustomerMember();

            try
            {
                data = await _customerMemberModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Edit Pasien";
            ViewBag.BloodGroups = await _bloodGroupModel.GetAll();
            ViewBag.CustomerRelations = await _customerRelationModel.GetAll();

            return View(data);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<VMResponse<VMMCustomerMember>?> EditAsync(VMMCustomerMember data)
        {
            VMResponse<VMMCustomerMember>? response = null;

            try
            {
                data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);

                response = await _customerMemberModel.UpdateAsync(data);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            VMMCustomerMember? data = new VMMCustomerMember();

            try
            {
                data = await _customerMemberModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hapus Pasien";

            return View(data);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<VMResponse<VMMCustomerMember>?> DeleteAsync(long id)
        {
            VMResponse<VMMCustomerMember>? response = null;

            try
            {
                long userId = long.Parse(HttpContext.Session.GetString("userId")!);

                response = await _customerMemberModel.DeleteAsync(id, userId);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }
    }
}
