﻿using AutoMapper;
using Blog.Data.Models;
using Blog.Contracts.Models.Tags;
using Blog.Contracts.Models.Posts;
using Blog.Contracts.Models.Users;
using Blog.Contracts.Models.Comments;

namespace BlogApi
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<Tag, TagsView>();
            CreateMap<Tag, TagView>();
            CreateMap<AddTagRequest, Tag>();

            CreateMap<AddPostRequest, Post>();
            CreateMap<Post, AllPostsView>();
            CreateMap<Post, GetPostByIdView>();
            CreateMap<Post, UserPostsView>();

           // CreateMap<AddUserRequest, User>();
            CreateMap<User, UserView>();
            CreateMap<User, UsersView>();

            CreateMap<Comment, CommentsView>();
            CreateMap<Comment, CommentView>();
            CreateMap<AddCommentRequest, Comment>();


            
            CreateMap<UpdateUserRequest, User>();
            CreateMap<AuthorizeUserRequest, User>();

            CreateMap<AddUserRequest, User>()
             .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
             .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));


        }
    }
}
