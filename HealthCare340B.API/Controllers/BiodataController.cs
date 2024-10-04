using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiodataController : ControllerBase
    {
        private DABiodata _biodata;

        public BiodataController(HealthCare340BContext _db)
        {
            _biodata = new DABiodata(_db);
        }

        // Fungsi untuk mengecek validasi MIME type
        private bool IsValidImageMimeType(string imagePath)
        {
            var allowedMimeTypes = new List<string> { "image/jpeg", "image/png", "image/gif" };

            string mimeType = GetMimeTypeFromImagePath(imagePath);

            return allowedMimeTypes.Contains(mimeType);
        }

        // Fungsi untuk mendapatkan MIME type dari file path (dapat dimodifikasi sesuai metode akses file)
        private string GetMimeTypeFromImagePath(string imagePath)
        {
            // Contoh sederhana, sebaiknya gunakan metode yang lebih aman untuk mendapatkan MIME type
            string extension = Path.GetExtension(imagePath).ToLower();

            switch (extension)
            {
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    return string.Empty;
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateImagePath(VMMBiodatum data)
        {
            try
            {
                // Validasi MIME type
                if (!IsValidImageMimeType(data.ImagePath))
                {
                    return BadRequest(
                        new VMResponse<VMMBiodatum?>()
                        {
                            Message = "Invalid image format. Only 'image/jpeg', 'image/png', and 'image/gif' are allowed.",
                            StatusCode = HttpStatusCode.BadRequest
                        }
                    );
                }

                VMResponse<VMMBiodatum?> response = await Task.Run(
                    () => _biodata.UpdateImagePath(data)
                );

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("BiodataController.UpdateImagePath: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("BiodataController.UpdateImagePath: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
