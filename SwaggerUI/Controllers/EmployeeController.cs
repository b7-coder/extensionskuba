using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using SwaggerUI.Models;
using SwaggerUI.Services;

namespace SwaggerUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Employee
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "emailId": "Mike.Andrew@gmail.com"        
        ///     }
        /// </remarks>
        /// <param name="employee"></param>
        /// <returns>A newly created employee</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>          
        [HttpPost]
        //[ProducesResponseType(201)]
        //[ProducesResponseType(400)]
        [Produces("application/json")]
        public RequestBody Post([FromBody] Url arg)
        {
            if(String.IsNullOrEmpty(arg.Value))
                return new RequestBody() { Status=400, Detail="Ошибка в параметрах" };

            DataBase db = new DataBase();

            if (db.IsInDataBase(arg.Value))
                return new RequestBody() { Status = 403, Detail = "Такая запись существует в базе данных" };

            db.InsertData(arg.Value);

            return new RequestBody() { Status=200, Detail="Успешно" };
        }


        /*
        [HttpPost]
        //[ProducesResponseType(201)]
        //[ProducesResponseType(400)]
        [Produces("application/json")]
        public PostGetAllReturnBody PostGetAll([FromBody] PostGetAllBody arg)
        {
            //if (CheckRequest(arg))
            //    return new PostGetAllReturnBody() { Status = 400, Detail = "Ошибка в параметрах" };

            
            // double pagesCountDouble = Math.Ceiling(rowsCount * 1.0 / pageSize);
            //    int pagesCount = Convert.ToInt32(pagesCountDouble);
            // .Skip(pageSize * (pageIndex - 1))
            //        .Take(pageSize)

            //GetCount
             

        DataBase db = new DataBase();

            var table = db.DataSource(arg.Page, arg.PageSize);

            List<Url> urls = new List<Url>();
            foreach (DataRow i in table.Rows)
                urls.Add(new Url { Value = i[0].ToString() });

            double pagesCountDouble = Math.Ceiling(db.GetCount() * 1.0 / arg.PageSize);
            int pagesCount = Convert.ToInt32(pagesCountDouble);

            var answer = new PostGetAllReturnBody() { Status = 200, Detail = "Ошибка в параметрах", PagesCount = pagesCount, PageSize = arg.PageSize, Urls = urls };

            return answer;
        }

        
        private bool CheckRequest(PostGetAllBody arg) 
        {
            if (arg == null)
                return true;
            if (arg.Page <= 0)
                return true;
            if (arg.PageSize <= 0)
                return true;

            return false;
        }*/
    }
}
