﻿namespace Ordering.Application.Orders.Queries.GetOrders;
public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;
