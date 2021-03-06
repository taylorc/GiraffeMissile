using System;
using System.Threading.Tasks;
using FluentAssertions;
using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using GiraffeMissile.Domain.Entities;
using NUnit.Framework;

namespace GiraffeMissile.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class CreateTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateTodoListCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            await SendAsync(new CreateTodoListCommand
            {
                Title = "Shopping"
            });

            var command = new CreateTodoListCommand
            {
                Title = "Shopping"
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateTodoListCommand
            {
                Title = "Tasks"
            };

            var id = await SendAsync(command);

            var list = await FindAsync<TodoList>(id);

            list.Should().NotBeNull();
            list.Title.Should().Be(command.Title);
            list.CreatedBy.Should().Be(userId);
            list.Created.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 0, 5));
        }
    }
}
