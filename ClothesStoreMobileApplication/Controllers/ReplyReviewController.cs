using AutoMapper;
using ClothesStoreMobileApplication.Models;
using ClothesStoreMobileApplication.Repository.IRepository;
using ClothesStoreMobileApplication.ViewModels.ReplyReview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothesStoreMobileApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyReviewController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReplyReviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var objList = _unitOfWork.ReplyReview.GetAll();
            return Ok(objList);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var obj = _unitOfWork.ReplyReview.GetFirstOrDefault(u => u.ReplyId == id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpGet("GetReplyReviewsForReview/{reviewId}")]
        public IActionResult GetReplyReviewsForReview(int reviewId)
        {
            var objList = _unitOfWork.ReplyReview.GetAll(u => u.ReviewId == reviewId);
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }


        [HttpPost]
        public IActionResult Create([FromBody] ReplyReviewViewModel replyReviewVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var replyReview = _mapper.Map<ReplyReview>(replyReviewVM);
            _unitOfWork.ReplyReview.Add(replyReview);
            _unitOfWork.Save();
            return CreatedAtAction("Get", new { id = replyReview.ReplyId }, replyReview);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ReplyReview.GetFirstOrDefault(u => u.ReplyId == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.ReplyReview.Remove(objFromDb);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
