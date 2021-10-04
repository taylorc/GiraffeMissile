﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using GiraffeMissile.Application.Common.Exceptions;
using GiraffeMissile.Application.TodoItems.Commands.CreateTodoItem;
using GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItem;
using GiraffeMissile.Application.TodoItems.Commands.UpdateTodoItemDetail;
using GiraffeMissile.Application.TodoLists.Commands.CreateTodoList;
using GiraffeMissile.Domain.Entities;
using GiraffeMissile.Domain.Enums;
using NUnit.Framework;

namespace GiraffeMissile.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemDetailTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            var command = new UpdateTodoItemDetailCommand
            {
                Id = itemId,
                ListId = listId,
                Note = "This is the note.",
                Priority = PriorityLevel.High
            };

            await SendAsync(command);

            var item = await FindAsync<TodoItem>(itemId);

            item.ListId.Should().Be(command.ListId);
            item.Note.Should().Be(command.Note);
            item.Priority.Should().Be(command.Priority);
            item.LastModifiedBy.Should().NotBeNull();
            item.LastModifiedBy.Should().Be(userId);
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, new TimeSpan(0, 0, 0, 5));
        }
    }
}
