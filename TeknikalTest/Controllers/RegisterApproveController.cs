﻿
using TeknikalTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TeknikalTest.Utilities.Handlers;
using TeknikalTest.DTOs.registerApprove;


namespace TeknikalTest.Controllers;


[ApiController]
[Route("api/register-payments")]
/*[Authorize]*/
public class RegisterApproveController : ControllerBase
{
    private readonly RegisterApproveService _service;
    private readonly RegisterApproveService _paymentService;

    public RegisterApproveController(RegisterApproveService service, RegisterApproveService paymentService)
    {
        _service = service;
        _paymentService = paymentService;
    }


    [HttpGet]
    /*[Authorize(Roles = $"{nameof(RoleLevel.SysAdmin)}")]*/
    public IActionResult GetAll()
    {
        var entities = _service.GetregisterApprove();

        if (entities == null)
        {
            return NotFound(new ResponseHandler<GetRegisterApproveDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GetRegisterApproveDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("img/{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/register_payments", imageName);
        if (System.IO.File.Exists(imagePath))
        {
            return PhysicalFile(imagePath, "image/png");
        }
        return NotFound();
    }

    [HttpGet("{guid}")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.SysAdmin)}, {nameof(RoleLevel.Company)}")]*/
    public IActionResult GetByGuid(Guid guid)
    {
        var registerpayment = _service.GetRegisterPayment(guid);
        if (registerpayment is null)
        {
            return NotFound(new ResponseHandler<GetRegisterApproveDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<GetRegisterApproveDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = registerpayment
        });
    }

    [HttpGet("summary/{email}")]
    /*[AllowAnonymous]*/
    public IActionResult GetPaymentSummary(string email)
    {
        var payment = _paymentService.GetPaymentSummary(email);

        if (payment == null)
        {
            return StatusCode(StatusCodes.Status402PaymentRequired, new ResponseHandler<ApproveSummaryDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Payment of this registered account is not found",
            });
        }
        else
        {
            
                return StatusCode(StatusCodes.Status200OK, new ResponseHandler<ApproveSummaryDto>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Payment of this registered account is Approve",
                });
            

            return StatusCode(StatusCodes.Status402PaymentRequired, new ResponseHandler<ApproveSummaryDto>
            {
                Code = StatusCodes.Status402PaymentRequired,
                Status = HttpStatusCode.PaymentRequired.ToString(),
                Message = "Sorry your account is inactive, please do the payment to activate",
                Data = payment
            });
        }
    }

    [HttpPost]
    public IActionResult Create(CreateRegisterApproveDto newRegisterPaymentDto)
    {
        var createEventPayment = _service.CreateRegisterPayment(newRegisterPaymentDto);
        if (createEventPayment is null)
        {
            return BadRequest(new ResponseHandler<GetRegisterApproveDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }

        return Ok(new ResponseHandler<GetRegisterApproveDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createEventPayment
        });
    }

    [HttpPut]
    public IActionResult Update(UpdateRegisterApproveDto updateRegisterPaymentDto)
    {
        var update = _service.UpdateRegisterPayment(updateRegisterPaymentDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UpdateRegisterApproveDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UpdateRegisterApproveDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }
        return Ok(new ResponseHandler<UpdateRegisterApproveDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var delete = _service.DeleteRegisterPayment(guid);

        if (delete is -1)
        {
            return NotFound(new ResponseHandler<GetRegisterApproveDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }
        if (delete is 0)
        {
            return BadRequest(new ResponseHandler<GetRegisterApproveDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check connection to database"
            });
        }

        return Ok(new ResponseHandler<GetRegisterApproveDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }



    [HttpPut("payment-submission")]
    [AllowAnonymous]
    public async Task<IActionResult> PaymentSubmission([FromForm] ApproveSubmissionDto paymentSubmissionDto)
    {

        var paymentSubmissionStatus = await _service.UploadPaymentSubmission(paymentSubmissionDto);

        if (paymentSubmissionStatus is -1)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed create folder image"
            });
        }

        if (paymentSubmissionStatus is -2)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseHandler<string>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "File size cannot be more than 2mb"
            });
        }

        if (paymentSubmissionStatus is -3)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseHandler<string>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "your uploaded file is not image file"
            });
        }

        if (paymentSubmissionStatus is -4)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to update image payment"
            });
        }

        if (paymentSubmissionStatus is -5)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Failed to update status payment"
            });
        }

        if (paymentSubmissionStatus is -6)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your internet connection"
            });
        }

        if (paymentSubmissionStatus is -7)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseHandler<string>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }

        if (paymentSubmissionStatus is 0)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, new ResponseHandler<string>
            {
                Code = StatusCodes.Status401Unauthorized,
                Status = HttpStatusCode.Unauthorized.ToString(),
                Message = "Not Authenticated"
            });
        }

        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully upload payment submission"
        });

    }

    [HttpPut("aprove")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.SysAdmin)}")]*/
    public IActionResult Aprove(AproveRegisterDto aproveRegisterPaymentDto)
    {
        var aprovedRegisterPaymentStatus = _service.AproveRegisterPayment(aproveRegisterPaymentDto);

        if (aprovedRegisterPaymentStatus is 0)
        {
            return BadRequest(new ResponseHandler<UpdateRegisterApproveDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }


        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully aprove register payment submission"
        });
    }

    [HttpPut("reject")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.SysAdmin)}")]*/
    public IActionResult Reject(AproveRegisterDto aproveRegisterPaymentDto)
    {
        var rejectedRegisterPaymentStatus = _service.RejectRegisterPayment(aproveRegisterPaymentDto);

        if (rejectedRegisterPaymentStatus is 0)
        {
            return BadRequest(new ResponseHandler<UpdateRegisterApproveDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Check your data"
            });
        }


        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully reject register payment submission"
        });
    }
}
