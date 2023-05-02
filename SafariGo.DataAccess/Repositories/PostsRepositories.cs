using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SafariGo.Core.Dto.Request.Posts;
using SafariGo.Core.Dto.Response;
using SafariGo.Core.Models;
using SafariGo.Core.Repositories;
using SafariGo.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.DataAccess.Repositories
{
    public class PostsRepositories : IPostsRepositories
    {
        const string defaultProfilePicture = "https://cdn-icons-png.flaticon.com/512/847/847969.png?w=740&t=st=1683038294~exp=1683038894~hmac=ff15f90aa346ed552d76f40cc8c100ccc3405550ac77655202761ba2dcaf3865";
        private readonly ApplicationDbContext _context;
        private readonly ICloudinaryServices _cloudinary;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsRepositories(ApplicationDbContext context, ICloudinaryServices cloudinary, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _cloudinary = cloudinary;
            _userManager = userManager;
        }

        public async Task<BaseResponse> CreateCommentAsync(CommentRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) == null || await _context.Posts.FindAsync(request.PostId) == null)
                return new BaseResponse { Message = "Something is wrong" };

            if (string.IsNullOrEmpty(request.description) && request.Image == null)
                return new BaseResponse { Message = "An empty comment cannot be created" };

            var image = string.Empty;
            if (!(request.Image == null))
            {
                var uploadPoster = await _cloudinary.UploadAsync(request.Image);
                if (!uploadPoster.Status)
                    return new BaseResponse { Message = uploadPoster.Message };
                image = uploadPoster.Data.ToString();
            }
            var comment = new Comment
            {
                UserId = request.UserId,
                PostId = request.PostId,
                description = request.description,
                Image = string.IsNullOrEmpty(image)? null :image
            };
            await _context.Comments.AddAsync(comment);
            _context.SaveChanges();
            return new BaseResponse { Status = true, Message = "Comment created successfully" };

        }
        public async Task<BaseResponse> DeleteCommentAsync(string CommentId)
        {

            var comment = await _context.Comments.FindAsync(CommentId);
            if (comment == null)
                return new BaseResponse { Message = "Invalid Comment Id" };
            if (!string.IsNullOrEmpty(comment.Image))
            {
                await _cloudinary.DeleteResorceAsync(comment.Image);

            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return new BaseResponse() { Status = true, Message = "The comment delete successfully" };
        }
        public async Task<BaseResponse> CreatePostAsync(CreatePostRequest request)
        {
            if (await _userManager.FindByIdAsync(request.UserId) == null)
                return new BaseResponse { Message = "Something is wrong" };

            if (string.IsNullOrEmpty(request.description) && request.Poster == null)
                return new BaseResponse { Message = "An empty post cannot be created" };

            var poster = string.Empty;
            if (!(request.Poster == null))
            {
                var uploadPoster = await _cloudinary.UploadAsync(request.Poster);
                if (!uploadPoster.Status)
                    return new BaseResponse { Message = uploadPoster.Message };
                poster = uploadPoster.Data.ToString();
            }

            var post = new Post
            {
                UserId = request.UserId,
                description = request.description,
                Poster = string.IsNullOrEmpty(poster) ? null :poster
            };
            await _context.Posts.AddAsync(post);
            _context.SaveChanges();
            return new BaseResponse { Status = true, Message = "Post created successfully" };
        }

        public async Task<BaseResponse> DeletePostAsync(string PostId)
        {

            var post = await _context.Posts.FindAsync(PostId);
            if (post == null)
                return new BaseResponse { Message = "Invalid Post Id" };
            if (!string.IsNullOrEmpty(post.Poster))
            {
                await _cloudinary.DeleteResorceAsync(post.Poster);

            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return new BaseResponse() { Status = true, Message = "The post delete successfully" };
        }


        public async Task<BaseResponse> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(u => u.Users)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .Select(p => new
                {
                    PostId = p.Id,
                    UserId = p.UserId,
                    Name = p.Users.FirstName + " " + p.Users.LastName,
                    ProfilePic = p.Users.ProfilePic ?? defaultProfilePicture,
                    Description = p.description,
                    Poster = p.Poster,
                    PostCreated = p.CreateAt,
                    NumberOfComments=p.Comments.Count,
                    Comments = p.Comments.Select(c => new
                    {
                        CommentId = c.Id,
                        CommenterId = c.UserId,
                        CommenterName = c.Users.FirstName + " " + c.Users.LastName,
                        CommenterPic= c.Users.ProfilePic ?? defaultProfilePicture,
                        DescriptionComment = c.description,
                        Image = c.Image,
                        CommentCreated = c.CrateAt

                    }),
                    NumberOfLikes =p.Likes.Count,
                    Likes = p.Likes.Select(l => new
                    {
                        LikeId= l.LikeId,
                        UserId=l.UserId,
                        Name= l.Users.FirstName + " " + l.Users.LastName,
                        LikedPic=l.Users.ProfilePic ?? defaultProfilePicture
                    })

                }).ToListAsync();
            return new BaseResponse { Data = posts };
        }

        public async Task<BaseResponse> AddLikeAsync(LikeRequest request)
        {
            var post = await _context.Posts.FindAsync(request.PostId);
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (post == null || user== null)
                return new BaseResponse { Message = "Something is wrong" };

            var check = await _context.Likes.Where(l=>l.UserId == request.UserId && l.PostId == request.PostId).ToListAsync();
            if(check.Any())
                return new BaseResponse { Message = "Something is wrong" };
            var like = new Like
            {
                PostId =request.PostId,
                UserId=request.UserId
            };
            await _context.Likes.AddAsync(like);
            _context.SaveChanges();
            return new BaseResponse { Status=true, Message = "OK" };
           
        }

        public async Task<BaseResponse> RemoveLike(string likeId)
        {
            var like = await _context.Likes.FindAsync(likeId);
            if(like == null)
            return new BaseResponse { Message = "Something is wrong" };

             _context.Likes.Remove(like);
            _context.SaveChanges();
            return new BaseResponse { Status = true, Message = "OK" };

        }

        //public async Task<BaseResponse> UpdatePostAsync(string PostId, UpdatePostRequest request)
        //{
        //    var post = await _context.Posts.FindAsync(PostId);
        //    if (post == null)
        //        return new BaseResponse { Message = "Post not found " };

        //    post.description = request.Description ?? post.description;
        //    if(request.Poster != null)
        //    {
        //        if (string.IsNullOrEmpty(post.Poster))
        //        {
        //            var upload = await _cloudinary.UploadAsync(request.Poster);
        //            if (!upload.Status)
        //                return new BaseResponse { Message = upload.Message };
        //            post.Poster = upload.Data.ToString();
        //        }
        //        else
        //        {
        //           var updatePoster = await _cloudinary.UpdateAsync(post.Poster,request.Poster);
        //            if(!updatePoster.Status)
        //                return new BaseResponse { Message = updatePoster.Message };
        //            post.Poster = updatePoster.Data.ToString();

        //        }

        //    }
        //    //post.Poster = request.Poster ?? post.Poster;
        //     _context.SaveChanges();
        //    return new BaseResponse() { Status = true, Message = "The post updated successfully" };

        //}
    }
}
