using System;
using PostService.Dto;
using PostService.Model;
using PostService.Service.Interface;
using PostService.Service.Interface.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenTracing.Mock;
using Xunit;
using PostService.Controllers;
using PostService.Repository.Interface.Pagination;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using PostService.Service;
using PostService.Repository.Interface;

namespace UnitTests.ServiceTests
{
    public class CommentServiceTests
    {
        private static readonly Guid id = Guid.NewGuid();
        private static readonly Guid authorId = Guid.NewGuid();
        private static readonly Guid postId = Guid.NewGuid();
        private static readonly string content = "test content";

        private static Comment comment;

        private static Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();

        CommentService commentService = new CommentService(mockCommentRepository.Object);

        private static void SetUp()
        {
            
            comment = new Comment()
            {
                Id = id,
                Content = content,
                AuthorId = authorId,
                PostId = postId
            };
        }

        [Fact]
        public async void Save_ValidRequest_SaveComment()
        {
            SetUp();

            mockCommentRepository
                .Setup(repository => repository.Save(comment))
                .ReturnsAsync(comment);

            var response = await commentService.Save(comment);

            var actionValue = Assert.IsType<Comment>(response);
            Assert.Equal(actionValue, comment);
        }

    }
}
