using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Models.Dto.Walks;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkReposotory _walkReposotory;

        public WalksController(IMapper mapper,IWalkReposotory walkReposotory) 
        {
            this._mapper = mapper;
            this._walkReposotory = walkReposotory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? filterOn, string? filterQuery,string? sortOn,bool isAscending=false,int pageNumber=1,int pageSize=1000)
        {
            var walksDomainModel= await _walkReposotory.GetAllAsync(filterOn, filterQuery, sortOn,isAscending, pageNumber, pageSize);
            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            var walksDomainModel = await _walkReposotory.GetByIdAsync(id);
            if (walksDomainModel == null) return NotFound();
            return Ok(_mapper.Map<WalkDto>(walksDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWalksDto createWalksDto)
        {
            var walksDomainModel= _mapper.Map<Walk>(createWalksDto);

            walksDomainModel=await _walkReposotory.CreateAsync(walksDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = walksDomainModel.Id }, walksDomainModel);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id,UpdateWalksDto updateWalksDto)
        { 
            var walksDomainModel = _mapper.Map<Walk>(updateWalksDto);
            walksDomainModel = await _walkReposotory.UpdateAsync(id, walksDomainModel);
            if (walksDomainModel == null) return NotFound();
            return Ok(_mapper.Map<WalkDto>(walksDomainModel));    

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        { 
            var walksDomainModel= await _walkReposotory.DeleteAsync(id);
            if (walksDomainModel == null) return NotFound();
            return Ok(_mapper.Map<WalkDto>(walksDomainModel));
        }
    }
}
