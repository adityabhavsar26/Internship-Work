using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new CartRepository();
        
        [HttpGet]
        [Route("list1")]
        public IActionResult GetCartItems(string keyword)
        {
            List<Cart> carts = _cartRepository.GetCartItems(keyword);
            IEnumerable<CartModel> cartModels = carts.Select(c => new CartModel(c));
            return Ok(cartModels);
        }
        [HttpGet]
        [Route("list/{id}")]
        public BaseList<CartResponse> GetAll(int id)
        {
            BaseList<CartResponse> cart = _cartRepository.GetAll(id);
            return cart;
            //return Ok(cart);
        }
        [HttpGet]
        [Route("list2")]
        [ProducesResponseType(typeof(ListResponse<CartModelResponse>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItem2(int UserId)
        {


            var cartitem = _cartRepository.GetCartListall(UserId);
            ListResponse<CartModelResponse> listResponce = new ListResponse<CartModelResponse>()
            {
                Results = cartitem.Results.Select(c => new CartModelResponse(c)),
                TotalRecords = cartitem.TotalRecords,
            };

            return Ok(listResponce);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetCarts(int id)
        {
            if(id == 0)
                return BadRequest("Please Enter the ID");
            var carts = _cartRepository.GetCarts(id);
            CartModel cartModel = new CartModel(carts);
            return Ok(cartModel);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCart(CartModel model)
        {
            if(model == null)
                return BadRequest();
            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.Bookid,
                Userid = model.Userid,
            };
            cart = _cartRepository.AddCart(cart);
            return Ok(new CartModel(cart));
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();
            Cart cart = new Cart()
            {
                Id = model.Id,
                Quantity = model.Quantity,
                Bookid = model.Bookid,
                Userid = model.Userid,
            };
            cart = _cartRepository.UpdateCart(cart);
            return Ok(new CartModel(cart));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();
            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }



    }
}
