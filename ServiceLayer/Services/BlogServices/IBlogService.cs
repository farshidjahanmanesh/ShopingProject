using AutoMapper;
using DataAccess.Context;
using EntityModels.DTOs;
using EntityModels.DTOs.PostDtos;
using EntityModels.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.CheckDtoValidations;
using ServiceLayer.QueryExtenstion;
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
        public Task<TaskStatus> AddPostAsync(PostDto post);
        public void UpdatePost(PostDto post);
        public void DeletePost(PostDto post);
        public Task<PostDto> FindPostAsync(int id);
        public List<PostDto> FindPosts(string titleOrKeyword);
        public List<PostDto> GetAllPosts();
        public List<PostSummeryDto> FindPosts(int count, int page, string titleOrKeyword);

        #endregion


        #region Comment
        public Task AddCommentAsync(PostCommentDto comment);
        public void ActivateComments(IEnumerable<PostComment> activate);
        public void DeleteComment(int id);
        public List<SummeryLastCommentDto> LastComments(int count);
        

        #endregion

        
        #region base
        Task SaveChangesAsync();
        Task SaveChangesAsyncWithOutDetect();
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

        public async Task AddCommentAsync(PostCommentDto comment)
        {
            var result = mapper.Map<PostComment>(comment);
            await _ctx.PostComment.AddAsync(result);
        }
        public void DeleteComment(int id)
        {
            var comment = new PostComment()
            {
                Id = id
            };
            _ctx.Entry(comment).State = EntityState.Deleted;

        }

        public List<SummeryLastCommentDto> LastComments(int count)
        {
            var len = _ctx.PostComment.Count();
           var comments= _ctx.PostComment.AsNoTracking()
                .OrderByDescending(x => x.PublishDate)
                .Skip(len-count>0?len-count:0)
                .Take(count);
            var result = mapper.Map<List<SummeryLastCommentDto>>(comments);
            return result;
        }

        #endregion

        #region post
        public void UpdatePost(PostDto postDto)
        {
            var OldPost = _ctx.Post.Find(postDto.Id);
            OldPost = (Post)mapper.Map(postDto, OldPost, typeof(PostDto), typeof(Post));
          
        }
        public async Task<TaskStatus> AddPostAsync(PostDto postDto)
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

        public List<PostDto> FindPosts(string titleOrKeyword)
        {
            if (titleOrKeyword == "" || titleOrKeyword == null)
            {
                return null;
            }
            var DtoPostList = new List<PostDto>();

            //get post when keyword is contain of input and then mapping it to type postdto
            //then add this collection to dtopostlist
            DtoPostList.AddRange(mapper.Map<List<PostDto>>(_ctx.PostKeyword.AsNoTracking().Where(x => (titleOrKeyword == "" || titleOrKeyword == null) || x.Text.Contains(titleOrKeyword))
                .Include(x => x.Post)?.Select(x => x.Post).ToList()));

            DtoPostList.AddRange(mapper.Map<List<PostDto>>(_ctx.Post.AsNoTracking().Where(x => (titleOrKeyword == "" || titleOrKeyword == null) || x.Title.Contains(titleOrKeyword))));

            return DtoPostList;
        }

        public List<PostSummeryDto> FindPosts(int count, int page, string titleOrKeyword)
        {
            if ( count < 0 || page < 0)
            {
                return null;
            }
            var DtoPostList = new List<PostSummeryDto>();

            
            //get post when keyword is contain of input and then mapping it to type postdto
            //then add this collection to dtopostlist
            DtoPostList.AddRange(mapper.Map<List<PostSummeryDto>>(_ctx.PostKeyword.AsNoTracking().Where(x =>(titleOrKeyword==""||titleOrKeyword==null)|| x.Text.Contains(titleOrKeyword))
                .Include(x => x.Post)?.Select(x => x.Post).SkipAndTake(page,count,QueryExtension.OrderBy.date).ToList()));

            DtoPostList.AddRange(mapper.Map<List<PostSummeryDto>>(
                _ctx.Post.AsNoTracking().Where(x => (titleOrKeyword == "" || titleOrKeyword == null) || x.Title.Contains(titleOrKeyword))
                .SkipAndTake(page, count, QueryExtension.OrderBy.date)));

            return DtoPostList;
        }

        public List<PostDto> GetAllPosts()
        {
            return mapper.Map<List<PostDto>>(_ctx.Post.AsNoTracking().ToList());
        }



        #endregion


        #region base

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task SaveChangesAsyncWithOutDetect()
        {

            _ctx.ChangeTracker.AutoDetectChangesEnabled = false;

            await _ctx.SaveChangesAsync();
        }

        #endregion
    }
}
