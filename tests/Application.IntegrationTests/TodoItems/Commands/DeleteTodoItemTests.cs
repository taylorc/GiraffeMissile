using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.TodoItems.Commands.CreateTodoItem;
using GiraffeMissile.Application.TodoItems.Commands.DeleteTodoItem;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using FluentAssertions;
using System.Threading.Tasks;
using GiraffeMissile.Domain.Entities;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().BeNull();
        }
    }
}
