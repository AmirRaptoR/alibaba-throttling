using Alibaba.Heracles.Application.Common.Exceptions;
using Alibaba.Heracles.Application.TodoLists.Commands.CreateTodoList;
using Alibaba.Heracles.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using Alibaba.Heracles.Application.Throttlings.Commands.Create;
using Alibaba.Heracles.Application.Throttlings.Commands.Delete;
using NUnit.Framework;

namespace Alibaba.Heracles.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteThrottlingCommand {Id = 99};

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

            var itemId = await SendAsync(new CreateThrottlingCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteThrottlingCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(listId);

            list.Should().BeNull();
        }
    }
}