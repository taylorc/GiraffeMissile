using System;
using System.Threading.Tasks;
using FluentAssertions;
using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using GiraffeMissile.Application.TodoLists.Commands.UpdateTodoList;
using GiraffeMissile.Domain.Entities;
using NUnit.Framework;

namespace GiraffeMissile.Application.IntegrationTests.TodoLists.Commands
{
    using static Testing;

    public class UpdateTodoListTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoListId()
        {
            var command = new UpdateTodoListCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueTitle()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            await SendAsync(new CreateTodoListCommand
            {
                Title = "Other List"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Other List"
            };

            var error = await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title"));

            error.And.Errors["Title"].Should().Contain("The specified title already exists.");
        }

        [Test]
        public async Task ShouldUpdateTodoList()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var command = new UpdateTodoListCommand
            {
                Id = listId,
                Title = "Updated List Title"
            };

            await SendAsync(command);

            var list = await FindAsync<TodoList>(listId);

            list.Title.Should().Be(command.Title);
            list.LastModifiedBy.Should().NotBeNull();
            list.LastModifiedBy.Should().Be(userId);
            list.LastModified.Should().NotBeNull();
            list.LastModified.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 0, 5));
        }
    }
}
