using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewZealandWalks.Models.Domain;
using NewZealandWalks.Models.Dto.Region;
using NewZealandWalks.Repository.Interfaces;

namespace NewZealandWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger1;

        public RegionsController(IRegionRepository repository,IMapper mapper,ILogger<RegionsController> logger1)
        {
            this._repository = repository;
            _mapper = mapper;
            this.logger1 = logger1;
        }

        // Get all Region
        // https://localhost:PortNumber/api/Regions
        [HttpGet]
        //[Authorize(Roles ="reader")]
        public async Task<IActionResult> GetAll()
        {
            throw new Exception("This is a exception for testing middleware.");

            logger1.LogInformation("this is info");
            logger1.LogError("this is error");
            logger1.LogWarning("This is warning");
            // Get Data From DataBase - Domain Models
            var regions= await _repository.GetAllAsync();

            // Creating a List of RegionDto
            var regiondto =new List<RegionDto>();

            if (regions.Any()) {

                // automapping Domain Model to RegionDto and return to client
                return Ok(_mapper.Map<List<RegionDto>>(regions));
            }

            return BadRequest();
        }

        // Get a Region by Id
        // https://localhost:PortNumber/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] // {id:Guid} making type safe id have to be Guid
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Get Data From DataBase - Domain Models
            var region = await _repository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            // Region Domain Model => RegionDto and send to client
            return Ok(_mapper.Map<RegionDto>(region));
        }

        // Create a Region
        // https://localhost:PortNumber/api/Regions
        [HttpPost]
        [Authorize(Roles ="writer")]
        public async Task<IActionResult> Create(CreateRegionDto createregiondto)
        {
            // Convert createregiondto to domain model
            var regionmodel = _mapper.Map<Region>(createregiondto);

            // Saving changes to Database
            regionmodel=await _repository.CreateAsync(regionmodel);

            // mapping region model => RegionDto inorder to send back to client
            var regiondto = _mapper.Map<RegionDto>(regionmodel);

            // CreatedAtAction sends back a 201 status code with response body
            return CreatedAtAction(nameof(GetById),new { id = regiondto.Id },regiondto);
        }

        // Update a Region
        // https://localhost:PortNumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> Update(Guid id,UpdateRegionDto updateRegionDto)
        {
            // mapping UpdateRegionDto to Region Domain Model
            var regionModel = _mapper.Map<Region>(updateRegionDto); 

            // get region detail and check region exists, if not return null else update region detail to database
            var regionDomainModel = await _repository.UpdateAsync(id, regionModel);
            if (regionDomainModel == null) return NotFound();

            // Region Domain Model => RegionDto and send to client
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        // Delete a Region
        // https://localhost:PortNumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // check region exists else send 404 response else remove region from dbContext
            var regionDomainModel = await _repository.DeleteAsync(id);
            if (regionDomainModel == null) return NotFound();

            // return removed region after mapping regionDomainModel => RegionDto
            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }


    }
}
