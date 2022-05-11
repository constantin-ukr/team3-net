using static GamesService.DTOs;

namespace GamesService.Clients
{
    public class CommentsClient
    {
        private readonly HttpClient _httpClient;

        public CommentsClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<IEnumerable<CommentDto>> GetAsync()
        {
            var comments = await _httpClient.GetFromJsonAsync<IEnumerable<CommentDto>>("/comments");
            return comments;
        }

    }
}
