using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Threading.Tasks;
using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;

namespace _26_HoangBaThong_Assignment01_BackEnd.Controllers
{
    public class TagsController : ODataController
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService) { _tagService = tagService; }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var tags = await _tagService.GetTagsAsync();
            return Ok(tags);
        }
    }
}
