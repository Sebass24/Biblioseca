﻿using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.Test
{
    [TestClass]
    public class BookDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session); //hack para tener el config de la session
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            BookDao bookDao = new BookDao(this.sessionFactory);

            IEnumerable<Book> books = bookDao.GetAll();

            Assert.IsTrue(books.Any());
        }

        [TestMethod]
        public void GetByFilter()
        {
            BookDao bookDao = new BookDao(this.sessionFactory);

            BookFilterDto bookFilterDto = new BookFilterDto
            {
                Title = "De la estratosfera a Japón",
                AuthorFirstName = "Carlitos"
            };

            IEnumerable<Book> books = bookDao.GetByFilter(bookFilterDto);

            Assert.IsTrue(books.Any());
        }

    }
}