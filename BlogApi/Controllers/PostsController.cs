using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Blog.Contracts.Models.Posts;
using Blog.Data.Queries;
using Blog.Data.Repos;
using Blog.Data.Models;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private IPostRepository _posts;
        private IMapper _mapper;
        private ITagRepository _tags;
        private IUserRepository _users;


        public PostsController(
            IPostRepository posts,
            IMapper mapper,
            ITagRepository tags,
            IUserRepository users)
        {
            _posts = posts;
            _mapper = mapper;
            _tags = tags;
            _users = users;   
        }

     
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddPost([FromBody] AddPostRequest request)
        {
            var user = await _users.GetUser(request.UserId);
            var tags = await _tags.GetTagsById(request.TagsId);
    
            var newPost = _mapper.Map<AddPostRequest, Post>(request);

            await _posts.SavePost(newPost, user, tags);

            return StatusCode(201, $"Пост: {newPost.Title} успешно добавлен!");
        }

   
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePost(
            [FromRoute] Guid id,
            [FromBody] UpdatePostRequest request)
        {
            var post = await _posts.GetPostById(id);
              
            if (post is null)
                return StatusCode(400, $"Публикация с id: {id} не существует!");

            var newTags = await _tags.GetTagsById(request.TagsId);

            await _posts.UpdatePost(
                post,
                new UpdatePostQuery(
                request.Title,
                request.ShortDescription,
                request.Content,
                newTags));

            return StatusCode(201, $"Пост {id} успешно изменен.");
        }

      
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _posts.GetAllPosts();

            var response = new GetAllPostsResponse
            {
                PostsAmount = posts.Length,
                Posts = _mapper.Map<Post[], AllPostsView[]>(posts)
            };
        
            return StatusCode(200, response);
        }

      
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] Guid id)
        {
            var post = await _posts.GetPostById(id);

            if (post is null)
                return StatusCode(400, $"Ошибка! Пост c id:{id} не найден!");

            var response = new GetPostByIdResponse
            {
                Post = _mapper.Map<Post, GetPostByIdView>(post)
            };

            return StatusCode(200, response);
        }

      
        [HttpGet]
        [Route("UserPosts/{id}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] Guid id)
        {
            var post = await _posts.GetPostsByUserId(id);

            if (post is null)
                return StatusCode(400, $"Ошибка! Пост c id:{id} не найден!");

            var response = new GetPostsByUserIdResponse
            {
                Posts = _mapper.Map<Post[], UserPostsView[]>(post)
            };

            return StatusCode(200, response);
        }

     
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var post = await _posts.GetPostById(id);

            if (post is null)
                return StatusCode(400, $"Ошибка удаления поста. Пост c id: {id} не найден!");

            await _posts.DeletePost(post);

            return StatusCode(200, $"Тэг {post.Title} :: {post.Id} успешно удален!");
        }
    }
}
