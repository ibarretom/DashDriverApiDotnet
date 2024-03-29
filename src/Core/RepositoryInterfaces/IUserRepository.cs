﻿using Core.Domain.Entities;

namespace Core.RepositoryInterfaces;

public interface IUserRepository
{
    Task<User> Create(User user);
    Task<User> FindByEmail(string email);
}
