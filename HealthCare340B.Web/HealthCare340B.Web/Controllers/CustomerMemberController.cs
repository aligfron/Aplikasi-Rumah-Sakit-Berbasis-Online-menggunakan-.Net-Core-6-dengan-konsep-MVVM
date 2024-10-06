using System.Net;
using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class CustomerMemberController : Controller
    {
        private readonly CustomerMemberModel _customerMemberModel;
        private readonly BloodGroupModel _bloodGroupModel;
        private readonly CustomerRelationModel _customerRelationModel;
        private readonly string _imageFolder;

        private string? _userId;
        private string? _roleCode;

        private readonly int _pageSize;

        public CustomerMemberController(IConfiguration configuration)
        {
            _customerMemberModel = new CustomerMemberModel(configuration);
            _bloodGroupModel = new BloodGroupModel(configuration);
            _customerRelationModel = new CustomerRelationModel(configuration);
            _imageFolder = configuration["ImageFolder"];

            _pageSize = int.Parse(configuration["PageSize"]);
        }

        private bool isInSession()
        {
            _userId = HttpContext.Session.GetString("userId") ?? null;

            return _userId != null;
        }

        private bool isInRole()
        {
            _roleCode = HttpContext.Session.GetString("userRoleCode") ?? null;

            return _roleCode == "ROLE_PASIEN";
        }

        public async Task<IActionResult> Index(
            string? filter,
            int? pageNumber,
            int? currPageSize,
            string? orderBy = "name",
            string? orderDirection = "asc"
        )
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            List<VMMCustomerMember>? data = new List<VMMCustomerMember>();

            try
            {
                data = await _customerMemberModel.GetByFilter(
                    string.IsNullOrEmpty(filter) ? "" : filter,
                    (long)HttpContext.Session.GetInt32("userBiodataId")!
                );
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            //Process data Order
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy == "name")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.Fullname).ToList()
                        : data?.OrderByDescending(d => d.Fullname).ToList();
                }
                else if (orderBy == "age")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.Age).ToList()
                        : data?.OrderByDescending(d => d.Age).ToList();
                }
                else if (orderBy == "totalChat")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.TotalChat).ToList()
                        : data?.OrderByDescending(d => d.TotalChat).ToList();
                }
                else if (orderBy == "totalAppointment")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.TotalAppointment).ToList()
                        : data?.OrderByDescending(d => d.TotalAppointment).ToList();
                }
            }

            ViewBag.Title = "Daftar Pasien";
            ViewBag.imgFolder = _imageFolder;
            ViewBag.Filter = filter;
            ViewBag.PageSize = currPageSize ?? _pageSize;
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderDirection = orderDirection;

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

        public async Task<IActionResult> Create()
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                ViewBag.BloodGroups = await _bloodGroupModel.GetAll();
                ViewBag.CustomerRelations = await _customerRelationModel.GetAll();
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Tambah Pasien";

            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerMember>?> CreateAsync(VMMCustomerMember data)
        {
            VMResponse<VMMCustomerMember>? response = null;

            try
            {
                data.ParentBiodataId = (long)HttpContext.Session.GetInt32("userBiodataId")!;
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

        public async Task<IActionResult> Edit(long id)
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            VMMCustomerMember? data = new VMMCustomerMember();

            try
            {
                data = await _customerMemberModel.GetById(id, (long)HttpContext.Session.GetInt32("userBiodataId")!);
                ViewBag.BloodGroups = await _bloodGroupModel.GetAll();
                ViewBag.CustomerRelations = await _customerRelationModel.GetAll();
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Edit Pasien";

            return View(data);
        }

        [HttpPost]
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

        public async Task<IActionResult> Delete(string ids)
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            List<VMMCustomerMember>? data = new List<VMMCustomerMember>();

            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    long parentBiodataId = long.Parse(HttpContext.Session.GetInt32("userBiodataId")!.ToString()!);

                    // Pisahkan string IDs yang dipisahkan koma menjadi array
                    List<long> idArray = ids.Split(',').Select(long.Parse).ToList();

                    foreach (long id in idArray)
                    {
                        try
                        {
                            data.Add(await _customerMemberModel.GetById(id, parentBiodataId));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hapus Pasien";

            return View(data);
        }

        [HttpPost]
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

        [HttpPost]
        public async Task<VMResponse<VMMCustomerMember>?> MultipleDeleteAsync(string ids)
        {
            VMResponse<VMMCustomerMember>? response = null;
            List<string> failedDeletes = new List<string>();

            try
            {
                if (!string.IsNullOrEmpty(ids))
                {
                    long userId = long.Parse(HttpContext.Session.GetString("userId")!);
                    long parentBiodataId = long.Parse(HttpContext.Session.GetInt32("userBiodataId")!.ToString()!);

                    // Pisahkan string IDs yang dipisahkan koma menjadi array
                    List<long> idList = ids.Split(',').Select(long.Parse).ToList();

                    foreach (long i in idList)
                    {
                        try
                        {
                            response = await _customerMemberModel.DeleteAsync(i, userId);
                        }
                        catch
                        {

                            var member = await _customerMemberModel.GetById(i, parentBiodataId);
                            failedDeletes.Add(member.Fullname!);
                        }
                    }
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else
                {
                    HttpContext.Session.SetString(
                        "errMsg",
                        "Failed to delete Customer Member: "
                            + string.Join(", ", failedDeletes)
                    );
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
