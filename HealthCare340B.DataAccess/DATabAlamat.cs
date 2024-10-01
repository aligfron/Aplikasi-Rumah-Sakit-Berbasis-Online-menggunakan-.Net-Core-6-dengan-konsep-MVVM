using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare340B.DataAccess
{
    public class DATabAlamat
    {
        private readonly HealthCare340BContext db;

        public DATabAlamat(HealthCare340BContext db)
        {
            this.db = db;
        }

        public VMResponse<VMMBiodataAddress> GetById(long id)
        {
            VMResponse<VMMBiodataAddress> response = new VMResponse<VMMBiodataAddress>();
            try
            {
                response.Data = (
                    from m in db.MBiodataAddresses
                    join l in db.MLocations on m.LocationId equals l.Id
                    where m.IsDelete == false && m.Id == id
                    select new VMMBiodataAddress
                    {
                        Id = m.Id,
                        Label = m.Label,
                        Recipient = m.Recipient,
                        RecipientPhoneNumber = m.RecipientPhoneNumber,
                        LocationId = l.Id,
                        Location = l.Name,
                        PostalCode = m.PostalCode,
                        Address = m.Address,
                        CreatedBy = m.CreatedBy,
                        CreatedOn = m.CreatedOn,
                        ModifiedBy = m.ModifiedBy,
                        ModifiedOn = m.ModifiedOn,
                        DeletedOn  = m.DeletedOn,
                        DeletedBy = m.DeletedBy,
                        IsDelete = m.IsDelete,
                    }
                ).FirstOrDefault();
                response.StatusCode = (response.Data != null)
                   ? HttpStatusCode.OK
                   : HttpStatusCode.NoContent;
                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} Biodata Address successfully created!"
                    : $"{HttpStatusCode.NoContent} - Biodata Address does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }
            return response;
        }

        public VMResponse<List<VMMBiodataAddress>?> GetAll()
        {
            VMResponse<List<VMMBiodataAddress>?> response = new VMResponse<List<VMMBiodataAddress>?>();
            try
            {
                var query = from m in db.MBiodataAddresses
                            where(m.IsDelete == false)
                            select new VMMBiodataAddress
                            {
                                Id = m.Id,
                                BiodataId = m.BiodataId,
                                Label = m.Label,
                                Recipient = m.Recipient,
                                RecipientPhoneNumber = m.RecipientPhoneNumber,
                                LocationId = m.LocationId,
                                PostalCode = m.PostalCode,
                                Address = m.Address,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} biodata address");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} biodata address(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No biodata address is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public VMResponse<List<VMMBiodataAddress>> GetByFilter(string? filter)
        {
            VMResponse<List<VMMBiodataAddress>> response = new VMResponse<List<VMMBiodataAddress>>();
            try
            {
                // Menggunakan string kosong jika filter null
                filter = filter?.ToLower() ?? string.Empty;

                response.Data = (
                    from m in db.MBiodataAddresses
                    join l in db.MLocations on m.LocationId equals l.Id
                    join e in db.MLocationLevels on l.LocationLevelId equals e.Id
                    where m.IsDelete == false &&
                          (m.Recipient.ToLower().Contains(filter) || m.Address.Contains(filter))
                    select new VMMBiodataAddress(m, l)
                ).ToList();

                if (response.Data.Any())
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} Biodata Address successfully fetched!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Biodata Address does not exist!";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<List<VMMBiodataAddress>> GetByLabel(string? label)
        {
            VMResponse<List<VMMBiodataAddress>> response = new VMResponse<List<VMMBiodataAddress>>();
            try
            {
                response.Data = (
                   from m in db.MBiodataAddresses
                   join l in db.MLocations on m.LocationId equals l.Id
                   join e in db.MLocationLevels on l.LocationLevelId equals e.Id
                   where m.IsDelete == false &&
                   (m.Label!.Contains(label!))
                   select new VMMBiodataAddress(m, l)
               ).ToList();
                response.StatusCode = (response.Data != null)
                   ? HttpStatusCode.OK
                   : HttpStatusCode.NoContent;
                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} Biodata Address successfully fetched!"
                    : $"{HttpStatusCode.NoContent} - Biodata Address does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }


        public VMResponse<VMMBiodataAddress> Create(VMMBiodataAddress data)
        {
            VMResponse<VMMBiodataAddress> response = new VMResponse<VMMBiodataAddress>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MBiodataAddress newData = new MBiodataAddress();
                    newData.BiodataId = data.BiodataId;
                    newData.Label = data.Label;
                    newData.Recipient = data.Recipient;
                    newData.RecipientPhoneNumber = data.RecipientPhoneNumber;
                    newData.LocationId = data.LocationId;
                    newData.PostalCode = data.PostalCode;
                    newData.Address = data.Address;
                    newData.CreatedBy = data.CreatedBy;
                    newData.CreatedOn = DateTime.Now;
                    newData.IsDelete = false;

                    db.Add(newData);
                    db.SaveChanges();

                    dbTran.Commit();

                    response.Data = GetById(newData.Id).Data;

                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = $"{HttpStatusCode.Created} - New Address has been successfully created";
                }
                catch (DbUpdateException dbEx)
                {
                    dbTran.Rollback(); // Rollback transaction on error
                    var innerException = dbEx.InnerException?.Message;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {dbEx.Message} - {innerException}";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback(); // Rollback transaction on error
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
                return response;
            }
        }

        public VMResponse<VMMBiodataAddress> Update(VMMBiodataAddress data)
        {
            VMResponse<VMMBiodataAddress> response = new VMResponse<VMMBiodataAddress>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMBiodataAddress? existingData = GetById(data.Id).Data;
                    if (existingData != null)
                    {
                        MBiodataAddress updateData = new MBiodataAddress()
                        {
                            Id = data.Id,
                            Label = data.Label,
                            Recipient = data.Recipient,
                            RecipientPhoneNumber = data.RecipientPhoneNumber,
                            LocationId = data.LocationId,
                            PostalCode = data.PostalCode,
                            Address = data.Address,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,

                            DeletedBy = existingData.DeletedBy,
                            DeletedOn = existingData.DeletedOn,
                            IsDelete = existingData.IsDelete,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                        };
                        db.Update(updateData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = GetById(updateData.Id).Data;

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{response.StatusCode} - Address {response.Data?.Label} has been successfully updated!";
                    }
                }
                catch (Exception ex)
                {
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        response.Message += $" Inner Exception: {ex.InnerException.Message}";
                    }
                    dbTran.Rollback();
                }
                return response;
            }
        }

        public VMResponse<VMMBiodataAddress> Delete(long id, long userId)
        {
            VMResponse<VMMBiodataAddress> response = new VMResponse<VMMBiodataAddress>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMBiodataAddress? existingData = GetById(id).Data;
                    if (existingData != null)
                    {
                        MBiodataAddress deletedData = new MBiodataAddress()
                        {
                            Id = existingData.Id,
                            Label = existingData.Label,
                            Recipient = existingData.Recipient,
                            RecipientPhoneNumber = existingData.RecipientPhoneNumber,
                            LocationId = existingData.LocationId,
                            PostalCode = existingData.PostalCode,
                            Address = existingData.Address,

                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = userId,
                            DeletedOn = DateTime.Now,

                            IsDelete = true,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = (DateTime)existingData.CreatedOn!,
                        };

                        db.Update(deletedData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = GetById(deletedData.Id).Data;

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - Address has been successfully deleted!";

                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Address does not exist!";
                    }
                }
                catch (Exception ex)
                {
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    dbTran.Rollback(); // Rollback transaction on error
                }

                return response;
            }

        }

        public VMResponse<VMMBiodataAddress> MultipleDelete(List<long> ids, long userId)
        {
            VMResponse<VMMBiodataAddress> response = new VMResponse<VMMBiodataAddress>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var id in ids)
                    {
                        var existingData = GetById((int)id).Data;
                        if (existingData != null)
                        {
                            MBiodataAddress deletedData = new MBiodataAddress()
                            {
                                Id = existingData.Id,
                                Label = existingData.Label,
                                Recipient = existingData.Recipient,
                                RecipientPhoneNumber = existingData.RecipientPhoneNumber,
                                LocationId = existingData.LocationId,
                                PostalCode = existingData.PostalCode,
                                Address = existingData.Address,

                                ModifiedBy = userId, // Menggunakan userId terkait
                                ModifiedOn = DateTime.Now,

                                IsDelete = true,
                                CreatedBy = existingData.CreatedBy,
                                CreatedOn = (DateTime)existingData.CreatedOn!,
                            };

                            db.Update(deletedData);
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.NotFound;
                            response.Message = $"{HttpStatusCode.NotFound} - Address with ID {id} does not exist!";
                            return response; // Jika salah satu ID tidak ada, keluar dan kembalikan response
                        }
                    }

                    db.SaveChanges();
                    dbTran.Commit();

                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Addresses have been successfully deleted!";
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    dbTran.Rollback(); // Rollback transaction on error
                }

                return response;
            }
        }

        public VMResponse<List<VMMLocation>?> GetAllLocation()
        {
            VMResponse<List<VMMLocation>?> response = new VMResponse<List<VMMLocation>?>();
            try
            {
                var query = from m in db.MLocations
                            select new VMMLocation
                            {
                                Id = m.Id,
                                Name = m.Name,
                                LocationLevelId = m.LocationLevelId,
                                ParentId = m.ParentId,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} Locations");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} Location(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No Location is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }



    }
}