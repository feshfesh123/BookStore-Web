using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly DataContext dataContext;

        public DashboardController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var model = new RevenueModel();

            var now = DateTime.Now;

            var orders = dataContext.Orders.Where(x => x.Status == "đã giao hàng" && x.DateTime >= now.AddDays(-7)).ToList();

            model.Total = orders.Sum(x => x.TotalPrice);

            var dayofweek = new List<DayOfWeek>(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday });

            model.Labels = new List<string>(new string[] { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN" });

            model.Values = new List<double>();

            foreach (var i in dayofweek)
            {
                var ordersInDay = orders != null ? orders.Where(x => x.DateTime.DayOfWeek == i).ToList() : null;

                double sum = ordersInDay != null ? ordersInDay.Sum(x => x.TotalPrice) : 0;

                model.Values.Add(sum);

                model.MaxValue = sum > model.MaxValue ? sum : model.MaxValue;
            }

            return View(model);
        }
    }
}
