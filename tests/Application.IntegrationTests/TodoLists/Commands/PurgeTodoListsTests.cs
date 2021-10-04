using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.Common.Security;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using GiraffeMissile.Application.TodoLists.Commands.PurgeTodoLists;
using GiraffeMissile.Application.TodoLists.Queries.ExportTodos;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;

namespace CleanArchitecture.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class PurgeTodoListsTests : TestBase
    {
        [Test]
        public void ShouldDenyAnonymousUser()
        {
            var command = new PurgeTodoListsCommand();

            command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Test]
        public async Task ShouldDenyNonAdministrator()
        {
            await RunAsDefaultUserAsync();

            var command = new PurgeTodoListsCommand();

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldAllowAdministrator()
        {
            await RunAsAdministratorAsync();

            var command = new PurgeTodoListsCommand();

            await FluentActions.Invoking(() => SendAsync(command))
                .Should().NotThrowAsync<ForbiddenAccessException>();
        }

        [Test]
        public async Task ShouldDeleteAllLists()
        {
            await RunAsAdministratorAsync();

            await SendAsync(new CreateTodoListCommand
            {
                Title = "New List #1"
            });

            await SendAsync(new CreateTodoListCommand
            {
                Title = "New List #2"
            });

            await SendAsync(new CreateTodoListCommand
            {
                Title = "New List #3"
            });

            await SendAsync(new PurgeTodoListsCommand());

            var count = await CountAsync<TodoList>();

            count.Should().Be(0);
        }
    }
}
