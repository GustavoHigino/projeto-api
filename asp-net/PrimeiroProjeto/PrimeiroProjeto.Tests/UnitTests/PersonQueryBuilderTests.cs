using FluentAssertions;
using PrimeiroProjeto.Repositories.QueryBuilders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeiroProjeto.Tests.UnitTests
{
    public class PersonQueryBuilderTests
    {
        private readonly PersonQueryBuilder _queryBuilder;
        public PersonQueryBuilderTests()
        {
            _queryBuilder = new PersonQueryBuilder();
        }
        [Fact]
        public void BuildQueries_ShouldReturnCorrectQueries()
        {
            var name = "John";
            var sortDirection = "asc";
            var pageSize = 10;
            var page = 1;

            var (query, countQuery, sort, size, offset) =
                _queryBuilder.buildQueries(name,
                sortDirection, pageSize, page);
            query.Should().Contain(
                "SELECT *  FROM person p WHERE 1 = 1  AND (p.first_name LIKE '%John%')");
            query.Should().Contain(
                "ORDER BY p.first_name asc");
            query.Should().Contain(
                "OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY");
            countQuery.Should().Contain(
                "SELECT COUNT(*)  FROM person p WHERE 1 = 1  AND (p.first_name LIKE '%John%')");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(0);

        }
        [Fact]
        public void 
            BuildQueries_ShouldHandleInvalidPageAndSize()
        {
            var name = "";
            var sortDirection = "desc";
            var pageSize = 0;
            var page = -1;
            var (query, countQuery, sort, size, offset) =
                _queryBuilder.buildQueries(name,
                sortDirection, pageSize, page);
            query.Should().Contain(
                "SELECT *  FROM person p WHERE 1 = 1");
            query.Should().Contain(
                "ORDER BY p.first_name desc");
            query.Should().Contain(
                "OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY");
            countQuery.Should().Contain(
                "SELECT COUNT(*)  FROM person p WHERE 1 = 1");
            sort.Should().Be("desc");
            size.Should().Be(1);
            offset.Should().Be(0);
        }
        [Fact]
        public void
            BuildQueries_ShouldHandleNullOrWhiteSpaceName()
        {
            string name = null;
            var sortDirection = "asc";
            var pageSize = 5;
            var page = 1;
            var (query, countQuery, sort, size, offset) =
                _queryBuilder.buildQueries(name,
                sortDirection, pageSize, page);
            query.Should().Contain(
                "SELECT *  FROM person p WHERE 1 = 1");
            query.Should().Contain(
                "ORDER BY p.first_name asc");
            query.Should().Contain(
                "OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY");
            query.Should().NotContain("p.first_name LIKE");
            countQuery.Should().Contain(
                "SELECT COUNT(*)  FROM person p WHERE 1 = 1");
            sort.Should().Be("asc");
            size.Should().Be(5);
            offset.Should().Be(0);
        }
        [Fact]
        public void
            BuildQueries_ShouldDefaultToDescForInvalidSortDirection()
        {
            string name = "Jane";
            var sortDirection = "invalid";
            var pageSize = 10;
            var page = 1;
            var (query, countQuery, sort, size, offset) =
                _queryBuilder.buildQueries(name,
                sortDirection, pageSize, page);
            query.Should().Contain(
                "SELECT *  FROM person p WHERE 1 = 1  AND (p.first_name LIKE '%Jane%')");
            query.Should().Contain(
                "ORDER BY p.first_name asc");
            query.Should().Contain(
                "OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY");
            countQuery.Should().Contain(
                "SELECT COUNT(*)  FROM person p WHERE 1 = 1  AND (p.first_name LIKE '%Jane%')");
            sort.Should().Be("asc");
            size.Should().Be(10);
            offset.Should().Be(0);
        }

    }
}
