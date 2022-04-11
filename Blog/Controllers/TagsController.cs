using Microsoft.AspNetCore.Mvc;
using Blog.Data.Repos;
using Blog.Contracts.Models.Tags;
using Blog.Data.Models;
using AutoMapper;


namespace Blog.Controllers
{
    public class TagsController : Controller
    {
        private ITagRepository _tags;
        private IMapper _mapper;


        public TagsController(ITagRepository tags, IMapper mapper)
        {
            _tags = tags;
            _mapper = mapper;   
        }

        //public IActionResult Index()
        //{
        //    //return View();
        //    return StatusCode(200, "привет!");
        //}


        /// <summary>
        /// view list of tags
        /// </summary>
        [HttpGet]
        [Route("Tags")]
        public async Task<IActionResult> Index()
        {
            var tags = await _tags.GetAllTags();

            var response = new GetTagsResponse
            {
                TagsAmount = tags.Length,
                Tags = _mapper.Map<Tag[], TagsView[]>(tags)
            };

            return StatusCode(200, response);
        }

        /// <summary>
        /// deleting existing tag
        /// </summary>
        [HttpDelete]
        [Route("Tags/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var tag = await _tags.GetTagById(id);

            if (tag is null)
                return StatusCode(400, $"Ошибка удаления тэга. Тэг {id} не найден!");

            await _tags.DeleteTag(tag);

            return StatusCode(200, $"Тэг {tag.TagId} успешно удален!");
        }

    }
}
