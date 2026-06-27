using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using WebAPI2.DTO;
using WebAPI2.Model;
namespace WebAPI2.Filters
{
    public class DepartmentActionLogFilter : IActionFilter
    {
        private readonly DepartmentDB _context;

        public DepartmentActionLogFilter(DepartmentDB context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("department"))
            {
                context.HttpContext.Items["CreatedDepartment"] = context.ActionArguments["department"];
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.ActionDescriptor.DisplayName.Contains("CreateDepartment"))
            {
                var department = context.HttpContext.Items["CreatedDepartment"] as Department;

                var log = new DepartmentLog
                {
                    DepartmentName = department?.DepartmentName,
                    ActionName = "CreateDepartment",
                    Timestamp = DateTime.Now
                };

                _context.DepartmentLogs.Add(log);
                _context.SaveChanges();
            }
        }
}
}
