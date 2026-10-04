using _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Interfaces;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Entities;
using _26_HoangBaThong_Assignment01_BackEnd.DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace _26_HoangBaThong_Assignment01_BackEnd.BLL.Services.Implements;

public class TagService : ITagService
{
    private readonly ITagRepository _repository;
    public TagService(ITagRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Tag>> GetTagsAsync() => _repository.GetTagsAsync();
}

