using AutoMapper;
using DataAccess.Context;
using EntityModels.DTOs;
using EntityModels.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.CheckDtoValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer.Services.BlogServices
{
    public interface IBlogService
    {
        #region Post
        public Task<TaskStatus> AddPost(PostDto post);
        public void UpdatePost(PostDto post);
        public void DeletePost(PostDto post);
        public Task<PostDto> FindPostAsync(int id);
        public List<PostDto> FindPosts(string titleOrKeyword);
        public List<PostDto> GetAllPosts();
        public List<PostDto> FindPosts(int count, int page);

        #endregion


        #region Comment
        public Task AddCommentAsync(PostComment comment);
        public void ActivateComments(IEnumerable<PostComment> activate);
        public void DeleteComment(int id);

        #endregion

        #region Image
        public void AddImage(PostImage image);
        public void AddImage(IEnumerable<PostImage> images);
        public void RemoveImage(int id);

        #endregion

        #region Keyword

        public void AddKeywords(IEnumerable<PostKeyword> keys);
        public void RemoveKeywords(IEnumerable<PostKeyword> keys);

        #endregion


        #region base
        Task SaveChangesAsync();
        #endregion

    }

    public class BlogService : IBlogService
    {
        private readonly ShopDbContext _ctx;
        private readonly IMapper mapper;

        public BlogService(ShopDbContext dbContext, MyMapper mapper)
        {
            this._ctx = dbContext;
            this.mapper = mapper.CreateMappings();

        }


        #region comment
        /// <summary>
        /// this method need SaveChangesAsyncWithOutDetect
        /// </summary>
        /// <param name="activate"></param>

        public void ActivateComments(IEnumerable<PostComment> activate)
        {

            foreach (var item in activate)
            {
                _ctx.Entry(item).State = EntityState.Unchanged;
                _ctx.Entry(item).Property(x => x.IsActive).IsModified = true;
            }

        }

        public async Task AddCommentAsync(PostComment comment)
        {
            await _ctx.PostComment.AddAsync(comment);
        }
        public void DeleteComment(int id)
        {
            var comment = new PostComment()
            {
                Id = id
            };
            _ctx.Entry(comment).State = EntityState.Deleted;

        }



        #endregion

        #region post
        public void UpdatePost(PostDto postDto)
        {
            var OldPost = _ctx.Post.Find(postDto.Id);
             OldPost =(Post)mapper.Map(postDto,OldPost,typeof(PostDto),typeof(Post));
            var res = _ctx.Entry(OldPost).State;
            _ctx.SaveChanges();
            //foreach (var item in _ctx.Entry(OldPost).Properties)
            //{
            //    foreach (var subitem in post.GetType().GetProperties())
            //    {
            //        if (item.Metadata.Name == subitem.Name)
            //        {
            //            var valueOfNewObject = post.GetType().GetProperty(subitem.Name).GetValue(post, null);

            //            if (valueOfNewObject == null)
            //                break;

            //            if (valueOfNewObject.Equals(item.CurrentValue)==false)
            //            {
            //                item.CurrentValue = valueOfNewObject;
            //            }
            //            break;
            //        }
            //    }
            //}


        }
        public async Task<TaskStatus> AddPost(PostDto postDto)
        {
            if (CheckValidation.CheckObjectIsValid<PostDto>(postDto))
            {
                var post = mapper.Map<Post>(postDto);
                await _ctx.Post.AddAsync(post);
                return TaskStatus.RanToCompletion;
            }
            else
            {
                return TaskStatus.Faulted;
            }
        }

        public void DeletePost(PostDto post)
        {
            if (_ctx.Post.Any(x => x.Id == post.Id))
            {
                _ctx.Post.Remove(new Post()
                {
                    Id = post.Id
                });
            }

        }
        public async Task<PostDto> FindPostAsync(int id)
        {
            var BasePost = await _ctx.Post.FindAsync(id);
            if (BasePost == null)
            {
                return null;
            }
            await _ctx.Entry(BasePost).Collection(x => x.Keywords).LoadAsync();
            await _ctx.Entry(BasePost).Collection(x => x.Comments).Query().Where(x => x.IsActive == true).ToListAsync();
            await _ctx.Entry(BasePost).Collection(x => x.Images).LoadAsync();
            return mapper.Map<PostDto>(BasePost);
        }
        #endregion

        public void AddImage(PostImage image)
        {
            throw new NotImplementedException();
        }

        public void AddImage(IEnumerable<PostImage> images)
        {
            throw new NotImplementedException();
        }

        public void AddKeywords(IEnumerable<PostKeyword> keys)
        {
            throw new NotImplementedException();
        }









        public List<PostDto> FindPosts(string titleOrKeyword)
        {
            throw new NotImplementedException();
        }

        public List<PostDto> FindPosts(int count, int page)
        {
            throw new NotImplementedException();
        }

        public List<PostDto> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public void RemoveImage(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveKeywords(IEnumerable<PostKeyword> keys)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task SaveChangesAsyncWithOutDetect()
        {

            _ctx.ChangeTracker.AutoDetectChangesEnabled = false;

            await _ctx.SaveChangesAsync();
        }


    }
}
