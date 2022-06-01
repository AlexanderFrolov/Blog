using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Blog.Contracts.Models.Users;
using Blog.Data.Models;
using Blog.Data.Queries;
using Blog.Data.Repos;
using Blog.Contracts.Models.Comments;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private IMapper _mapper;
        private ICommentRepository _comments;
        private IUserRepository _users;
        private IPostRepository _posts;

        public CommentsController(
            IMapper mapper,
            ICommentRepository comments,
            IUserRepository users,
            IPostRepository posts)
        {
            _mapper = mapper;
            _comments = comments;
            _users = users;
            _posts = posts;
        }

        /// <summary>
        /// view all comments 
        /// </summary>
        [HttpGet]
        [Route("Comments")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _comments.GetComments();

            var response = new GetCommentsResponse
            {
                Amount = comments.Length,
                Comments = _mapper.Map<Comment[], CommentsView[]>(comments)
            };

            return View(response);
        }

        /// <summary>
        /// add comment
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
        {
            var user = await _users.GetUser(request.UserId);
            var post = await _posts.GetPostById(request.PostId);

            var comment = _mapper.Map<AddCommentRequest, Comment>(request);

            await _comments.SaveComment(comment, user, post);
          
            return StatusCode(201, $"Комментарий: {comment.Id} Пользователя: {user.DisplayName} " +
                $"под постом: {post.Title} успешно добавлен!");
        }

        /// <summary>
        /// view comment by id 
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            var comment = await _comments.GetComment(id);

            if (comment is null)
                return StatusCode(400, $"Ошибка! Комментарий c id:{id} не найден!");

            var response = new GetCommentResponse
            {
                Comment = _mapper.Map<Comment, CommentView>(comment)
            };

            return StatusCode(200, response);
        }

        /// <summary>
        /// update comment
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment(
            [FromRoute] Guid id,
            [FromBody] UpdateCommentRequest request)
        {
            var comment = await _comments.GetComment(id);

            if (comment is null)
                return StatusCode(400, $"Ошибка! Комментарий c id:{id} не найден!");

            await _comments.UpdateComment(
               comment,
               request.Content           
               );

            return StatusCode(201, $"Комментарий {id} успешно изменен.");
        }

        /// <summary>
        /// deleting comment by id
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var comment = await _comments.GetComment(id);

            if (comment is null)
                return StatusCode(400, $"Ошибка удаления комментария. Комментарий c id: {id} не найден!");

            await _comments.DeleteComment(comment);
            
            return StatusCode(200, $"Комментарий: {comment.Id} пользователя: {comment.UserId} успешно удален!");
        }
    }
}
