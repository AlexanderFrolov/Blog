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

        /// <summary>
        /// add tag
        /// </summary>
        [HttpPost]
        [Route("Tags/Add")]
        public async Task<IActionResult> AddTag([FromBody] AddTagRequest request)
        {
            var tags = await _tags.GetAllTags();

            var hasTag = tags.Any(t => t.Name == request.TagName);

            if (hasTag)
                return StatusCode(400, $"Такой тэг уже существует!");


            // нужно использовать маппер тут
            var tag = new Tag { Name = request.TagName};
            
            await _tags.SaveTag(tag);
           
            return StatusCode(201, $"Тэг {tag.Name}  успешно добавлен!");
        }

        /// <summary>
        /// add tag
        /// </summary>
        [HttpPut]
        [Route("Tags/{id}")]
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
                return StatusCode(400, $"Ошибка: Тэг с именем {request.TagName} уже существует. Введите другое имя!");

            // нужно использовать маппер тут
            await _tags.UpdateTag(tag, request.TagName);

            return StatusCode(201, $"Тэг {id}::{tag.Name} успешно изменен на: {request.TagName}");
        }

        /// <summary>
        /// view list of tags
        /// </summary>
        [HttpGet]
        [Route("Tags")]
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
        [Route("Tags/{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] Guid id)
        {
            var tag = await _tags.GetTagById(id);
            
            if (tag is null)
                return StatusCode(400, $"Ошибка! Тэг c id:{id} не найден!");

            // ТУТ МЫ НАРУШИЛИ ПРАВИЛО. ИСПРАВИТЬ! 
            return StatusCode(200, tag);
        }

        /// <summary>
        /// deleting existing tag by id
        /// </summary>
        [HttpDelete]
        [Route("Tags/{id}")]
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
