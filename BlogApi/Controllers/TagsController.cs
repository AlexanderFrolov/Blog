using AutoMapper;
using Blog.Contracts.Models.Tags;
using Blog.Data.Models;
using Blog.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private ITagRepository _tags;
        private IMapper _mapper;


        public TagsController(ITagRepository tags, IMapper mapper)
        {
            _tags = tags;
            _mapper = mapper;
        }

        /// <summary>
        /// add tag
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddTag([FromBody] AddTagRequest request)
        {
            var tags = await _tags.GetAllTags();

            var hasTag = tags.Any(t => t.Name == request.Name);

            if (hasTag)
                return StatusCode(400, $"Такой тэг уже существует!");

            var newTag = _mapper.Map<AddTagRequest, Tag>(request);

            await _tags.SaveTag(newTag);

            return StatusCode(201, $"Тэг {newTag.Name}  успешно добавлен!");
        }

        /// <summary>
        /// add tag
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTag(
            [FromRoute] Guid id,
            [FromBody] UpdateTagRequest request)
        {
            var tag = await _tags.GetTagById(id);
            
            if (tag is null)
                return StatusCode(400, $"Тэг с id: {id} не существует!");

            var tags = await _tags.GetAllTags();
            var withSameName = tags.Any(t => t.Name == request.TagName);

            if (withSameName)
                return StatusCode(400, $"Ошибка: Тэг с именем {request.TagName} которое вы хотите добавить уже существует. Введите другое имя!");

            await _tags.UpdateTag(tag, request.TagName);

            return StatusCode(201, $"Тэг {id} успешно изменен.");
        }

        /// <summary>
        /// view list of tags
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllTags()
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
        /// view tag by id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] Guid id)
        {
            var tag = await _tags.GetTagById(id);

            if (tag is null)
                return StatusCode(400, $"Ошибка! Тэг c id:{id} не найден!");

            var response = new GetTagResponse
            {
                Tag = _mapper.Map<Tag, TagView>(tag)
            };

            return StatusCode(200, response);
        }

        /// <summary>
        /// deleting existing tag by id
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var tag = await _tags.GetTagById(id);

            if (tag is null)
                return StatusCode(400, $"Ошибка удаления тэга. Тэг c id: {id} не найден!");

            await _tags.DeleteTag(tag);

            return StatusCode(200, $"Тэг {tag.Name} :: {tag.Id} успешно удален!");
        }
    }
}
