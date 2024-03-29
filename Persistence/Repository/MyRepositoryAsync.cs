﻿using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    /// <MyRepositoryAsync>
    /// The class is created in the repository where the logic will be called to have the functionalities with the database
    /// </MyRepositoryAsync>
    /// <typeparam name="T"></typeparam>
    public class MyRepositoryAsync<T> : RepositoryBase <T>, IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public MyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
