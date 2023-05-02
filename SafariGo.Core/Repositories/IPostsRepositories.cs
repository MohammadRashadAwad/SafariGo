using SafariGo.Core.Dto.Request.Posts;
using SafariGo.Core.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariGo.Core.Repositories
{
    public interface IPostsRepositories
    {
        Task<BaseResponse> GetAllPosts();
        Task<BaseResponse> CreatePostAsync(CreatePostRequest request);
        Task<BaseResponse> DeletePostAsync(string PostId);
        //Task<BaseResponse> UpdatePostAsync(string PostId,UpdatePostRequest request);
        Task<BaseResponse> CreateCommentAsync(CommentRequest request);
        Task<BaseResponse> DeleteCommentAsync(string CommentId);

        Task<BaseResponse> AddLikeAsync(LikeRequest request);
        Task<BaseResponse> RemoveLike(string likeId);
    }
}
