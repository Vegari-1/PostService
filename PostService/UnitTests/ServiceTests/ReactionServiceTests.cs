//using System;
//using PostService.Dto;
//using PostService.Model;
//using PostService.Service.Interface;
//using PostService.Service.Interface.Exceptions;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using OpenTracing.Mock;
//using Xunit;
//using PostService.Controllers;
//using PostService.Repository.Interface.Pagination;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using PostService.Repository.Interface;

//namespace UnitTests.ServiceTests
//{
//    public class ReactionServiceTests
//    {
//        private static readonly Guid id = Guid.NewGuid();
//        private static readonly Guid authorId = Guid.NewGuid();
//        private static readonly Guid postId = Guid.NewGuid();
//        private static readonly bool positive = true;

//        private static Reaction reaction;

//        private static Mock<IReactionRepository> mockReactionRepository = new Mock<IReactionRepository>();

//        ReactionService reactionService = new ReactionService(mockReactionRepository.Object);


//        private static void SetUp()
//        {
//            reaction = new Reaction()
//            {
//                Id = id,
//                Positive = positive,
//                AuthorId = authorId,
//                PostId = postId
//            };
//        }


//        [Fact]
//        public async void Save_ValidRequest_SaveReaction()
//        {
//            SetUp();

//            mockReactionRepository
//                .Setup(repository => repository.Save(reaction))
//                .ReturnsAsync(reaction);

//            var response = await reactionService.Save(reaction);

//            var actionValue = Assert.IsType<Reaction>(response);
//            Assert.Equal(actionValue, reaction);
//        }
//    }
//}
