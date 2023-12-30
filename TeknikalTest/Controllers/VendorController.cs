using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeknikalTest.DTOs.Vendors;
using TeknikalTest.Services;
using TeknikalTest.Utilities.Handlers;

namespace TeknikalTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly VendorService _vendorService;

        public VendorController(VendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var vendors = _vendorService.Get();
            if (!vendors.Any())
            {
                return NotFound(new ResponseHandler<VendorDtoGet>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Vendor not found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<VendorDtoGet>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Vendors found",
                Data = vendors
            });
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var vendor = _vendorService.Get(guid);
            if (vendor is null)
            {
                return NotFound(new ResponseHandler<VendorDtoGet>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Vendor not found"
                });
            }

            return Ok(new ResponseHandler<VendorDtoGet>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Vendor found",
                Data = vendor
            });
        }

        [AllowAnonymous]
        [HttpGet("ByEmail/{email}")]
        public IActionResult Get(string email)
        {
            var vendor = _vendorService.Get(email);
            if (vendor is null)
            {
                return NotFound(new ResponseHandler<VendorDtoGet>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Vendor not found"
                });
            }

            return Ok(new ResponseHandler<VendorDtoGet>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Vendor found",
                Data = vendor
            });
        }

        [HttpPost]
        public IActionResult Create(VendorDtoCreate vendorDtoCreate)
        {
            var vendorCreated = _vendorService.Create(vendorDtoCreate);
            if (vendorCreated is null)
            {
                return BadRequest(new ResponseHandler<VendorDtoCreate>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Vendor not created"
                });
            }

            return Ok(new ResponseHandler<VendorDtoCreate>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.Created.ToString(),
                Message = "Vendor successfully created",
                Data = vendorCreated
            });
        }

        [HttpPut]
        public IActionResult Update(VendorDtoUpdate vendorDtoUpdate)
        {
            var vendorUpdated = _vendorService.Update(vendorDtoUpdate);
            if (vendorUpdated is -1)
            {
                return NotFound(new ResponseHandler<VendorDtoUpdate>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Vendor not found"
                });
            }

            if (vendorUpdated is 0)
            {
                return BadRequest(new ResponseHandler<VendorDtoUpdate>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Vendor not updated"
                });
            }

            return Ok(new ResponseHandler<VendorDtoUpdate>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Vendor successfully updated",
                Data = vendorDtoUpdate
            });
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var vendorDeleted = _vendorService.Delete(guid);
            if (vendorDeleted is -1)
            {
                return NotFound(new ResponseHandler<VendorDtoGet>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Vendor not found"
                });
            }

            if (vendorDeleted is 0)
            {
                return BadRequest(new ResponseHandler<VendorDtoGet>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Vendor not deleted"
                });
            }

            return Ok(new ResponseHandler<VendorDtoGet>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Vendor successfully deleted"
            });
        }
    }
}
