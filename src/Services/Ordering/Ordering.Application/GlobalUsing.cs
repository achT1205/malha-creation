﻿global using BuildingBlocks.CQRS;
global using Ordering.Application.Dtos;
global using Ordering.Domain.ValueObjects;
global using FluentValidation;
global using BuildingBlocks.Exceptions;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using BuildingBlocks.Pagination;
global using MassTransit;
global using Microsoft.FeatureManagement;
global using Ordering.Application.Abstractions.Data;
global using Ordering.Application.Orders.Exceptions;
global using Ordering.Domain.Orders.Models;
global using Microsoft.EntityFrameworkCore;
global using Ordering.Application.Orders.Extensions;
global using Ordering.Application.Abstractions.Services;
global using Ordering.Application.Orders.Helpers;
global using Ordering.Application.Abstractions.Models;
global using Ordering.Domain.Enums;
global using Ordering.Domain.Orders.Events;
global using BuildingBlocks.Messaging.Events;