﻿namespace FastFood.Core.Controllers
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Models;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Orders;

    public class OrdersController : Controller
    {
        private readonly FastFoodContext context;
        private readonly IMapper mapper;

        public OrdersController(FastFoodContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Create()
        {
            var viewOrder = new CreateOrderViewModel
            {
                Items = this.context.Items.Select(x => x.Id).ToList(),
                Employees = this.context.Employees.Select(x => x.Id).ToList(),
            };

            return this.View(viewOrder);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderInputModel model)
        {
            var order = this.mapper.Map<Order>(model);

            order.OrderItems.Add(new OrderItem { ItemId = model.ItemId });

            this.context.Orders.Add(order);
            this.context.SaveChanges();
            return this.RedirectToAction("All", "Orders");
        }

        public IActionResult All()
        {
            var allOrders = this.context.Orders.ProjectTo<OrderAllViewModel>(this.mapper.ConfigurationProvider).ToList();
            return this.View(allOrders);
        }
    }
}
