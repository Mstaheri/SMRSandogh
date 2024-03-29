﻿using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepositorie
    {
        ValueTask AddAsync(User user);
        Task DeleteAsync(UserName userName);
        Task<User> GetAsync(UserName userName);
        Task<List<User>> GetAllAsync();
    }
}
