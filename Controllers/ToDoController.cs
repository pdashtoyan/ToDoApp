using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private static List<Todo> ToDoItems = new List<Todo>();
        static int userID = 1;





        // GET: api/<ToDoController>
        [HttpGet("Get/Items")]
        public ActionResult<List<Todo>> GetToDo()
        {
            return Ok(ToDoItems);
        }





        // GET api/<ToDoController>/5
        [HttpGet("Get/ItemById")]
        public ActionResult<Todo> GetById(int id)
        {
            var resource = ToDoItems.FirstOrDefault(r => r.Id == id);
            if (resource != null)
            {
                return resource;
            }

            return NotFound();
        }





        // POST api/<ToDoController>
        [HttpPost("Add/NewItem")]
        public ActionResult<Todo> AddToDo([FromBody] Todo value)
        {
            foreach (var item in ToDoItems)
            {
                if (item.Id == value.Id)
                {
                    throw new Exception("the item already exists");
                }
            }



            value.Id = userID++;

            ToDoItems.Add(value);

            return Ok(value);
        }






        // PUT api/<ToDoController>/5
        [HttpPost("Update/Item")]
        public ActionResult UpdateToDo([FromBody] Todo value)
        {
            var resource = ToDoItems.FirstOrDefault(r => r.Id == value.Id);
            if (resource != null)
            {
                resource.Title = value.Title;
                resource.IsImportant = value.IsImportant;
                resource.Date = value.Date;

                return Ok(value);
            }

            return NotFound(new List<Todo>());
        }





        // DELETE api/<ToDoController>/5
        [HttpDelete("Delete/Item")]
        public ActionResult DeleteToDo(int id)
        {
            var resource = ToDoItems.FirstOrDefault(r => r.Id == id);

            if (resource != null)
            {
                ToDoItems.Remove(resource);

                return NoContent();
            }
            return BadRequest();
        }




        [HttpGet("Get/ByImportant")]
        public ActionResult GetByImportant(bool? isImportant)
        {
            if (isImportant == null)
            {
                return Ok(ToDoItems);
            }

            // List<Todo> Importants = new List<Todo>();


            //for (int i = 0; i < ToDoItems.Count; i++)
            //{
            //    if (ToDoItems[i].IsImportant == isImportant)
            //        Importants.Add(ToDoItems[i]);
            //}


            //IEnumerable<Todo> Importants = from item in ToDoItems
            //                               where item.IsImportant == isImportant
            //                               select item;



            IEnumerable<Todo> Importants = ToDoItems.Where(item => item.IsImportant == isImportant);


            if (Importants.Any())
            {
                return Ok(Importants);
            }
            return BadRequest(new List<Todo>());
        }




        [HttpGet("Get/ByDone")]
        public ActionResult GetByDone()
        {
            if (ToDoItems.Any())
            {
                foreach (var todo in ToDoItems)
                {
                    if (todo.Date < DateTime.Today)
                    {
                        todo.IsDone = true;
                    }
                    else
                    {
                        todo.IsDone = false;
                    }
                }
                return Ok(ToDoItems.ToList());
            }

            return Ok(new List<Todo>());







            //if (value.Date < DateTime.Today)
            //{
            //    value.IsDone = true;
            //}
            //else
            //{
            //    value.IsDone = false;
            //}
            //if (isDone == null)
            //{
            //    return Ok(ToDoItems);
            //}
            //IEnumerable<Todo> Done = ToDoItems.Where(item => item.IsDone == isDone);


            //if (Done.Any())
            //{
            //    return Ok(Done);
            //}
            //return BadRequest(new List<Todo>());


        }
    }
}

