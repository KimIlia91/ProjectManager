﻿using ErrorOr;
using MediatR;
using PM.Application.Features.StatusContext.Dtos;

namespace PM.Application.Features.StatusContext.Queries.GetStatusList;

/// <summary>
/// Represents a query to retrieve a list of statuses.
/// </summary>
public sealed record GetStatusListQuery : IRequest<ErrorOr<List<GetStatusListResult>>>;