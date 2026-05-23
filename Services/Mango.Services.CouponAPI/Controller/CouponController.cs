using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microservices.Services.CouponAPI.Data;
using Microservices.Services.CouponAPI.Models;
using Microservices.Services.CouponAPI.Models.DTO;
using AutoMapper;

namespace Microservices.Services.CouponAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public CouponController(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._response = new ResponseDTO();
        }

        [HttpGet("all")]
        public ActionResult<ResponseDTO> Get()
        {
            try
            {
                var coupons = _context.coupons.ToList();
                var couponDTOs = _mapper.Map<List<CouponDTO>>(coupons);
                this._response.Result = couponDTOs;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                this._response.IsSuccess = false;
                this._response.Message = "Error fetching coupons";
                return NotFound(_response);
            }
            
        }

        [HttpGet("{CouponId}")]
        public ActionResult<ResponseDTO> GetById(int CouponId)
        {
            try
            {
                var coupon = this._context.coupons.FirstOrDefault(x => x.CouponId == CouponId);
                var couponDTO = _mapper.Map<CouponDTO>(coupon);
                this._response.Result = couponDTO;
                return Ok(_response);
            }
            catch(Exception ex){
                this._response.IsSuccess = false;
                this._response.Message = "Error fetching coupon";
                return NotFound(_response);
            }
        }

        [HttpGet("GetByCoupon/Code{CouponCode}")]
        public ActionResult<ResponseDTO> GetByCouponCode(string CouponCode)
        {
            try
            {
                var coupon = this._context.coupons.FirstOrDefault(x => x.CouponCode == CouponCode);
                if(coupon == null)
                {
                    this._response.IsSuccess = false;
                    this._response.Message = "Not Fetched Successfully";    
                }

                this._response.Result = coupon;
                return Ok(this._response);
            }
            catch(Exception ex)
            {
                this._response.IsSuccess = false;
                this._response.Message = "Not Fetched Successfully";
                return NotFound(this._response);
            }
        }

        [HttpPost("CreateCoupon")]
        public ActionResult CreateCoupon([FromBody] Coupon coupon)
        {
            try
            {
                this._context.coupons.Add(coupon);
                this._context.SaveChanges();
                var coupondto = _mapper.Map<CouponDTO>(coupon);
                this._response.Result = coupondto;
                this._response.Message = "New Coupon Created Successfully";
                return Ok(this._response);
            }
            catch(Exception ex)
            {
                this._response.IsSuccess = false;
                this._response.Message = "New Coupon Cannot Created Successfully";
                return NotFound(this._response);
            }
        }

        [HttpPut("UpdateCoupon")]
        public ActionResult CreateCoupon([FromBody] CouponDTO coupondto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(coupondto);
                this._context.coupons.Update(coupon);
                this._context.SaveChanges();
                this._response.Result = coupondto;
                this._response.Message = "Coupon with ID : " + coupondto.CouponId + "Updated Successfully";
                return Ok(this._response);
            }
            catch(Exception ex)
            {
                this._response.IsSuccess = false;
                this._response.Message = "Coupon with ID : " + coupondto.CouponId + "Not Updated Successfully";
                return NotFound(this._response);
            }
        }

        [HttpDelete("DeleteCoupon")]
        public ActionResult CreateCoupon(int CouponId)
        {
            try
            {
                var coupon = this._context.coupons.FirstOrDefault( x => x.CouponId == CouponId);
                this._context.coupons.Remove(coupon);
                this._context.SaveChanges();
                this._response.Result = coupon;
                this._response.Message = "Coupon with ID : " + CouponId + "Deleted Successfully";
                return Ok(this._response);
            }
            catch(Exception ex)
            {
                this._response.IsSuccess = false;
                this._response.Message = "Coupon with ID : " + CouponId + "Not Deleted Successfully";
                return NotFound(this._response);
            }
        }

    }

}