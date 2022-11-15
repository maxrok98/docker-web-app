using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class EntityController : ControllerBase {

    private AppDbContext _context;

    public EntityController(AppDbContext context) {
      _context = context;
    }

    [HttpGet]
    public IActionResult GetAllEntities() {
      return Ok(_context.Entities.Select(e => new { Id = e.EntityId, Title = e.Title }).ToArray());
    }

    [HttpGet("{id}")]
    public IActionResult GetEntity(int id) {
      return Ok(_context.Entities.Where(e => e.EntityId == id).FirstOrDefault());
    }

    [HttpPost]
    public IActionResult CreateNewEntity(EntityDto entityDto) {
      _context.Entities.Add(new Entity {Title = entityDto.Title, Description = entityDto.Description });
      _context.SaveChanges();
      return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateEntity(int id, EntityDto entityDto) {
      var entity = _context.Entities.Where(e => e.EntityId == id).FirstOrDefault();
      if (entity == null) {
        return NotFound();
      }
      entity.Title = entityDto.Title;
      entity.Description = entityDto.Description;

      _context.Entities.Update(entity);
      _context.SaveChanges();
      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEntity(int id) {
      var entity = _context.Entities.Where(e => e.EntityId == id).FirstOrDefault();
      if (entity == null) {
        return NotFound();
      }

      _context.Entities.Remove(entity);
      _context.SaveChanges();
      return Ok();
    }
  }
}
